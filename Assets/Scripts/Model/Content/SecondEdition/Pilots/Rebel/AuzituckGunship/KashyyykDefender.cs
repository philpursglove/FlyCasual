using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.AuzituckGunship
    {
        public class KashyyykDefender : AuzituckGunship
        {
            public KashyyykDefender() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Kashyyyk Defender",
                    "",
                    Faction.Rebel,
                    1,
                    5,
                    6,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Crew,
                        UpgradeType.Modification
                    },
                    seImageNumber: 33,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class KashyyykDefenderXWA : AuzituckGunship
        {
            public KashyyykDefenderXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Kashyyyk Defender",
                    "",
                    Faction.Rebel,
                    1,
                    5,
                    16,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Crew,
                        UpgradeType.Crew,
                        UpgradeType.Modification
                    },
                    seImageNumber: 33,
                    legality: new List<Legality>() { Legality.XWA }
                );
            }
        }
    }
}
