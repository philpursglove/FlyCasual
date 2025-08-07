using Content;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class HullUpgrade : GenericUpgrade
    {
        public HullUpgrade() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Hull Upgrade",
                UpgradeType.Modification,
                cost: 6,
                addHull: 1,
                seImageNumber: 73,
                legalityInfo: new List<Legality>
                {
                    Legality.StandardBanned,
                    Legality.ExtendedLegal
                }
            );
        }
    }

    public class HullUpgradeXWA : HullUpgrade
    {
        public HullUpgradeXWA() : base()
        {
            UpgradeInfo.Cost = 9;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}