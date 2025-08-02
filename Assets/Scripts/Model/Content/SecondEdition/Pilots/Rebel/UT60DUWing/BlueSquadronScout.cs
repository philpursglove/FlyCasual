using Content;
using Ship.SecondEdition.T65XWing;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.UT60DUWing
    {
        public class BlueSquadronScout : UT60DUWing
        {
            public BlueSquadronScout() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Blue Squadron Scout",
                    "",
                    Faction.Rebel,
                    2,
                    5,
                    6,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    seImageNumber: 60,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class BlueSquadronScoutXWA : BlueSquadronScout
        {
            public BlueSquadronScoutXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 5;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 22;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                { 
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Sensor,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
