using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.T70XWing
    {
        public class BlackSquadronAce : T70XWing
        {
            public BlackSquadronAce() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Black Squadron Ace",
                    "",
                    Faction.Resistance,
                    4,
                    5,
                    10,
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
                    skinName: "Black One",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "blacksquadronace-t70xwing";
            }
        }

        public class BlackSquadronAceXWA : BlackSquadronAce
        {
            public BlackSquadronAceXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 5;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 15;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}