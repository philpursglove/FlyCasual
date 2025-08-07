using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIESfFighter
    {
        public class ZetaSquadronSurvivor : TIESfFighter
        {
            public ZetaSquadronSurvivor() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Zeta Squadron Survivor",
                    "",
                    Faction.FirstOrder,
                    2,
                    4,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Sensor,
                        UpgradeType.Tech,
                        UpgradeType.Gunner
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class ZetaSquadronSurvivorXWA : ZetaSquadronSurvivor
        {
            public ZetaSquadronSurvivorXWA() : base()
            {
                var pilot = (PilotCardInfo25)PilotInfo;
                pilot.Cost = 4;
                pilot.LoadoutValue = 11;
                pilot.Legality = new List<Legality> { Legality.XWA };
                pilot.ExtraUpgrades = new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Sensor,
                    UpgradeType.Tech,
                    UpgradeType.Gunner
                };
            }
        }
    }
}
