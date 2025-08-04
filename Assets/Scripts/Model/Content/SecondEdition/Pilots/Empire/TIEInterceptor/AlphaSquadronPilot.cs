using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEInterceptor
    {
        public class AlphaSquadronPilot : TIEInterceptor
        {
            public AlphaSquadronPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Alpha Squadron Pilot",
                    "",
                    Faction.Imperial,
                    1,
                    4,
                    2,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    seImageNumber: 106,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class AlphaSquadronPilotXWA : AlphaSquadronPilot
        {
            public AlphaSquadronPilotXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 0;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}