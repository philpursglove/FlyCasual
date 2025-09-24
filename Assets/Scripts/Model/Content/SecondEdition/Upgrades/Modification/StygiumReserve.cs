using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class StygiumReserve : GenericUpgrade
    {
        public StygiumReserve()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Stygium Reserve",
                UpgradeType.Modification,
                abilityType: typeof(Abilities.SecondEdition.StygiumReserveAbility)
            );
            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class StygiumReserveAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            // Ability implementation goes here
        }
        public override void DeactivateAbility()
        {
            // Ability deactivation logic goes here
        }
    }
}