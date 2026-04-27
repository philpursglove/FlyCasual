using ActionsList;
using Content;
using Movement;
using Ship;
using SubPhases;
using System;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class AdaptivePowerSystems : GenericUpgrade
    {
        public AdaptivePowerSystems() : base()
        {
            UpgradeInfo = new UpgradeCardInfo
            (
                "Adaptive Power Systems",
                UpgradeType.Modification,
                charges: 2,
                abilityType: typeof(Abilities.SecondEdition.AdaptivePowerSystemsAbility),
                legalityInfo: new() { Legality.XWA }
            );
            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AdaptivePowerSystemsAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += CheckAbilityAction;
            HostShip.OnMovementFinishSuccessfully += CheckAbilityMovement;
            HostShip.OnAttackStartAsAttacker += CheckAbilityAttack;
        }
        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= CheckAbilityAction;
            HostShip.OnMovementFinishSuccessfully -= CheckAbilityMovement;
            HostShip.OnAttackStartAsAttacker -= CheckAbilityAttack;
        }

        private void CheckAbilityAction(GenericAction action)
        {
            if (HostUpgrade.State.Charges > 0 && action.IsRed)
            {
                Triggers.RegisterTrigger(new Trigger()
                {
                    Name = "Adaptive Power Systems",
                    TriggerType = TriggerTypes.OnActionIsPerformed,
                    TriggerOwner = HostShip.Owner.PlayerNo,
                    EventHandler = UseAdaptivePowerSystemsToRemoveStressToken
                });
            }
        }

        private void CheckAbilityMovement(GenericShip ship)
        {
            if (HostUpgrade.State.Charges > 0 && ship.AssignedManeuver.ColorComplexity == MovementComplexity.Complex)
            {
                Triggers.RegisterTrigger(new Trigger()
                {
                    Name = "Adaptive Power Systems",
                    TriggerType = TriggerTypes.OnMovementFinish,
                    TriggerOwner = HostShip.Owner.PlayerNo,
                    EventHandler = UseAdaptivePowerSystemsToRemoveStressToken
                });
            }
        }

        private void UseAdaptivePowerSystemsToRemoveStressToken(object sender, EventArgs e)
        {
            AskToUseAbility("Adaptive Power Systems",
                AlwaysUseByDefault,
                RemoveStressTokenAndAddDeplete,
                descriptionLong: "You may spend 1 charge to remove 1 Stress token and gain 1 Deplete token.",
                imageHolder: HostShip,
                callback: Triggers.FinishTrigger
            );
        }

        private void RemoveStressTokenAndAddDeplete(object sender, EventArgs e)
        {
            HostShip.Tokens.RemoveToken(typeof(StressToken), null);
            HostShip.Tokens.AssignToken(new DepleteToken(HostShip), null);
            DecisionSubPhase.ConfirmDecision();
            HostUpgrade.State.SpendCharge();
        }

        private void CheckAbilityAttack()
        {
            if (HostUpgrade.State.Charges > 0 && HostShip.Tokens.HasToken<DepleteToken>())
            {
                Triggers.RegisterTrigger(new Trigger()
                {
                    Name = "Adaptive Power Systems",
                    TriggerType = TriggerTypes.OnAttackStart,
                    TriggerOwner = HostShip.Owner.PlayerNo,
                    EventHandler = UseAdaptivePowerSystemsToRemoveDepleteToken
                });
            }
        }

        private void UseAdaptivePowerSystemsToRemoveDepleteToken(object sender, EventArgs e)
        {
            AskToUseAbility("Adaptive Power Systems",
                AlwaysUseByDefault,
                RemoveDepleteTokenAndAddStrain,
                descriptionLong: "You may spend 1 charge to remove 1 Deplete token and gain 1 Strain token.",
                imageHolder: HostShip,
                callback: Triggers.FinishTrigger
            );
        }

        private void RemoveDepleteTokenAndAddStrain(object sender, EventArgs e)
        {
            HostShip.Tokens.RemoveToken(typeof(DepleteToken), null);
            HostShip.Tokens.AssignToken(new StrainToken(HostShip), null);
            DecisionSubPhase.ConfirmDecision();
            HostUpgrade.State.SpendCharge();
        }
    }
}