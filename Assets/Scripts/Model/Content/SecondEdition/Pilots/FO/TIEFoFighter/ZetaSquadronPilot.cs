using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIEFoFighter
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
        public ZetaSquadronPilotXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 7;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 6;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Sensor,
                UpgradeType.Modification,
                UpgradeType.Tech
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}