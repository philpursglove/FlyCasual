using Ship;
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
    public class RelaySystemAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnTokenIsAssignedGlobal += CheckReceiveTargetLockAbility;

            HostShip.OnAttackHitAsAttacker += CheckPassOnTargetLockAbility;
        }

        private void CheckPassOnTargetLockAbility()
        {
            // Check for other friendly ship at range 0-1
            List<GenericShip> friendlyShipsAtRange0To1 = new List<GenericShip>();

            foreach (KeyValuePair<string, GenericShip> friendlyShip in HostShip.Owner.Ships)
            {
                if (friendlyShip.Value != HostShip)
                {
                    BoardTools.DistanceInfo distanceInfo = new BoardTools.DistanceInfo(HostShip, friendlyShip.Value);
                    if (distanceInfo.Range < 2)
                    {
                        friendlyShipsAtRange0To1.Add(friendlyShip.Value);
                    }
                }
            }

            if (HostShip.Tokens.HasToken<BlueTargetLockToken>() && friendlyShipsAtRange0To1.Any())
            {
                BlueTargetLockToken existingToken = (BlueTargetLockToken)HostShip.Tokens.GetToken(typeof(BlueTargetLockToken));
                ITargetLockable lockedObject = existingToken.OtherTargetLockTokenOwner;
                if (lockedObject != Combat.Defender) return;
                AskToUseAbility("Relay System",
                    NeverUseByDefault,
                    descriptionLong: $"You may pass your target lock to a friendly ship at range 0-1",
                    useAbility: delegate { PassTargetLock(lockedObject, friendlyShipsAtRange0To1); });
            }
        }

        private void PassTargetLock(ITargetLockable lockedObject, List<GenericShip> potentialRecipients)
        {
            // If only one possible recipient, skip the decision
            if (potentialRecipients.Count == 1)
            {
                TransferTargetLock(lockedObject, potentialRecipients.First());
            }
            else
            {
                SelectTargetForAbility(
                    TransferTargetLock,
                    potentialRecipients,
                    GetAiPriorityForTargetLockTransfer,
                    description: "Select a ship to transfer the target lock to",
                    showSkipButton: true
                );
            }
        }

        private int GetAiPriorityForTargetLockTransfer(GenericShip ship)
        {
            int priority = 0;
            // Prefer ships that are attacking
            if (ship.IsAttacking) priority += 100;
            // Prefer ships that don't already have a target lock on the same target
            if (ship.Tokens.HasToken<BlueTargetLockToken>())
            {
                BlueTargetLockToken existingToken = (BlueTargetLockToken)ship.Tokens.GetToken(typeof(BlueTargetLockToken));
                if (existingToken.OtherTargetLockTokenOwner == Combat.Defender) priority -= 50;
            }
            // Prefer ships with lower pilot skill (assuming they benefit more from the target lock)
            priority += (10 - ship.PilotInfo.Initiative);
            return priority;
        }

        private void TransferTargetLock(ITargetLockable lockedObject, GenericShip recipient)
        {
            BlueTargetLockToken existingToken = (BlueTargetLockToken)HostShip.Tokens.GetToken(typeof(BlueTargetLockToken));
            HostShip.RemoveToken(existingToken);
            ActionsHolder.AcquireTargetLock(recipient, lockedObject, delegate { }, delegate { }, false);
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnTokenIsAssignedGlobal -= CheckReceiveTargetLockAbility;

            HostShip.OnAttackHitAsAttacker -= CheckPassOnTargetLockAbility;
        }

        private ITargetLockable _lockedObject;

        private void CheckReceiveTargetLockAbility(GenericShip ship, GenericToken token)
        {
            if ((token is BlueTargetLockToken) && (ship.Owner.PlayerNo == HostShip.Owner.PlayerNo) &&
                (ship.GetRangeToShip(HostShip) < 3))
            {
                _lockedObject = ((BlueTargetLockToken)token).OtherTargetLockTokenOwner;

                AskToUseAbility("Relay System",
                    NeverUseByDefault,
                    descriptionLong: $"You may acquire a lock",
                    useAbility: AcquireTargetLock);
            }
        }

        private void AcquireTargetLock(object sender, EventArgs e)
        {
            if (HostShip.Tokens.HasToken<BlueTargetLockToken>())
            {
                BlueTargetLockToken existingToken = (BlueTargetLockToken)HostShip.Tokens.GetToken(typeof(BlueTargetLockToken));
                HostShip.RemoveToken(existingToken);
            }

            ActionsHolder.AcquireTargetLock(HostShip, _lockedObject, () => { }, () => { }, false);
        }
    }
}