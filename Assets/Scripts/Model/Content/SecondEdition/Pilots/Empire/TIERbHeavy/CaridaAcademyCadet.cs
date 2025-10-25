using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIERbHeavy
    {
        public class CaridaAcademyCadet : TIERbHeavy
        {
            public CaridaAcademyCadet() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Carida Academy Cadet",
                    "",
                    Faction.Imperial,
                    1,
                    5,
                    8,
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Cannon,
                        UpgradeType.Cannon,
                        UpgradeType.Configuration
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class CaridaAcademyCadetXWA : CaridaAcademyCadet
        {
            public CaridaAcademyCadetXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 9;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Cannon,
                    UpgradeType.Cannon,
                    UpgradeType.Configuration
                };
            }
        }
    }
}