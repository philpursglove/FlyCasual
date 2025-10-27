using BoardTools;
using Content;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.DroidTriFighter
{
    public class Dis347 : DroidTriFighter
    {
        public Dis347()
        {
            PilotInfo = new PilotCardInfo25
            (
                "DIS-347",
                "Target Acquired",
                Faction.Separatists,
                3,
                3,
                4,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.Dis347Ability),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Sensor,
                    UpgradeType.Missile,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Droid
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class Dis347XWA : Dis347
    {
        public Dis347XWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 9;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Talent,
                UpgradeType.Sensor,
                UpgradeType.Modification,
                UpgradeType.Modification,
                UpgradeType.Cannon,
                UpgradeType.Missile,
                UpgradeType.Configuration
            };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class Dis347Ability : GenericAbility
    {
        public override void ActivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers -= RegisterAbility;
        }

        private void RegisterAbility()
        {
            RegisterAbilityTrigger(TriggerTypes.OnCombatPhaseStart, AskToSelectTarget);
        }

        private void AskToSelectTarget(object sender, EventArgs e)
        {
            if (HasTargetsForAbility())
            {
                SelectTargetForAbility(
                    GetLock,
                    FilterTargets,
                    GetAiPriority,
                    HostShip.Owner.PlayerNo,
                    name: "DIS-347",
                    description: "You may acquire a lock on an object at range 1-3 that has a friendly lock",
                    imageSource: HostShip,
                    showSkipButton: true
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private bool HasTargetsForAbility()
        {
            bool result = false;

            foreach (GenericShip unit in Roster.AllUnits.Values)
            {
                if (FilterTargets(unit))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private void GetLock()
        {
            DecisionSubPhase.ConfirmDecisionNoCallback();

            ActionsHolder.AcquireTargetLock(
                HostShip,
                TargetShip,
                Triggers.FinishTrigger,
                Triggers.FinishTrigger
            );
        }

        private bool FilterTargets(GenericShip ship)
        {
            bool result = false;

            foreach (GenericShip friendlyShip in HostShip.Owner.Ships.Values)
            {
                if (ActionsHolder.HasTargetLockOn(friendlyShip, ship))
                {
                    DistanceInfo distInfo = new DistanceInfo(HostShip, ship);
                    if (distInfo.Range >= 1 && distInfo.Range <= 3)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private int GetAiPriority(GenericShip ship)
        {
            ShotInfo shotInfo = new ShotInfo(HostShip, ship, HostShip.PrimaryWeapons);
            return ship.PilotInfo.Cost + ((shotInfo.IsShotAvailable) ? 100 : 0);
        }
    }
}
