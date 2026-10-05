using ActionsList;
using Arcs;
using Content;
using Movement;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class HomingBeacon : GenericUpgrade
    {
        public HomingBeacon()
        {
            UpgradeInfo = new UpgradeCardInfo(
                name: "Homing Beacon",
                type: UpgradeType.Sensor,
                cost: 0,
                abilityType: typeof(Abilities.SecondEdition.HomingBeaconAbility),
                charges: 2,
                legalityInfo: new List<Legality> { Legality.XWA });
            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class HomingBeaconAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.BeforeActionIsPerformed += RegisterHomingBeaconAbilityOnAction;
            HostShip.OnMovementFinishSuccessfully += RegisterHomingBeaconAbilityOnMovement;
        }
        public override void DeactivateAbility()
        {
            HostShip.BeforeActionIsPerformed -= RegisterHomingBeaconAbilityOnAction;
            HostShip.OnMovementFinishSuccessfully -= RegisterHomingBeaconAbilityOnMovement;
        }

        private void RegisterHomingBeaconAbilityOnMovement(GenericShip ship)
        {
            if (HostUpgrade.State.Charges > 0)
            {
                ManeuverBearing move = HostShip.GetLastManeuverBearing();

                if (move.IsAdvancedManeuver())
                {
                    Triggers.RegisterTrigger(new Trigger()
                    {
                        Name = "Homing Beacon",
                        TriggerType = TriggerTypes.OnMovementFinish,
                        EventHandler = delegate { UseHomingBeaconAcquireTargetLock(); }
                    });
                }
            }
        }

        private void RegisterHomingBeaconAbilityOnAction(GenericAction action, ref bool isFree)
        {
            if (HostUpgrade.State.Charges > 0 && action is TargetLockAction)
            {
                Triggers.RegisterTrigger(new Trigger()
                {
                    Name = "Homing Beacon",
                    TriggerType = TriggerTypes.OnActionIsPerformed,
                    EventHandler = delegate { UseHomingBeaconTargetLock(); }
                });
            }
        }

        private void UseHomingBeaconTargetLock()
        {
            AskToUseAbility(descriptionShort: "Homing Beacon",
                useByDefault: AlwaysUseByDefault,
                useAbility: UseHomingBeaconRanges,
                descriptionLong: "You may spend a charge to ignore range restrictions",
                imageHolder: HostShip,
                callback: delegate { ResetTargetLockRanges(); Triggers.FinishTrigger(); });
        }

        private void UseHomingBeaconRanges(object sender, EventArgs e)
        {
            HostUpgrade.State.SpendCharge();

            HostShip.SetTargetLockRange(0, int.MaxValue);

            DecisionSubPhase.ConfirmDecision();
        }

        private void UseHomingBeaconAcquireTargetLock()
        {
            List<GenericShip> enemiesInFrontArc = HostShip.SectorsInfo.GetEnemiesInAllSectors()
                .Single(a => a.Key == ArcFacing.Front).Value;

            if (enemiesInFrontArc.Any() &&
                enemiesInFrontArc.Any(a => HostShip.SectorsInfo.RangeToShipBySector(a, ArcType.Front) >= 1
                                           && HostShip.SectorsInfo.RangeToShipBySector(a, ArcType.Front) <= 2))
            {
                HostShip.SetTargetLockRange(1, 2);

                HostShip.AskPerformFreeAction(new TargetLockAction() { CanBePerformedWhileStressed = true },
                    delegate { HostUpgrade.State.SpendCharge(); ResetTargetLockRanges(); Triggers.FinishTrigger(); },
                    descriptionShort: "Homing Beacon",
                    descriptionLong: "You may acquire a target lock on an enemy ship in your front arc at range 1-2",
                    imageHolder: HostShip);
            }
        }

        private void ResetTargetLockRanges()
        {
            HostShip.SetTargetLockRange(1, 3);
        }
    }

    public class HomingBeaconTargetLockAction : TargetLockAction
    {
    }
}