using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTLS8KWing
    {
        public class WardenSquadronPilot : BTLS8KWing
        {
            public WardenSquadronPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Warden Squadron Pilot",
                    "",
                    Faction.Rebel,
                    2,
                    5,
                    7,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Torpedo,
                        UpgradeType.Missile,
                        UpgradeType.Gunner,
                        UpgradeType.Device,
                        UpgradeType.Device
                    },
                    seImageNumber: 64,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class WardenSquadronPilotXWA : BTLS8KWing
        {
            public WardenSquadronPilotXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Warden Squadron Pilot",
                    "",
                    Faction.Rebel,
                    2,
                    5,
                    25,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Gunner,
                        UpgradeType.Device,
                        UpgradeType.Device,
                        UpgradeType.Missile,
                        UpgradeType.Torpedo                        
                    },
                    seImageNumber: 64,
                    legality: new List<Legality>() { Legality.XWA }
                );
            }
        }
    }
}
