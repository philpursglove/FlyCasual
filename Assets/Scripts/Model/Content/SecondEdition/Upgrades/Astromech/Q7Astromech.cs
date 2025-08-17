using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class Q7Astromech : GenericUpgrade
    {
        public Q7Astromech() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Q7 Astromech",
                UpgradeType.Astromech,
                cost: 1,
                abilityType: typeof(Abilities.SecondEdition.Q7AstromechAbility),
                restriction: new FactionRestriction(Faction.Republic)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class Q7AstromechAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnCheckIgnoreObstaclesDuringBarrelRoll += Allow;
            HostShip.OnCheckIgnoreObstaclesDuringBoost += Allow;
        }

        private void Allow(ref bool isAllowed)
        {
            isAllowed = true;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnCheckIgnoreObstaclesDuringBarrelRoll -= Allow;
            HostShip.OnCheckIgnoreObstaclesDuringBoost -= Allow;
        }
    }
}