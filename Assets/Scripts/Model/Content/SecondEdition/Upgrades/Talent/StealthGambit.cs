using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class StealthGambit : GenericUpgrade
    {
        public StealthGambit() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Stealth Gambit",
                UpgradeType.Talent,
                abilityType: typeof(Abilities.SecondEdition.StealthGambitAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class StealthGambitAbility : GenericAbility
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