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
            (PilotInfo as PilotCardInfo25).Cost = 9;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 13;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                    UpgradeType.Device,
                    UpgradeType.Turret,
                    UpgradeType.Missile
                };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}