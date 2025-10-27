using ActionsList;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class BBAstromech : GenericUpgrade
    {
        public BBAstromech() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "BB Astromech",
                UpgradeType.Astromech,
                charges: 2,
                cost: 4,
                restriction: new FactionRestriction(Faction.Resistance),
                abilityType: typeof(Abilities.SecondEdition.BBAstromechAbility),
                legalityInfo: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class BBAstromechXWA : BBAstromech
    {
        public BBAstromechXWA() : base()
        {
            UpgradeInfo.Cost = 3;
            UpgradeInfo.LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    //Before you execute a blue maneuver, you may spend 1 charge to perform a barrel roll action.
    public class BBAstromechAbility : GenericAbility
    {
        protected List<GenericAction> AbilityActions = new List<GenericAction> { new BarrelRollAction() };

        public override void ActivateAbility()
        {
            HostShip.BeforeMovementIsExecuted += PlanAction;
        }

        public override void DeactivateAbility()
        {
            HostShip.BeforeMovementIsExecuted -= PlanAction;
        }

        private void PlanAction(GenericShip host)
        {
            if (host.AssignedManeuver.ColorComplexity == Movement.MovementComplexity.Easy && HostUpgrade.State.Charges > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.BeforeMovementIsExecuted, AskPerformAction);
            }
        }

        private void AskPerformAction(object sender, EventArgs e)
        {
            HostShip.BeforeActionIsPerformed += SpendCharge;

            HostShip.AskPerformFreeAction(
                AbilityActions,
                CleanUp,
                HostUpgrade.UpgradeInfo.Name,
                "Before you execute a blue maneuver, you may spend 1 Charge to perform a Barrel Roll action",
                HostUpgrade
            );
        }

        private void SpendCharge(GenericAction action, ref bool isFreeAction)
        {
            HostShip.BeforeActionIsPerformed -= SpendCharge;
            Sounds.PlayShipSound("BB-8-Sound");
            HostUpgrade.State.SpendCharge();
        }

        private void CleanUp()
        {
            HostShip.BeforeActionIsPerformed -= SpendCharge;
            Triggers.FinishTrigger();
        }
    }
}