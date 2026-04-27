using Content;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class AdaptivePowerSystems : GenericUpgrade
    {
        public AdaptivePowerSystems() : base()
        {
            UpgradeInfo = new UpgradeCardInfo
            (
                "Adaptive Power Systems",
                UpgradeType.Modification,
                charges: 2,
                restriction: new TagRestriction(Tags.Mandalorian),
                abilityType: typeof(Abilities.SecondEdition.AdaptivePowerSystemsAbility),
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AdaptivePowerSystemsAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
        }
        public override void DeactivateAbility()
        {
        }
    }
}