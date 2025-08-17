using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.AlphaClassStarWing
    {
        public class NuSquadronPilot : AlphaClassStarWing
        {
            public NuSquadronPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Nu Squadron Pilot",
                    "",
                    Faction.Imperial,
                    2,
                    5,
                    7,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Cannon,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    seImageNumber: 138,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class NuSquadronPilotXWA : NuSquadronPilot
        {
            public NuSquadronPilotXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 20;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
