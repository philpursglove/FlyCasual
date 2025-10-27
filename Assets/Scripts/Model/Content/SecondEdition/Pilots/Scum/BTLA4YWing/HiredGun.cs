using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTLA4YWing
    {
        public class HiredGun : BTLA4YWing
        {
            public HiredGun() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Hired Gun",
                    "",
                    Faction.Scum,
                    2,
                    4,
                    6,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Illicit,
                        UpgradeType.Device,
                        UpgradeType.Turret,
                        UpgradeType.Missile,
                        UpgradeType.Torpedo
                    },
                    tags: new List<Tags>
                    {
                        Tags.YWing
                    },
                    seImageNumber: 167,
                    skinName: "Gray",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class HiredGunXWA : HiredGun
        {
            public HiredGunXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 9;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 12;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Astromech,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Device,
                    UpgradeType.Turret,
                    UpgradeType.Missile,
                };
            }
        }
    }
}
