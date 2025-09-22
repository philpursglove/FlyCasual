using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class RelaySystem : GenericUpgrade
    {
        public RelaySystem()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Relay System",
                UpgradeType.Sensor,
                abilityType: typeof(Abilities.SecondEdition.RelaySystemAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class RelaySystemAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
        }
        public override void DeactivateAbility()
        {
        }
    }
}