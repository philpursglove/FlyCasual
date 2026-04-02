using ActionsList;
using Obstacles;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using System.Linq;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class RelaySystem : GenericUpgrade
    {
        public RelaySystem()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Relay System",
                UpgradeType.Sensor,
                abilityType: typeof(Abilities.SecondEdition.RelaySystemAbility)
            );

            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    // After a friendly ship at range 0-2 performs a target lock action, you may acquire a lock on the same object.
    // After you perform an attack that hits, you may spend a lock you have on the defender.
    // If you do, another friendly ship at range 0-1 may acquire a lock on the defender.
    public class RelaySystemAbility : GenericAbility
    {
        GenericShip friendlyShip;
        List<GenericShip> potentialRecipients;
        BlueTargetLockToken targetLock;

        public override void ActivateAbility()
        {
            GenericShip.OnActionIsPerformedGlobal += RegisterTargetLockActionCheck;

            HostShip.OnAttackHitAsAttacker += CheckPassOnTargetLockAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnActionIsPerformedGlobal -= RegisterTargetLockActionCheck;

            HostShip.OnAttackHitAsAttacker -= CheckPassOnTargetLockAbility;
        }

        private void RegisterTargetLockActionCheck(GenericAction action)
        {
            if (IsValidFriendlyLockAction(action))
            {
                friendlyShip = action.HostShip;

                RegisterAbilityTrigger(TriggerTypes.OnTargetLockIsAcquired, AskAcquireTargetLock);
            }
        }

        private bool IsValidFriendlyLockAction(GenericAction action)
        {
            return (action is TargetLockAction) &&
                   Tools.IsFriendly(HostShip, action.HostShip) &&
                   (action.HostShip.GetRangeToShip(HostShip) < 3);
        }

        private void AskAcquireTargetLock(object sender, EventArgs e)
        {
            // Last lock should be the latest
            ITargetLockable lockedObject = friendlyShip.Tokens.GetTokens<BlueTargetLockToken>('*').Last().OtherTargetLockTokenOwner;


            string objectName = null;

            if (lockedObject is GenericShip)
            {
                objectName = (lockedObject as GenericShip).PilotInfo.PilotName;
                if (HostShip.GetRangeToShip((lockedObject as GenericShip)) > HostShip.TargetLockMaxRange)
                {
                    // Unable to acquire lock due to distance from lockedObject
                    Triggers.FinishTrigger();
                    return;
                }
            }
            else if (lockedObject is GenericObstacle)
            {
                // Need to figure out how to check range here
                objectName = (lockedObject as GenericObstacle).Name;
            }

            AskToUseAbility(
                HostShip.PilotInfo.PilotName,
                GetAIPriorityToAcquireLock, // only use when doesn't already have a lock?
                delegate
                {
                    ActionsHolder.AcquireTargetLock(HostShip, lockedObject, DecisionSubPhase.ConfirmDecision, DecisionSubPhase.ConfirmDecision);
                },
                descriptionLong: $"Do you want to acquire a Target Lock{(!String.IsNullOrWhiteSpace(objectName) ? $" on {objectName}" : "")}?",
                imageHolder: HostShip
            );
        }

        private bool GetAIPriorityToAcquireLock()
        {
            // Don't use if already have a target lock
            return !HostShip.Tokens.HasToken<BlueTargetLockToken>('*');
        }

        private void CheckPassOnTargetLockAbility()
        {
            // Check for host ship having target lock on defender
            targetLock = null;

            foreach (BlueTargetLockToken tl in HostShip.Tokens.GetTokens<BlueTargetLockToken>('*'))
            {
                if (tl.OtherTargetLockTokenOwner == Combat.Defender)
                {
                    targetLock = tl;
                    break;
                }
            }

            if (targetLock is null) return;


            // Check for other friendly ship at range 0-1 who are in range of defender
            potentialRecipients = new();

            foreach (GenericShip friendlyShip in HostShip.Owner.Ships.Values)
            {
                if (friendlyShip != HostShip &&
                    HostShip.GetRangeToShip(friendlyShip) < 2 &&
                    friendlyShip.GetRangeToShip(Combat.Defender) <= friendlyShip.TargetLockMaxRange)
                {
                    potentialRecipients.Add(friendlyShip);
                }
            }

            if (potentialRecipients.Any())
            {
                RegisterAbilityTrigger(TriggerTypes.OnAttackHit, PassTargetLock);
            }
        }

        private void PassTargetLock(Object sender, EventArgs e)
        {
            SelectTargetForAbility(
                TransferTargetLock,
                FilterAbilityTargets,
                GetAiPriorityForTargetLockTransfer,
                HostShip.Owner.PlayerNo,
                HostShip.PilotInfo.PilotName,
                description: "You may spend a lock to allow a friendly ship at range 0-1 to acquire a target lock on the defender.",
                showSkipButton: true,
                callback: Triggers.FinishTrigger
            );
        }

        private void TransferTargetLock()
        {
            HostShip.Tokens.SpendToken(
                targetLock,
                delegate
                {
                    ActionsHolder.AcquireTargetLock(TargetShip, Combat.Defender, DecisionSubPhase.ConfirmDecision, DecisionSubPhase.ConfirmDecision);
                }
            );
        }

        private bool FilterAbilityTargets(GenericShip ship)
        {
            if (potentialRecipients.Contains(ship))
                return true;

            return false;
        }

        private int GetAiPriorityForTargetLockTransfer(GenericShip ship)
        {
            int priority = 0;

            // Prefer ships that haven't attacked yet
            if (ship.HasCombatActivation) priority += 100;

            // Prefer ships that don't already have a target lock on the same target
            foreach (BlueTargetLockToken blueLock in ship.Tokens.GetTokens<BlueTargetLockToken>('*'))
            {
                if (blueLock.OtherTargetLockTokenOwner == Combat.Defender) priority -= 50;
            }

            // Prefer ships with lower pilot skill (assuming they benefit more from the target lock)
            priority += (10 - ship.PilotInfo.Initiative);

            return priority;
        }
    }
}