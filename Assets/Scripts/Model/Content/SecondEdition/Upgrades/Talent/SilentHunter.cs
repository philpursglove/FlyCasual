using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class SilentHunter : GenericUpgrade
    {
        public SilentHunter() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Silent Hunter",
                UpgradeType.Talent,
                abilityType: typeof(Abilities.SecondEdition.SilentHunterAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SilentHunterAbility : GenericAbility
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