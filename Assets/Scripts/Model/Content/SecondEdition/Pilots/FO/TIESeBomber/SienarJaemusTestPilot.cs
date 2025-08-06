
using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIESeBomber
    {
        public class SienarJeamusTestPilot : TIESeBomber
        {
            public SienarJeamusTestPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Sienar-Jaemus Test Pilot",
                    "",
                    Faction.FirstOrder,
                    2,
                    4,
                    8,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Tech,
                        UpgradeType.Tech,
                        UpgradeType.Missile,
                        UpgradeType.Device,
                        UpgradeType.Device,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality>{Legality.StandardLegal, Legality.ExtendedLegal}
                );
            }
        }
    }
}
