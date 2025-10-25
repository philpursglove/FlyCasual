using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIESfFighter
{
    public class OmegaSquadronExpert : TIESfFighter
    {
        public OmegaSquadronExpert() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Omega Squadron Expert",
                "",
                Faction.FirstOrder,
                3,
                4,
                7,
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Sensor,
                    UpgradeType.Tech,
                    UpgradeType.Missile,
                    UpgradeType.Gunner,
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

    public class OmegaSquadronExpertXWA : OmegaSquadronExpert
    {
        public OmegaSquadronExpertXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 11;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 19;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Talent,
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