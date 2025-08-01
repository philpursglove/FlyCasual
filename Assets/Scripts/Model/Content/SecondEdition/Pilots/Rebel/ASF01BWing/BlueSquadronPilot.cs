using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ASF01BWing
    {
        public class BlueSquadronPilot : ASF01BWing
        {
            public BlueSquadronPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Blue Squadron Pilot",
                    "",
                    Faction.Rebel,
                    2,
                    4,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Cannon,
                        UpgradeType.Cannon,
                        UpgradeType.Device,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.BWing
                    },
                    seImageNumber: 26,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class BlueSquadronPilotXWA : ASF01BWing
        {
            public BlueSquadronPilotXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Blue Squadron Pilot",
                    "",
                    Faction.Rebel,
                    2,
                    4,
                    8,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Device,
                        UpgradeType.Cannon,
                        UpgradeType.Cannon,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.BWing
                    },
                    seImageNumber: 26,
                    legality: new List<Legality> { Legality.XWA }
                );
            }
        }
    }
}
