using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.LancerClassPursuitCraft
    {
        public class ShadowportHunter : LancerClassPursuitCraft
        {
            public ShadowportHunter() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Shadowport Hunter",
                    "",
                    Faction.Scum,
                    2,
                    6,
                    6,
                    tags: new List<Tags>
                    {
                        Tags.BountyHunter
                    },
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Illicit,
                        UpgradeType.Illicit
                    },
                    seImageNumber: 221,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class ShadowportHunterXWA : ShadowportHunter
        {
            public ShadowportHunterXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 6;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 15;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Illicit,
                    UpgradeType.Illicit,
                    UpgradeType.Modification
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}