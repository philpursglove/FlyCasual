using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class Intimidation : GenericUpgrade
    {
        public Intimidation() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Intimidation",
                UpgradeType.Talent,
                cost: 3,
                abilityType: typeof(Abilities.SecondEdition.IntimidationAbility),
                seImageNumber: 7,
                legalityInfo: new List<Legality>
                {
                    Legality.StandardBanned,
                    Legality.ExtendedLegal
                }
            );
        }        
    }
}

namespace Abilities.SecondEdition
{
    //When you are touching an enemy ship, reduce that ship's agility value by 1.
    public class IntimidationAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackStartAsAttacker += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsAttacker -= CheckAbility;
        }

        private void CheckAbility()
        {
            if (HostShip.ShipsBumped.Contains(Combat.Defender))
            {
                Combat.Defender.AfterGotNumberOfDefenceDice += RegisterIntimidated;
            }
        }

        private void RegisterIntimidated(ref int count)
        {
            Combat.Defender.AfterGotNumberOfDefenceDice -= RegisterIntimidated;
            Messages.ShowInfo(HostUpgrade.UpgradeInfo.Name + " on a ship at range 0 causes " + Combat.Defender.PilotInfo.PilotName + " to roll 1 fewer defense die");
            count--;
        }
    }
}