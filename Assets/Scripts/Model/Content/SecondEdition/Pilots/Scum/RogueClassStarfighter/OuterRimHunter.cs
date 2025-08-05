using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.RogueClassStarfighter
    {
        public class OuterRimHunter : RogueClassStarfighter
        {
            public OuterRimHunter() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Outer Rim Hunter",
                    "",
                    Faction.Scum,
                    3,
                    4,
                    5,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Cannon,
                        UpgradeType.Cannon                        
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class OuterRimHunterXWA : OuterRimHunter
        {
            public OuterRimHunterXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 15;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}