using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEBaInterceptor
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
                var pilot = (PilotInfo as PilotCardInfo25);
                pilot.Cost = 4;
                pilot.LoadoutValue = 11;
                pilot.LegalityInfo = new List<Legality> {Legality.XWA};
            }
        }
    }
}
