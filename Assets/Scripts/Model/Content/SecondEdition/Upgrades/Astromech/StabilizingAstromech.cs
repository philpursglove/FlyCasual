using Abilities.SecondEdition;
using Actions;
using ActionsList;
using NUnit.Framework;
using Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class StabilizingAstromech : GenericUpgrade
    {
        public StabilizingAstromech() : base()
        {
            IsHidden = true;

            UpgradeInfo = new UpgradeCardInfo(
                "Stabilizing Astromech",
                UpgradeType.Astromech,
                cost: 0,
                charges: 1,
                abilityType: typeof(StabilizingAstromechAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    //After you fully execute a maneuver, you may spend 1 charge to perform a white action, even while stressed.
    public class StabilizingAstromechAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully -= RegisterAbility;
        }

        private void RegisterAbility(GenericShip ship)
        {
            if (HostUpgrade.State.Charges > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, PerformFreeAction);
            }
        }

        private void PerformFreeAction(object sender, EventArgs e)
        {
            HostShip.BeforeActionIsPerformed += RegisterSpendChargeTrigger;
            HostShip.OnCanPerformActionWhileStressed += FilterActions;

            List<GenericAction> actions = HostShip.GetAvailableActionsWhiteOnly();

            if (actions.Count == 0) CleanUp();

            Selection.ThisShip.AskPerformFreeAction(actions,
                CleanUp,
                HostUpgrade.UpgradeInfo.Name,
                "After you fully execute a maneuver, you may spend 1 charge to perform a white action, even while stressed.",
                HostUpgrade
            );
        }

        private void FilterActions(GenericAction action, ref bool isAllowed)
        {
            isAllowed = action.Color == ActionColor.White;
        }

        private void RegisterSpendChargeTrigger(GenericAction action, ref bool isFreeAction)
        {
            HostShip.BeforeActionIsPerformed -= RegisterSpendChargeTrigger;

            RegisterAbilityTrigger(
                TriggerTypes.OnFreeAction,
                delegate
                {
                    HostUpgrade.State.SpendCharge();
                    Triggers.FinishTrigger();
                }
            );
        }

        private void CleanUp()
        {
            HostShip.BeforeActionIsPerformed -= RegisterSpendChargeTrigger;
            HostShip.OnCanPerformActionWhileStressed -= FilterActions;
            Triggers.FinishTrigger();
        }
    }
}