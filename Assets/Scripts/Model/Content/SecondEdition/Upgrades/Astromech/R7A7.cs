using System;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class R7A7 : GenericUpgrade
    {
        public R7A7() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "R7-A7",
                UpgradeType.Astromech,
                cost: 5,
                isLimited: true,
                charges: 3,
                restriction: new FactionRestriction(Faction.Republic),
                abilityType: typeof(Abilities.SecondEdition.R7A7Ability)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class R7A7Ability : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                "R7-A7",
                CanBeUsed,
                GetAiPriority,
                DiceModificationType.Change,
                count: 1,
                sidesCanBeSelected: new List<DieSide>() { DieSide.Success },
                sideCanBeChangedTo: DieSide.Crit,
                payAbilityCost: SpendCharge
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private bool CanBeUsed()
        {
            return (Combat.AttackStep == CombatStep.Attack && HostUpgrade.State.Charges > 0);
        }

        private int GetAiPriority()
        {
            return 20;
        }

        private void SpendCharge(Action<bool> callback)
        {
            if (HostUpgrade.State.Charges > 0)
            {
                HostUpgrade.State.SpendCharge();
                callback(true);
            }
            else
            {
                Messages.ShowError("No charges to spend");
                callback(false);
            }
            
        }
    }
}