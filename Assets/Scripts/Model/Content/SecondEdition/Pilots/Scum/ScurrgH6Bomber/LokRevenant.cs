using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ScurrgH6Bomber
    {
        public class LokRevenant : ScurrgH6Bomber
        {
            public LokRevenant() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Lok Revenant",
                    "",
                    Faction.Scum,
                    2,
                    5,
                    8,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Turret,
                        UpgradeType.Gunner,
                        UpgradeType.Device,
                        UpgradeType.Device
                    },
                    seImageNumber: 206,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class LokRevenantXWA : LokRevenant
        {
            public LokRevenantXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 6;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 25;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Device,
                    UpgradeType.Device,
                    UpgradeType.Turret
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}