using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.CloneZ95Headhunter
    {
        public class ReaperSquadronScout : CloneZ95Headhunter
        {
            public ReaperSquadronScout() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Reaper Squadron Scout",
                    "",
                    Faction.Republic,
                    3,
                    3,
                    2,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Sensor
                    },
                    tags: new List<Tags>
                    {
                        Tags.Clone
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class ReaperSquadronScoutXWA : ReaperSquadronScout
        {
            public ReaperSquadronScoutXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 8;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 11;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Sensor,
                    UpgradeType.Modification
                };
            }
        }
    }
}