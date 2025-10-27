using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.VultureClassDroidFighter
{
    public class SeparatistDrone : VultureClassDroidFighter
    {
        public SeparatistDrone()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Separatist Drone",
                "",
                Faction.Separatists,
                3,
                2,
                3,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Missile,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Droid
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class SeparatistDroneXWA : SeparatistDrone
    {
        public SeparatistDroneXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 6;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 6;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Modification,
                UpgradeType.Missile,
                UpgradeType.Configuration
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}