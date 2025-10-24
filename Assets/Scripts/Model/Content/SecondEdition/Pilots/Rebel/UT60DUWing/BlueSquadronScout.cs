using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.UT60DUWing
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
            (PilotInfo as PilotCardInfo25).Cost = 12;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 17;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Talent,
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