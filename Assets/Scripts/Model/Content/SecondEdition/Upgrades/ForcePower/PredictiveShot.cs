﻿using Arcs;
using SubPhases;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class PredictiveShot : GenericUpgrade
    {
        public PredictiveShot() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Predictive Shot",
                UpgradeType.ForcePower,
                cost: 1,
                abilityType: typeof(Abilities.SecondEdition.PredictiveShotAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class PredictiveShotAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackStartAsAttacker += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsAttacker -= RegisterAbility;
        }

        private void RegisterAbility()
        {
            if (HostShip.State.Force > 0 &&
                HostShip.IsCanUseForceNow() &&
                HostShip.SectorsInfo.IsShipInSector(Combat.Defender, ArcType.Bullseye)
            )
            {
                RegisterAbilityTrigger(TriggerTypes.OnAttackStart, AskUsePredictiveShot);
            }
        }

        protected void AskUsePredictiveShot(object sender, System.EventArgs e)
        {
            AskToUseAbility(
                HostUpgrade.UpgradeInfo.Name,
                AlwaysUseByDefault,
                RegisterPredictiveShot,
                descriptionLong: "Do you want to spend 1 Force? (If you do, during the Roll Defense Dice step, the defender cannot roll more defense dice than the number of your \"hit\"/\"crit\" results.",
                imageHolder: HostUpgrade
            );
        }

        protected void RegisterPredictiveShot(object sender, System.EventArgs e)
        {
            Combat.Defender.AfterGotNumberOfDefenceDiceCap += SetDefenseDic;
            HostShip.State.SpendForce(1, DecisionSubPhase.ConfirmDecision);
        }

        private void SetDefenseDic(ref int result)
        {
            if (Combat.DiceRollAttack.Successes < result) { result = Combat.DiceRollAttack.Successes; }
            Combat.Defender.AfterGotNumberOfDefenceDiceCap -= SetDefenseDic;
        }
    }
}