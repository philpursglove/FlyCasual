using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class R2A3BoY : GenericUpgrade
    {
        public R2A3BoY() : base()
        {
            IsHidden = true;

            UpgradeInfo = new UpgradeCardInfo(
                "R2-A3",
                UpgradeType.Astromech,
                cost: 0,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.R2AstromechBoYAbility),
                charges: 2
            );
            NameCanonical = "r2a3-battleofyavin";
        }
    }
}