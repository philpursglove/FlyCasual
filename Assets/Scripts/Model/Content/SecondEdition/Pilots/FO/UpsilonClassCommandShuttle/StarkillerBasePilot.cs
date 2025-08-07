using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.UpsilonClassCommandShuttle
    {
        public class StarkillerBasePilot : UpsilonClassCommandShuttle
        {
            public StarkillerBasePilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Starkiller Base Pilot",
                    "",
                    Faction.FirstOrder,
                    2,
                    7,
                    8,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Tech,
                        UpgradeType.Tech,
                        UpgradeType.Cannon,
                        UpgradeType.Crew,
                        UpgradeType.Crew,
                        UpgradeType.Modification
                    },
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class StarkillerBasePilotXWA : StarkillerBasePilot
        {
            public StarkillerBasePilotXWA() : base()
            {
                var pilot = (PilotCardInfo25)PilotInfo;
                pilot.Legality = new List<Legality> { Legality.XWA };
                pilot.Cost = 7;
                pilot.LoadoutValue = 17;
                pilot.ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Sensor,
                    UpgradeType.Tech,
                    UpgradeType.Tech,
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Modification,
                    UpgradeType.Cannon
                };
            }
        }
    }
}
