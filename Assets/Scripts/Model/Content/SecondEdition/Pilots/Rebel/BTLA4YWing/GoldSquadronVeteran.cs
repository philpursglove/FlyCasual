using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.BTLA4YWing
{
    public class GoldSquadronVeteran : BTLA4YWing
    {
        public GoldSquadronVeteran() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Gold Squadron Veteran",
                "",
                Faction.Rebel,
                3,
                4,
                6,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Turret,
                    UpgradeType.Missile,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.YWing
                },
                seImageNumber: 17,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class GoldSquadronVeteranXWA : GoldSquadronVeteran
    {
        public GoldSquadronVeteranXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 3;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Modification,
                    UpgradeType.Turret,
                    UpgradeType.Missile
                };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}