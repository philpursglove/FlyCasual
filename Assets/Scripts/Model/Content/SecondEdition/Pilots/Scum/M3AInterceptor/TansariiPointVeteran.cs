using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.M3AInterceptor
    {
        public class TansariiPointVeteran : M3AInterceptor
        {
            public TansariiPointVeteran() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Tansarii Point Veteran",
                    "",
                    Faction.Scum,
                    3,
                    3,
                    3,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Modification
                    },
                    seImageNumber: 189,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class TansariiPointVeteranXWA : TansariiPointVeteran
        {
            public TansariiPointVeteranXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 8;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 10;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Illicit,
                    UpgradeType.Modification
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}