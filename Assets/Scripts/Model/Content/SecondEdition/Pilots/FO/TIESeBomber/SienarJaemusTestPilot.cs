
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

        public class SienarJeamusTestPilotXWA : SienarJeamusTestPilot
        {
            public SienarJeamusTestPilotXWA() : base()
            {
                var pilot = (PilotCardInfo25)PilotInfo;
                pilot.Cost = 3;
                pilot.LoadoutValue = 6;
                pilot.Legality = new List<Legality> { Legality.XWA };
                pilot.ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Tech,
                    UpgradeType.Tech,
                    UpgradeType.Missile,
                    UpgradeType.Device,
                    UpgradeType.Device,
                    UpgradeType.Modification
                };
            }
        }
    }
}
