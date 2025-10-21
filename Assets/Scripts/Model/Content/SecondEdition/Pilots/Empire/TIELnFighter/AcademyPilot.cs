using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIELnFighter
    {
        public class AcademyPilot : TIELnFighter
        {
            public AcademyPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Academy Pilot",
                    "",
                    Faction.Imperial,
                    1,
                    2,
                    0,
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    seImageNumber: 92,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class AcademyPilotXWA : AcademyPilot
        {
            public AcademyPilotXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 5;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 0;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>();
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
