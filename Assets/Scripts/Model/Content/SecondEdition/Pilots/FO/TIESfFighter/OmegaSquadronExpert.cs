using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIESfFighter
    {
        public class OmegaSquadronExpert : TIESfFighter
        {
            public OmegaSquadronExpert() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Omega Squadron Expert",
                    "",
                    Faction.FirstOrder,
                    3,
                    4,
                    7,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Tech,
                        UpgradeType.Missile,
                        UpgradeType.Gunner,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class OmegaSquadronExpertXWA : OmegaSquadronExpert
        {
            public OmegaSquadronExpertXWA(): base()
            {
                var pilot = (PilotCardInfo25) PilotInfo;
                pilot.Cost = 4;
                pilot.LoadoutValue = 9;
                pilot.Legality = new List<Legality> {Legality.XWA};
                pilot.ExtraUpgrades = new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Sensor,
                    UpgradeType.Tech,
                    UpgradeType.Missile,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                };
            }
        }
    }
}
