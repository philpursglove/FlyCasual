using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.CustomizedYT1300LightFreighter
    {
        public class FreighterCaptain : CustomizedYT1300LightFreighter
        {
            public FreighterCaptain() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Freighter Captain",
                    "",
                    Faction.Scum,
                    1,
                    5,
                    6,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Missile,
                        UpgradeType.Gunner,
                        UpgradeType.Illicit
                    },
                    tags: new List<Tags>
                    {
                        Tags.Freighter,
                        Tags.YT1300
                    },
                    seImageNumber: 225,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class FreighterCaptainXWA : FreighterCaptain
        {
            public FreighterCaptainXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 11;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Modification
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).Limited = 1;
            }
        }
    }
}
