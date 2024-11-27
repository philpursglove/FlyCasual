using Upgrade;

namespace UpgradesList.SecondEdition
{ 
    public class BoKatanKryzeRepublic : GenericUpgrade
    {
        public BoKatanKryzeRepublic()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Bo-Katan Kryze",
                UpgradeType.Crew,
                cost: 8,
                isLimited: true,
                restriction: new FactionRestriction(Faction.Republic, Faction.Separatists),
                abilityType: typeof(Abilities.SecondEdition.BoKatanKryzeRepublicSeparatistsAbility)
            );
        }
    }

    public class BoKatanKryzeSeparatists : GenericUpgrade
    {
        public BoKatanKryzeSeparatists()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Bo-Katan Kryze",
                UpgradeType.Crew,
                cost: 6,
                isLimited: true,
                restriction: new FactionRestriction(Faction.Republic, Faction.Separatists),
                abilityType: typeof(Abilities.SecondEdition.BoKatanKryzeRepublicSeparatistsAbility)
            );
        }
    }

}

namespace Abilities.SecondEdition
{
    public class BoKatanKryzeRepublicSeparatistsAbility : GenericAbility
    {
        //While you perform an attack, if you are at range 0-1 of the defender, you may reroll 1 attack die. 
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostName,
                IsAvailable,
                GetAiPriority,
                DiceModificationType.Reroll,
                1
            );
        }

        private bool IsAvailable()
        {
            return Combat.AttackStep == CombatStep.Attack && Combat.ShotInfo.Range <= 1;
        }

        private int GetAiPriority()
        {
            return 90;
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }
    }
}