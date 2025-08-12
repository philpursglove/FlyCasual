using ActionsList;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using System.Linq;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class TargetingMatrix : GenericUpgrade
    {
        public TargetingMatrix() : base()
        {
            IsHidden = true;

            UpgradeInfo = new UpgradeCardInfo(
                "Targeting Matrix",
                UpgradeType.Modification,
                cost: 0,
                abilityType: typeof(Abilities.SecondEdition.TargetingMatrixAbility)
            );
            ImageUrl = "https://raw.githubusercontent.com/sampson-matt/FlyCasualLegacyCustomCards/refs/heads/main/BattleOverEndor/ChaffParticles.jpg";
        }        
    }
}

namespace Abilities.SecondEdition
{
    public class TargetingMatrixAbility : GenericAbility
    {

        public override void ActivateAbility()
        {
            HostShip.OnAttackStartAsAttacker += AssignTrigger;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsAttacker -= AssignTrigger;
        }

        public void AssignTrigger()
        {
            HostShip.OnAfterNeutralizeResults += CheckAbility;
        }

        public void ReleaseTrigger()
        {
            HostShip.OnAfterNeutralizeResults -= CheckAbility;
        }

        private void CheckAbility()
        {
            ReleaseTrigger();

            if (Combat.DiceRollAttack.Focuses > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.OnAfterNeutralizeResults, AskToStrain);
            }
        }

        private void AskToStrain(object sender, EventArgs e)
        {

            AskToUseAbility(
                    HostUpgrade.UpgradeInfo.Name,
                    AlwaysUseByDefault,
                    UseAbility,
                    descriptionLong: "Do you want to spend one focus result to assign a strain token to the defender?",
                    imageHolder: HostUpgrade
                );
        }

        private void UseAbility(object sender, EventArgs e)
        {
            Combat.Defender.Tokens.AssignToken(new StrainToken(Combat.Defender), DecisionSubPhase.ConfirmDecision);
        }
    }
}

