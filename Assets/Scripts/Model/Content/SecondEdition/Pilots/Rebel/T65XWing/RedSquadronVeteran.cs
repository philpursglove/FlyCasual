using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.T65XWing
    {
        public class RedSquadronVeteran : T65XWing
        {
            public RedSquadronVeteran() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Red Squadron Veteran",
                    "",
                    Faction.Rebel,
                    3,
                    5,
                    3,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Astromech,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.XWing
                    },
                    seImageNumber: 10,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class RedSquadronVeteranXWA : RedSquadronVeteran
        {
            public RedSquadronVeteranXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
