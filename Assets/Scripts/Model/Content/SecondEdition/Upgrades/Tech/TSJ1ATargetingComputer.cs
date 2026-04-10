using Abilities.SecondEdition;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class TSJ1ATargetingComputer : GenericUpgrade
    {
        public TSJ1ATargetingComputer() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "T-SJ1A Targeting Computer",
                UpgradeType.Tech,
                cost: 1,
                charges: 1, // TODO: Update points
                abilityType: typeof(TSJ1ATargetingComputerAbility),
                restriction: new FactionRestriction(Faction.FirstOrder),
                legalityInfo: new List<Legality>() { Legality.XWA }
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class TSJ1ATargetingComputerAbility : GenericAbility
    {
        // While you perform a primary attack, if the defender does not have any green tokens,
        // you may spend 1 charge and 1 crit result. If you do, add 2 hit results.
        public override void ActivateAbility()
        {
            AddDiceModification(
                name: HostUpgrade.UpgradeInfo.Name,
                isAvailable: IsDiceModificationAvailable,
                aiPriority: GetAiPriority,
                modificationType: DiceModificationType.Add,
                count: 2,
                sidesCanBeSelected: new List<DieSide>() { DieSide.Success },
                payAbilityCost: PayCost
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private bool IsDiceModificationAvailable()
        {
            return Combat.Attacker == HostShip &&
                HostUpgrade.State.Charges > 0 &&
                Combat.ChosenWeapon is PrimaryWeaponClass && 
                !Combat.Defender.Tokens.HasGreenTokens &&
                Combat.DiceRollAttack.CriticalSuccesses > 0;
        }

        private int GetAiPriority()
        {
            // Priority given to more hits if it will knock out shields
            if (Combat.Defender.State.ShieldsCurrent >= (Combat.DiceRollAttack.RegularSuccesses + 1)) return 100;

            // Extra hits will help ensure opponent is destroyed by attack
            if ((Combat.Defender.State.HullCurrent + Combat.Defender.State.ShieldsCurrent) <= Combat.DiceRollAttack.Successes) return 50;

            return 0;
        }

        private void PayCost(Action<bool> callback)
        {
            HostUpgrade.State.SpendCharge();
            Combat.DiceRollAttack.RemoveType(DieSide.Crit);

            callback(true);
        }
    }
}