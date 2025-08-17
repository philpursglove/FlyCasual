using Abilities.SecondEdition;
using ActionsList;
using System;
using System.Linq;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEInterceptor
    {
        public class MausMonareBoE : TIEInterceptor
        {
            public MausMonareBoE()
            {
                PilotInfo = new PilotCardInfo(
                    "Maus Monare",
                    3,
                    42,
                    isLimited: true,
                    abilityType: typeof(MausMonareAbility),
                    extraUpgradeIcon: UpgradeType.Talent
                );
                PilotNameCanonical = "mausmonare-battleoverendor";
                ShipInfo.Shields++;
                ShipInfo.UpgradeIcons.Upgrades.Remove(UpgradeType.Modification);
                ShipInfo.UpgradeIcons.Upgrades.Remove(UpgradeType.Configuration);
                AutoThrustersAbility oldAbility = (AutoThrustersAbility)ShipAbilities.First(n => n.GetType() == typeof(AutoThrustersAbility));
                ShipAbilities.Remove(oldAbility);
                ShipAbilities.Add(new SensitiveControlsRealAbility());
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    //After you perform an evade action, gain a calculate token.
    public class MausMonareAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += CheckCalculateBonus;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= CheckCalculateBonus;
        }

        private void CheckCalculateBonus(GenericAction action)
        {
            if (action is EvadeAction)
            {
                RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AssignBonusCalculateToken);
            }
        }
        private void AssignBonusCalculateToken(object sender, EventArgs e)
        {
            Messages.ShowInfo($"{HostShip.PilotInfo.PilotName} gains Calculate token");

            HostShip.Tokens.AssignToken(typeof(CalculateToken), Triggers.FinishTrigger);
        }

    }
}