using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class ManualAilerons : GenericUpgrade
    {
        public ManualAilerons()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Manual Ailerons",
                UpgradeType.Modification,
                abilityType: typeof(Abilities.SecondEdition.ManualAileronsAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class ManualAileronsAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
        }

        public override void DeactivateAbility()
        {
        }
    }
}