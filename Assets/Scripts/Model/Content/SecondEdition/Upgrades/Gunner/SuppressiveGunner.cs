using Content;
using SubPhases;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class SuppressiveGunner : GenericUpgrade
    {
        public SuppressiveGunner() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Suppressive Gunner",
                UpgradeType.Gunner,
                cost: 7,
                abilityType: typeof(Abilities.SecondEdition.SuppressiveGunnerAbility),
                legalityInfo: new List<Legality>
                {
                    Legality.StandardLegal,
                    Legality.ExtendedLegal
                }
            );
        }
    }

    public class SuppressiveGunnerXWA : SuppressiveGunner
    {
        public SuppressiveGunnerXWA() : base()
        {
            UpgradeInfo.Cost = 6;
            UpgradeInfo.LegalityInfo = new List<Legality> { Legality.XWA };
            UpgradeInfo.Limited = 2;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SuppressiveGunnerAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                "Suppressive Gunner",
                IsAvailable,
                GetAiPriority,
                DiceModificationType.Cancel,
                count: 1,
                sidesCanBeSelected: new List<DieSide>() { DieSide.Focus },
                payAbilityCost: AskOpponentAbility
            );
        }

        private bool IsAvailable()
        {
            return Combat.AttackStep == CombatStep.Attack
                && Combat.DiceRollAttack.HasResult(DieSide.Focus);
        }

        private int GetAiPriority()
        {
            return 53;
        }

        private void AskOpponentAbility(Action<bool> callBack)
        {
            RegisterAbilityTrigger(TriggerTypes.OnAbilityDirect, AskOpponentAbilityTrigger);

            Triggers.ResolveTriggers(TriggerTypes.OnAbilityDirect, delegate { callBack(true); });
        }

        private void AskOpponentAbilityTrigger(object sender, EventArgs e)
        {
            AskOpponent(
                AiUseByDefault,
                GetDeplete,
                DealDamage,
                descriptionShort: "Suppresive Gunner",
                descriptionLong: "Do you want to get a Deplete token?\nIf not, you will supper 1 regular damage.",
                imageSource: HostUpgrade,
                showSkipButton: false
            );
        }

        private bool AiUseByDefault()
        {
            return true;
        }

        private void GetDeplete(object sender, EventArgs e)
        {
            DecisionSubPhase.ConfirmDecisionNoCallback();
            Combat.Defender.Tokens.AssignToken(
                typeof(DepleteToken),
                Triggers.FinishTrigger
            );
        }

        private void DealDamage(object sender, EventArgs e)
        {
            DecisionSubPhase.ConfirmDecisionNoCallback();
            Combat.Defender.Damage.TryResolveDamage(
                damage: 1,
                new DamageSourceEventArgs()
                {
                    DamageType = DamageTypes.CardAbility,
                    Source = HostUpgrade
                },
                Triggers.FinishTrigger
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }
    }
}