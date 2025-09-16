using ActionsList;
using Ship;
using System;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class AutomatedLoaders : GenericUpgrade
    {
        public AutomatedLoaders()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Automated Loaders",
                UpgradeType.Modification,
                cost: 0,
                abilityType: typeof(Abilities.SecondEdition.AutomatedLoadersAbility),
                charges: 1
            );

            IsWIP = true; // Mark as Work In Progress if applicable

            ImageUrl = "https://infinitearenas.com/xw2/images/quickbuilds/majorrhymer-swz98.png";
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AutomatedLoadersAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackFinishAsAttacker += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackFinishAsAttacker -= CheckAbility;
        }

        private void CheckAbility(GenericShip ship)
        {
            if (Combat.ShotInfo.Weapon.WeaponType != WeaponTypes.PrimaryWeapon) return;

            RegisterAbilityTrigger(TriggerTypes.OnAttackFinish, AskUseAbility);
        }

        private void AskUseAbility(object sender, EventArgs e)
        {
            HostShip.BeforeActionIsPerformed += RegisterSpendChargeTrigger;

            //TODO Would be neat if this could check whether any ordnance upgrades are currently reloadable

            if (HostUpgrade.UpgradeInfo.Charges > 0)
            {
                HostShip.AskPerformFreeAction(
                    new ReloadAction() { CanBePerformedWhileStressed = false },
                    CleanUp,
                    HostUpgrade.UpgradeInfo.Name,
                    "After you perform a primary attack you may spend 1 Charge to perform a Reload action.",
                    HostUpgrade
                );
            }

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
            Triggers.FinishTrigger();
        }
    }
}