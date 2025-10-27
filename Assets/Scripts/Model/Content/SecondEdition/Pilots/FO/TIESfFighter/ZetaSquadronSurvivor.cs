using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIESfFighter
{
    public class ZetaSquadronSurvivor : TIESfFighter
    {
        public ZetaSquadronSurvivor() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Zeta Squadron Survivor",
                "",
                Faction.FirstOrder,
                2,
                4,
                4,
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Sensor,
                    UpgradeType.Tech,
                    UpgradeType.Gunner
                },
                tags: new List<Tags>
                {
                    Tags.Tie
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class ZetaSquadronSurvivorXWA : ZetaSquadronSurvivor
    {
        public ZetaSquadronSurvivorXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 10;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 16;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Sensor,
                UpgradeType.Gunner,
                UpgradeType.Modification,
                UpgradeType.Tech,
                UpgradeType.Missile
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}