using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.NimbusClassVWing
    {
        public class LoyalistVolunteer : NimbusClassVWing
        {
            public LoyalistVolunteer() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Loyalist Volunteer",
                    "",
                    Faction.Republic,
                    2,
                    3,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class LoyalistVolunteerXWA : LoyalistVolunteer
        {
            public LoyalistVolunteerXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 5;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}