using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.ASF01BWing
{
    public class BladeSquadronVeteran : ASF01BWing
    {
        public BladeSquadronVeteran() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Blade Squadron Veteran",
                "",
                Faction.Rebel,
                3,
                4,
                4,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Sensor,
                    UpgradeType.Cannon,
                    UpgradeType.Cannon,
                    UpgradeType.Torpedo,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.BWing
                },
                seImageNumber: 25,
                skinName: "Blue",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class BladeSquadronVeteranXWA : BladeSquadronVeteran
    {
        public BladeSquadronVeteranXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 10;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Sensor,
                UpgradeType.Modification,
                UpgradeType.Cannon,
                UpgradeType.Cannon,
                UpgradeType.Missile,
                UpgradeType.Torpedo,
                UpgradeType.Configuration
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}
