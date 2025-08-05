using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTLBYWing
    {
        public class ShadowSquadronVeteran : BTLBYWing
        {
            public ShadowSquadronVeteran() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Shadow Squadron Veteran",
                    "",
                    Faction.Republic,
                    3,
                    4,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Turret,
                        UpgradeType.Torpedo,
                        UpgradeType.Gunner,
                        UpgradeType.Astromech,
                        UpgradeType.Device,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Clone,
                        Tags.YWing
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class ShadowSquadronVeteranXWA : ShadowSquadronVeteran
        {
            public ShadowSquadronVeteranXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                    UpgradeType.Device,
                    UpgradeType.Turret,
                    UpgradeType.Torpedo
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
