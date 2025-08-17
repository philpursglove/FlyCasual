using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.Fireball
    {
        public class ColossusStationMechanic : Fireball
        {
            public ColossusStationMechanic() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Colossus Station Mechanic",
                    "",
                    Faction.Resistance,
                    2,
                    3,
                    5,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Modification,
                        UpgradeType.Missile                        
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class ColossusStationMechanicXWA : ColossusStationMechanic
        {
            public ColossusStationMechanicXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 6;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}