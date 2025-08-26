using Abilities;
using ActionsList;
using Analytics;
using Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            HostShip.AskPerformFreeAction(
                new ReloadAction() {CanBePerformedWhileStressed = false},
                CleanUp,
                HostUpgrade.UpgradeInfo.Name,
                "After you perform a primary attack you may spend 1 Charge to perform a Reload action.",
                HostUpgrade
            );
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