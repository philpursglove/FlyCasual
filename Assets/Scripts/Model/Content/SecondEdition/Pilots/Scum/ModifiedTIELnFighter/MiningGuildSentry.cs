using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ModifiedTIELnFighter
    {
        public class MiningGuildSentry : ModifiedTIELnFighter
        {
            public MiningGuildSentry() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Mining Guild Sentry",
                    "",
                    Faction.Scum,
                    1,
                    3,
                    3,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Illicit
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class MiningGuildSentryXWA : MiningGuildSentry
        {
            public MiningGuildSentryXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 5;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 1;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Modification,
                    UpgradeType.Modification
                };
            }
        }
    }
}
