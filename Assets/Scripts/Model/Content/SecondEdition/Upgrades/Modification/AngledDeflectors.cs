using Actions;
using ActionsList;
using Content;
using Ship;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class AngledDeflectors : GenericUpgrade
    {
        public AngledDeflectors() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Angled Deflectors",
                UpgradeType.Modification,
                cost: 4,
                restrictions: new UpgradeCardRestrictions(
                    new BaseSizeRestriction(BaseSize.Small, BaseSize.Medium), 
                    new StatValueRestriction(
                        StatValueRestriction.Stats.Shields,
                        StatValueRestriction.Conditions.HigherThanOrEqual,
                        1
                    )
                ),
                addAction: new ActionInfo(typeof(ReinforceAction)),
                addShields: -1,
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class AngledDeflectorsXWA : AngledDeflectors
    {
        public AngledDeflectorsXWA() : base()
        {
            UpgradeInfo.Cost = 1;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}