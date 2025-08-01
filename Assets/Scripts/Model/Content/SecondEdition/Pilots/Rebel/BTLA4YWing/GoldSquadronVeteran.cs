using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTLA4YWing
    {
        public class GoldSquadronVeteran : BTLA4YWing
        {
            public GoldSquadronVeteran() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Gold Squadron Veteran",
                    "",
                    Faction.Rebel,
                    3,
                    4,
                    6,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Turret,
                        UpgradeType.Missile,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.YWing
                    },
                    seImageNumber: 17,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class GoldSquadronVeteranXWA : BTLA4YWing
        {
            public GoldSquadronVeteranXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Gold Squadron Veteran",
                    "",
                    Faction.Rebel,
                    3,
                    3,
                    8,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Modification,
                        UpgradeType.Turret,
                        UpgradeType.Missile
                    },
                    tags: new List<Tags>
                    {
                        Tags.YWing
                    },
                    seImageNumber: 17,
                    legality: new List<Legality> { Legality.XWA }
                );
            }
        }
    }
}
