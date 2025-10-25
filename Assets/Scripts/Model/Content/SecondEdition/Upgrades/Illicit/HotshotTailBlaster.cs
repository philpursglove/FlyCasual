using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class HotshotTailBlaster : GenericSpecialWeapon
    {
        public HotshotTailBlaster() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Hotshot Tail Blaster",
                UpgradeType.Illicit,
                cost: 2,
                restriction: new BaseSizeRestriction(BaseSize.Medium, BaseSize.Large),
                weaponInfo: new SpecialWeaponInfo(
                    attackValue: 2,
                    minRange: 0,
                    maxRange: 1,
                    arc: Arcs.ArcType.Rear,
                    charges: 2
                ),
                legalityInfo: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class HotshotTailBlasterXWA : HotshotTailBlaster
    {
        public HotshotTailBlasterXWA() : base()
        {
            UpgradeInfo.Cost = 1;
            UpgradeInfo.LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}