using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.KihraxzFighter
    {
        public class CartelMarauder : KihraxzFighter
        {
            public CartelMarauder() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Cartel Marauder",
                    "",
                    Faction.Scum,
                    2,
                    4,
                    5,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Illicit
                    },
                    seImageNumber: 196,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class CartelMarauderXWA : CartelMarauder
        {
            public CartelMarauderXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 5;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 15;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Missile
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
