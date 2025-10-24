using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.BTLS8KWing
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

    public class WardenSquadronPilotXWA : WardenSquadronPilot
    {
        public WardenSquadronPilotXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 11;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 12;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Crew,
                UpgradeType.Gunner,
                UpgradeType.Modification,
                UpgradeType.Device,
                UpgradeType.Device,
                UpgradeType.Missile,
                UpgradeType.Missile,
                UpgradeType.Torpedo
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}