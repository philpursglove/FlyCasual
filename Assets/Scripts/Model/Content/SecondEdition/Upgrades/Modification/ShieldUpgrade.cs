using Content;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class ShieldUpgrade : GenericUpgrade
    {
        public ShieldUpgrade() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Shield Upgrade",
                UpgradeType.Modification,
                cost: 8,
                addShields: 1,
                seImageNumber: 75,
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class ShieldUpgradeXWA : ShieldUpgrade
    {
        public ShieldUpgradeXWA() : base()
        {
            UpgradeInfo.Cost = 10;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}