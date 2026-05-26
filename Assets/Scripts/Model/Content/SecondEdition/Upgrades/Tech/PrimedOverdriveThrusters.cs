using ActionsList;
using BoardTools;
using Content;
using Movement;
using SubPhases;
using System;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class PrimedOverdriveThruster : GenericUpgrade
    {
        public PrimedOverdriveThruster() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Primed Overdrive Thruster",
                UpgradeType.Tech,
                cost: 0,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.PrimedOverdriveThrusterAbility),
                legalityInfo: new() { Legality.XWA }
            );

            IsHidden = true;
            ImageUrl = "https://infinitearenas.com/xw2xwa/images/pilots/poedameron-evacuationofdqar.png";
        }
    }
}

namespace Abilities.SecondEdition
{
    public class PrimedOverdriveThrusterAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnUpdateChosenBoostTemplate += UpdateBoostTemplate;
            HostShip.OnUpdateChosenBarrelRollTemplate += UpdateBarrelRollTemplate;
            HostShip.OnUpdateChosenSlamTemplate += UpdateSlamTemplate;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnUpdateChosenBoostTemplate -= UpdateBoostTemplate;
            HostShip.OnUpdateChosenBarrelRollTemplate -= UpdateBarrelRollTemplate;
            HostShip.OnUpdateChosenSlamTemplate -= UpdateSlamTemplate;
        }

        private void UpdateBoostTemplate(ref string name)
        {
            if (ActionsHolder.CurrentAction.IsRed)
            {
                IncreaseSpeedOfTemplateByName(ref name);
                AddActionTriggers();
            }
        }

        private void IncreaseSpeedOfTemplateByName(ref string name)
        {
            bool isChanged = false;

            if (name.Contains("1"))
            {
                name = name.Replace('1', '2');
                isChanged = true;
            }

            if (isChanged)
            {
                Messages.ShowInfo("Primed Overdrive Thursters: Template of 1 speed higher is used");
            }
        }

        private void UpdateBarrelRollTemplate(ref ManeuverTemplate maneuverTemplate)
        {
            if (ActionsHolder.CurrentAction.IsRed)
            {
                if (maneuverTemplate.TryIncreaseSpeed())
                {
                    Messages.ShowInfo("Primed Overdrive Thursters: Template of 1 speed higher is used");
                }
                AddActionTriggers();
            }
        }

        private void UpdateSlamTemplate(GenericMovement movement)
        {
            if (ActionsHolder.CurrentAction.IsRed)
            {
                if (movement.TryIncreaseSpeed())
                {
                    Messages.ShowInfo("Primed Overdrive Thursters: Template of 1 speed higher is used");
                }
                AddActionTriggers();
            }
        }

        private void AddActionTriggers()
        {
            HostShip.OnActionIsPerformed += RegisterTrigger1;
            HostShip.OnActionIsReallyFailed += RegisterTrigger2;
        }

        private void RegisterTrigger1(GenericAction action)
        {
            HostShip.OnActionIsPerformed -= RegisterTrigger1;
            HostShip.OnActionIsReallyFailed -= RegisterTrigger2;
            RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed,AskToSwapStressForStrain);
        }

        private void RegisterTrigger2(GenericAction action)
        {
            HostShip.OnActionIsPerformed -= RegisterTrigger1;
            HostShip.OnActionIsReallyFailed -= RegisterTrigger2;
            RegisterAbilityTrigger(TriggerTypes.OnActionIsReallyFailed,AskToSwapStressForStrain);
        }

        private void AskToSwapStressForStrain(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostUpgrade.UpgradeInfo.Name,
                AlwaysUseByDefault,
                SwapStressForStrain,
                descriptionLong: "Do you want to gain 1 Strain Token to remove 1 Stress Token?",
                imageHolder: HostUpgrade
            );
        }

        private void SwapStressForStrain(object sender, EventArgs e)
        {
            HostShip.Tokens.RemoveToken(typeof(StressToken), delegate
            {
                HostShip.Tokens.AssignToken(new StrainToken(HostShip),DecisionSubPhase.ConfirmDecision);
            });
        }
    }
}