using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ASF01BWing
    {
        public class BladeSquadronVeteran : ASF01BWing
        {
            public BladeSquadronVeteran() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Blade Squadron Veteran",
                    "",
                    Faction.Rebel,
                    3,
                    4,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Cannon,
                        UpgradeType.Cannon,
                        UpgradeType.Torpedo,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.BWing
                    },
                    seImageNumber: 25,
                    skinName: "Blue",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class BladeSquadronVeteranXWA : ASF01BWing
        {
            public BladeSquadronVeteranXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Blade Squadron Veteran",
                    "",
                    Faction.Rebel,
                    3,
                    5,
                    17,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Cannon,
                        UpgradeType.Cannon,
                        UpgradeType.Torpedo,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.BWing
                    },
                    seImageNumber: 25,
                    skinName: "Blue",
                    legality: new List<Legality> { Legality.XWA }
                );
            }
        }
    }
}
