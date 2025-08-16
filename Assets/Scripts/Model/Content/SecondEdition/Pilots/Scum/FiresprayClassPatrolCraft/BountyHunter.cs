using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.FiresprayClassPatrolCraft
    {
        public class BountyHunter : FiresprayClassPatrolCraft
        {
            public BountyHunter() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Bounty Hunter",
                    "",
                    Faction.Scum,
                    2,
                    7,
                    10,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Cannon,
                        UpgradeType.Missile,
                        UpgradeType.Device,
                        UpgradeType.Illicit
                    },
                    tags: new List<Tags>
                    {
                        Tags.BountyHunter
                    },
                    seImageNumber: 154,
                    skinName: "Mandalorian Mercenary",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class BountyHunterXWA : BountyHunter
        {
            public BountyHunterXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 6;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 16;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                        UpgradeType.Gunner,
                        UpgradeType.Illicit,
                        UpgradeType.Device,
                        UpgradeType.Cannon,
                        UpgradeType.Missile
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }

    }
}
