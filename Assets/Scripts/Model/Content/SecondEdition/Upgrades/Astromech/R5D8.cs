using Content;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class R5D8 : GenericUpgrade
    {
        public R5D8() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "R5-D8",
                UpgradeType.Astromech,
                cost: 6,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.R5AstromechAbility),
                restriction: new FactionRestriction(Faction.Rebel),
                charges: 3,
                seImageNumber: 101,
                legalityInfo: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class R5D8XWA : R5D8
    {
        public R5D8XWA() : base()
        {
            UpgradeInfo.Cost = 7;
            UpgradeInfo.LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}