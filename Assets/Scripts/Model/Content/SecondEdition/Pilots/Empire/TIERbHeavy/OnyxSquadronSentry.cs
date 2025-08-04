using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIERbHeavy
    {
        public class OnyxSquadronSentry : TIERbHeavy
        {
            public OnyxSquadronSentry() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Onyx Squadron Sentry",
                    "",
                    Faction.Imperial,
                    3,
                    5,
                    7,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Modification,
                        UpgradeType.Cannon,
                        UpgradeType.Cannon,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class OnyxSquadronSentryXWA : OnyxSquadronSentry
        {
            public OnyxSquadronSentryXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 5;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 17;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}