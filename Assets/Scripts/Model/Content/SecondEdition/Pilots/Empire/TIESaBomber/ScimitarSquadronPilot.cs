using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIESaBomber
    {
        public class ScimitarSquadronPilot : TIESaBomber
        {
            public ScimitarSquadronPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Scimitar Squadron Pilot",
                    "",
                    Faction.Imperial,
                    2,
                    4,
                    6,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Gunner,
                        UpgradeType.Modification,
                        UpgradeType.Device,
                        UpgradeType.Device,
                        UpgradeType.Missile
                    },
                    seImageNumber: 112,
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class ScimitarSquadronPilotXWA : ScimitarSquadronPilot
        {
            public ScimitarSquadronPilotXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 12;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
