using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.ResistanceTransport
{
    public class LogisticsDivisionPilot : ResistanceTransport
    {
        public LogisticsDivisionPilot()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Logistics Division Pilot",
                "",
                Faction.Resistance,
                1,
                4,
                6,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Astromech,
                    UpgradeType.Astromech,
                    UpgradeType.Crew,
                    UpgradeType.Cannon
                },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
        }
    }

    public class LogisticsDivisionPilotXWA : LogisticsDivisionPilot
    {
        public LogisticsDivisionPilotXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 4;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 18;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}