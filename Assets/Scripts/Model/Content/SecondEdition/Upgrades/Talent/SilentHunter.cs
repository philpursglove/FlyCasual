using Arcs;
using BoardTools;
using Ship;
using SubPhases;
using System.Collections.Generic;
using System.Linq;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class SilentHunter : GenericUpgrade
    {
        public SilentHunter() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Silent Hunter",
                UpgradeType.Talent,
                abilityType: typeof(Abilities.SecondEdition.SilentHunterAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SilentHunterAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnDecloak += CheckAbility;
        }

        private void CheckAbility()
        {
            // Are there any enemy ships in bullseye
            // Offer a target lock on one of them
            var enemyShips = HostShip.SectorsInfo.GetEnemiesInAllSectors();
            var shipsInBullseye = enemyShips.TryGetValue(ArcFacing.Bullseye, out var bullseyeShips) ? bullseyeShips : new List<GenericShip>();
            if (shipsInBullseye.Any())
            {
                SelectTargetForAbility(
                    GrantFreeTargetLock,
                    FilterAbilityTargets,
                    GetAiAbilityPriority,
                    HostShip.Owner.PlayerNo,
                    HostUpgrade.State.Name,
                    "You may spend 1 Charge to acquire a lock on an object in your front arc",
                    HostUpgrade
                );
            }
        }

        private bool FilterAbilityTargets(GenericShip ship)
        {
            return HostShip.SectorsInfo.IsShipInSector(ship, ArcType.Bullseye);
        }

        private void GrantFreeTargetLock()
        {
            if (TargetShip != null)
            {
                ActionsHolder.AcquireTargetLock(HostShip, TargetShip, SelectShipSubPhase.FinishSelection, SelectShipSubPhase.FinishSelection);
            }
            else
            {
                SelectShipSubPhase.FinishSelection();
            }
        }

        private int GetAiAbilityPriority(GenericShip ship)
        {
            int priority = 0;

            if (!HostShip.Tokens.HasToken(typeof(BlueTargetLockToken))) priority += 50;
            ShotInfo shotInfo = new ShotInfo(HostShip, ship, ship.PrimaryWeapons);
            if (shotInfo.IsShotAvailable) priority += 40;

            priority += ship.PilotInfo.Cost;

            return priority;
        }

        public override void DeactivateAbility()
        {
            // Ability deactivation logic goes here
            HostShip.OnDecloak -= CheckAbility;
        }
    }
}