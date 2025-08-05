using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.T70XWing
    {
        public class RedSquadronExpert : T70XWing
        {
            public RedSquadronExpert() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Red Squadron Expert",
                    "",
                    Faction.Resistance,
                    3,
                    5,
                    2,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Tech,
                        UpgradeType.Astromech,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.XWing
                    },
                    skinName: "Red",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class RedSquadronExpertXWA : RedSquadronExpert
        {
            public RedSquadronExpertXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 5;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
