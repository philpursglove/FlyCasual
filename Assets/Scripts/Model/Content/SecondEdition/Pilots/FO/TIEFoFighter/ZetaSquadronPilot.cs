using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEFoFighter
    {
        public class ZetaSquadronPilot : TIEFoFighter
        {
            public ZetaSquadronPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Zeta Squadron Pilot",
                    "",
                    Faction.FirstOrder,
                    2,
                    3,
                    3,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Tech,
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

        public class ZetaSquadronPilotXWA : ZetaSquadronPilot
        {
            public ZetaSquadronPilotXWA(): base()
            {
                var pilot = (PilotCardInfo25) PilotInfo;
                pilot.LegalityInfo = new List<Legality> {Legality.XWA};
                pilot.Cost = 3;
                pilot.LoadoutValue = 12;
                pilot.ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Sensor,
                    UpgradeType.Tech,
                    UpgradeType.Modification
                };
            }
        }
    }
}
