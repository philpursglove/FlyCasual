using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTANR2YWing
    {
        public class KijimiSpiceRunner : BTANR2YWing
        {
            public KijimiSpiceRunner() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Kijimi Spice Runner",
                    "",
                    Faction.Resistance,
                    2,
                    4,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Astromech,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Tech,
                        UpgradeType.Device,
                        UpgradeType.Turret,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.YWing
                    },
                    skinName: "Red",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class KijimiSpiceRunnerXWA : KijimiSpiceRunner
        {
            public KijimiSpiceRunnerXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 10;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Astromech,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Tech,
                    UpgradeType.Device,
                    UpgradeType.Turret
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
