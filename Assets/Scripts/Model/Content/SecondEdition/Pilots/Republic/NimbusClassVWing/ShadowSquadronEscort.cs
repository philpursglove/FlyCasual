using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.NimbusClassVWing
    {
        public class ShadowSquadronEscort : NimbusClassVWing
        {
            public ShadowSquadronEscort() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Shadow Squadron Escort",
                    "",
                    Faction.Republic,
                    3,
                    3,
                    3,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.Clone
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class ShadowSquadronEscortXWA : ShadowSquadronEscort
        {
            public ShadowSquadronEscortXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 4;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}