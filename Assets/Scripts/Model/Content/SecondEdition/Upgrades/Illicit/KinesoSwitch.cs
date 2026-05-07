using ActionsList;
using SubPhases;
using System;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class KinesoSwitch : GenericUpgrade
    {
        public KinesoSwitch() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Kineso-Switch",
                UpgradeType.Illicit,
                cost: 0,
                charges: 2,
                abilityType: typeof(Abilities.SecondEdition.KinesoSwitchAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class KinesoSwitchAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += RegisterKinesoSwitch;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= RegisterKinesoSwitch;
        }

        private void RegisterKinesoSwitch(GenericAction action)
        {
            if (HostUpgrade.State.Charges > 0 && !HostShip.IsBumped
                                              && HostShip.Tokens.HasToken<RedTargetLockToken>())
            {
                AskToUseAbility(
                    descriptionShort: HostUpgrade.UpgradeInfo.Name,
                    descriptionLong: "Do you want to spend 1 charge to break all target locks on you?",
                    useByDefault: AlwaysUseByDefault,
                    useAbility: BreakLocks,
                    imageHolder: HostUpgrade
                );
            }
        }

        private void BreakLocks(object sender, EventArgs e)
        {
            DecisionSubPhase.ConfirmDecision();

            HostUpgrade.State.SpendCharge();
            HostShip.Tokens.RemoveAllTokensByType(typeof(RedTargetLockToken), null);
        }
    }
}
