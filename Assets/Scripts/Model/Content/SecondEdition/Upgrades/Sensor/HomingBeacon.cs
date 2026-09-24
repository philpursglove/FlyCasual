using ActionsList;
using Arcs;
using Content;
using Movement;
using Ship;
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

                if (move is ManeuverBearing.KoiogranTurn or ManeuverBearing.TallonRoll or ManeuverBearing.SegnorsLoop
                    or ManeuverBearing.Stationary or ManeuverBearing.ReverseStraight or ManeuverBearing.ReverseBank
                    or ManeuverBearing.SideslipAny)
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
            AskToUseAbility("Homing Beacon", AlwaysUseByDefault,
                UseHomingBeaconRanges,
                descriptionLong: "You may spend a charge to ignore range restrictions",
                imageHolder: HostShip,
                callback: delegate { ResetTargetLockRanges(); Triggers.FinishTrigger(); });
        }

        private void UseHomingBeaconRanges(object sender, EventArgs e)
        {
            HostUpgrade.State.SpendCharge();

            HostShip.SetTargetLockRange(0, int.MaxValue);
        }

        private void UseHomingBeaconAcquireTargetLock()
        {
            if (HostShip.SectorsInfo.GetEnemiesInAllSectors().Any(a => a.Key == ArcFacing.Front))
            {
                HostShip.SetTargetLockRange(1, 2);

                HostShip.AskPerformFreeAction(new TargetLockAction(),
                    delegate { HostUpgrade.State.SpendCharge(); ResetTargetLockRanges(); Triggers.FinishTrigger(); },
                    descriptionShort: "Homing Beacon",
                    descriptionLong: "You may acquire a target lock on an enemy ship in your front arc",
                    imageHolder: HostShip);
            }
        }

        private void ResetTargetLockRanges()
        {
            HostShip.SetTargetLockRange(1, 3);
        }
    }
}