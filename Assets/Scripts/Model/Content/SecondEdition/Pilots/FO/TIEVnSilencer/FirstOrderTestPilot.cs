using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEVnSilencer
    {
        public class FirstOrderTestPilot : TIEVnSilencer
        {
            public FirstOrderTestPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "First Order Test Pilot",
                    "",
                    Faction.FirstOrder,
                    4,
                    5,
                    5,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Tech,
                        UpgradeType.Torpedo,
                        UpgradeType.Missile,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class FirstOrderTestPilotXWA : FirstOrderTestPilot
        {
            public FirstOrderTestPilotXWA(): base()
            {
                var pilot = (PilotCardInfo25)PilotInfo;
                pilot.Cost = 5;
                pilot.LoadoutValue = 9;
                pilot.LegalityInfo = new List<Legality> { Legality.XWA };
                pilot.ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Tech,
                    UpgradeType.Missile,
                    UpgradeType.Torpedo,
                    UpgradeType.Configuration
                };
            }
        }
    }
}
