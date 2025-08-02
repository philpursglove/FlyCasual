using Content;
using System.Collections;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.RZ1AWing
    {
        public class GreenSquadronPilot : RZ1AWing
        {
            public GreenSquadronPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Green Squadron Pilot",
                    "",
                    Faction.Rebel,
                    3,
                    4,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.AWing
                    },
                    seImageNumber: 21,
                    skinName: "Green",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class GreenSquadronPilotXWA : RZ1AWing
        {
            public GreenSquadronPilotXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Green Squadron Pilot",
                    "",
                    Faction.Rebel,
                    3,
                    4,
                    13,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Missile,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.AWing
                    },
                    seImageNumber: 21,
                    skinName: "Green",
                    legality: new List<Legality> { Legality.XWA }
                );
            }
        }
    }
}