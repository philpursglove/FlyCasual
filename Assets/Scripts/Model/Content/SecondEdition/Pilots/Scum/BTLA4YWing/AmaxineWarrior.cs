using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTLA4YWing
    {
        public class AmaxineWarrior : BTLA4YWing
        {
            public AmaxineWarrior() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Amaxine Warrior",
                    "",
                    Faction.Scum,
                    3,
                    4,
                    6,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Turret,
                        UpgradeType.Torpedo,
                        UpgradeType.Missile,
                        UpgradeType.Astromech,
                        UpgradeType.Device
                    },
                    tags: new List<Tags>
                    {
                        Tags.YWing
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class AmaxineWarriorXWA : AmaxineWarrior
        {
            public AmaxineWarriorXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 16;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                    UpgradeType.Device,
                    UpgradeType.Turret,
                    UpgradeType.Torpedo,
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
