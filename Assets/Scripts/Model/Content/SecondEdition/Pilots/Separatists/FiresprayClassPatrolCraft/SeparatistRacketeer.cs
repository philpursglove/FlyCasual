using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.FiresprayClassPatrolCraft
{
    public class SeparatistRacketeer : FiresprayClassPatrolCraft
    {
        public SeparatistRacketeer() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Separatist Racketeer",
                "",
                Faction.Separatists,
                2,
                7,
                10,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Cannon,
                    UpgradeType.Missile,
                    UpgradeType.Device
                },
                skinName: "Jango Fett",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class SeparatistRacketeerXWA : SeparatistRacketeer
    {
        public SeparatistRacketeerXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 15;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Crew,
                UpgradeType.Illicit,
                UpgradeType.Modification,
                UpgradeType.Device,
                UpgradeType.Cannon,
                UpgradeType.Missile,
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}
