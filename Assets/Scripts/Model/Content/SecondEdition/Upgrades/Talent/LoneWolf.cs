using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class LoneWolf : GenericUpgrade
    {
        public LoneWolf() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Lone Wolf",
                UpgradeType.Talent,
                cost: 3,
                isLimited: true,
                charges: 1,
                regensCharges: true,
                abilityType: typeof(Abilities.SecondEdition.LoneWolfAbility),
                seImageNumber: 9
            );
        }        
    }
}

namespace Abilities.SecondEdition
{
    //While you defend or perform an attack, if there are no other friendly ships at range 0-2, you may spend 1 charge to reroll 1 of your dice.
    public class LoneWolfAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostUpgrade.UpgradeInfo.Name,
                IsDiceModificationAvailable,
                GetDiceModificationAiPriority,
                DiceModificationType.Reroll,
                1,
                payAbilityCost: PayAbilityCost
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private void PayAbilityCost(Action<bool> callback)
        {
            if (HostUpgrade.State.Charges > 0)
            {
                HostUpgrade.State.SpendCharge();
                callback(true);
            }
            else callback(false);
        }

        public bool IsDiceModificationAvailable()
        {
            bool noFriendlyShipsInRange0to2 = true;

            foreach (KeyValuePair<string, GenericShip> friendlyShip in HostShip.Owner.Ships)
            {
                if (friendlyShip.Value != HostShip)
                {
                    BoardTools.DistanceInfo distanceInfo = new BoardTools.DistanceInfo(HostShip, friendlyShip.Value);
                    if (distanceInfo.Range < 3)
                    {
                        noFriendlyShipsInRange0to2 = false;
                        break;
                    }
                }
            }

            bool allDiceRerolled = Combat.CurrentDiceRoll.DiceRerolled.Count == Combat.CurrentDiceRoll.DiceList.Count;

            return ((HostShip.IsAttacking || HostShip.IsDefending) && noFriendlyShipsInRange0to2 && HostUpgrade.State.Charges > 0 && !allDiceRerolled);
        }

        public int GetDiceModificationAiPriority()
        {
            if (Combat.AttackStep == CombatStep.Attack)
            {
                return 80;
            }

            if (Combat.AttackStep == CombatStep.Defence)
            {
                return 85;
            }

            else return 0;
        }
    }
}