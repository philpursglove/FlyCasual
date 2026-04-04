using Arcs;
using BoardTools;
using Ship;
using SubPhases;
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

            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    // After you declock, you may acquire a lock on an enemy ship in your bullseye.

    public class SilentHunterAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnDecloak += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnDecloak -= CheckAbility;
        }

        private void CheckAbility()
        {
            if (HostShip.Owner.AnotherPlayer.Ships.Values.Any(s => FilterAbilityTargets(s)))
            {
                SelectTargetForAbility(
                    GrantFreeTargetLock,
                    FilterAbilityTargets,
                    GetAiAbilityPriority,
                    HostShip.Owner.PlayerNo,
                    HostUpgrade.State.Name,
                    "You may spend 1 Charge to acquire a lock on an enemy ship in your bullseye",
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

            if (new ShotInfo(HostShip, ship, ship.PrimaryWeapons).IsShotAvailable) priority += 40;

            priority += ship.PilotInfo.Cost;

            return priority;
        }
    }
}