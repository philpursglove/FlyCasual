using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.M3AInterceptor
    {
        public class CartelSpacer : M3AInterceptor
        {
            public CartelSpacer() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Cartel Spacer",
                    "",
                    Faction.Scum,
                    1,
                    3,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Modification
                    },
                    seImageNumber: 190,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class CartelSpacerXWA : CartelSpacer
        {
            public CartelSpacerXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 6;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 0;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).Limited = 3;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Illicit,
                    UpgradeType.Modification
                };
            }
        }
    }
}