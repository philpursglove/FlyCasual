using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ModifiedTIELnFighter
    {
        public class MiningGuildSurveyor : ModifiedTIELnFighter
        {
            public MiningGuildSurveyor() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Mining Guild Surveyor",
                    "",
                    Faction.Scum,
                    2,
                    3,
                    1,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class MiningGuildSurveyorXWA : MiningGuildSurveyor
        {
            public MiningGuildSurveyorXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 6;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 6;
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
