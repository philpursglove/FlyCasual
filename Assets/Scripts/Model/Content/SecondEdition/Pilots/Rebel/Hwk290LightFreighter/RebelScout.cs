using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.Hwk290LightFreighter
    {
        public class RebelScout : Hwk290LightFreighter
        {
            public RebelScout() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Rebel Scout",
                    "",
                    Faction.Rebel,
                    2,
                    4,
                    6,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Device,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Freighter
                    },
                    seImageNumber: 45,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class RebelScoutXWA : Hwk290LightFreighter
        {
            public RebelScoutXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Rebel Scout",
                    "",
                    Faction.Rebel,
                    2,
                    3,
                    7,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Modification,
                        UpgradeType.Device
                    },
                    tags: new List<Tags>
                    {
                        Tags.Freighter
                    },
                    seImageNumber: 45,
                    legality: new List<Legality> { Legality.XWA }
                );
            }
        }
    }
}
