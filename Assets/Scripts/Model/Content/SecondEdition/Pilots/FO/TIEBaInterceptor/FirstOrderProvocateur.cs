using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIEBaInterceptor
{
    public class FirstOrderProvocateur : TIEBaInterceptor
    {
        public FirstOrderProvocateur() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "First Order Provocateur",
                "",
                Faction.FirstOrder,
                3,
                4,
                3,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Tech,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Tie
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class FirstOrderProvocateurXWA : FirstOrderProvocateur
    {
        public FirstOrderProvocateurXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 9;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 5;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Talent,
                UpgradeType.Modification,
                UpgradeType.Tech
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}