using Arcs;
using Content;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class ProtonRockets : GenericSpecialWeapon
    {
        public ProtonRockets() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Proton Rockets",
                UpgradeType.Missile,
                cost: 8,
                weaponInfo: new SpecialWeaponInfo(
                    attackValue: 5,
                    minRange: 1,
                    maxRange: 2,
                    requiresToken: typeof(FocusToken),
                    charges: 1,
                    arc: ArcType.Bullseye
                ),
                seImageNumber: 41,
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class ProtonRocketsXWA : ProtonRockets
    {
        public ProtonRocketsXWA() : base()
        {
            UpgradeInfo.Cost = 7;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
            UpgradeInfo.Limited = 3;
        }
    }
}