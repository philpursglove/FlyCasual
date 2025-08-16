using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.Z95AF4Headhunter
    {
        public class BanditSquadronPilot : Z95AF4Headhunter
        {
            public BanditSquadronPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Bandit Squadron Pilot",
                    "",
                    Faction.Rebel,
                    1,
                    3,
                    5,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Missile,
                        UpgradeType.Modification
                    },
                    seImageNumber: 30,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class BanditSquadronPilotXWA : BanditSquadronPilot
        {
            public BanditSquadronPilotXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 12;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
