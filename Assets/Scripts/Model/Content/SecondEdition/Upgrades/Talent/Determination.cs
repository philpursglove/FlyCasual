using Abilities.SecondEdition;
using Ship;
using System;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class Determination : GenericUpgrade
    {
        public Determination() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                name: "Determination",
                type: UpgradeType.Talent,
                cost: 0,
                abilityType: typeof(DeterminationAbility)
            );

            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    // While you perform a primary attack, if the defender is in your bullseye, you may suffer 1 damage to add 1 focus result.

    public class DeterminationAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                name: HostUpgrade.UpgradeInfo.Name,
                isAvailable: IsAvailable,
                aiPriority: AiPriority,
                modificationType: DiceModificationType.Add,
                count: 1,
                payAbilityCost: PayCost,
                sideCanBeChangedTo: DieSide.Focus
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private void PayCost(Action<bool> callback)
        {
            Messages.ShowInfo($"{HostUpgrade.UpgradeInfo.Name}: {HostShip.PilotInfo.PilotName} ship suffered 1 damage.");

            DamageSourceEventArgs determinationDamage = new ()
            {
                Source = "Determination Upgrade Ability",
                DamageType = DamageTypes.CardAbility
            };

            HostShip.Damage.TryResolveDamage(1, determinationDamage, delegate { callback(true); });
        }

        private int AiPriority()
        {            
            return 0;
        }

        private bool IsAvailable()
        {
            return Combat.AttackStep == CombatStep.Attack
                && Combat.ChosenWeapon is PrimaryWeaponClass
                && HostShip.SectorsInfo.IsShipInSector(Combat.Defender, Arcs.ArcType.Bullseye);
        }
    }
}