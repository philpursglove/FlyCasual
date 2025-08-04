using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEAdvancedV1
    {
        public class BaronOfTheEmpire : TIEAdvancedV1
        {
            public BaronOfTheEmpire() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Baron of the Empire",
                    "",
                    Faction.Imperial,
                    3,
                    4,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Sensor,
                        UpgradeType.Missile
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    seImageNumber: 101,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class BaronOfTheEmpireXWA : BaronOfTheEmpire
        {
            public BaronOfTheEmpireXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 5;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Sensor,
                    UpgradeType.Modification,
                    UpgradeType.Missile
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}