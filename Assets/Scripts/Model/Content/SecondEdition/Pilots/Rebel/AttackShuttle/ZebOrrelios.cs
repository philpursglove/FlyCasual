using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.AttackShuttle
{
    public class ZebOrrelios : AttackShuttle
    {
        public ZebOrrelios() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "\"Zeb\" Orrelios",
                "Spectre-4",
                Faction.Rebel,
                2,
                3,
                10,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.ZebOrreliosPilotAbility),
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Turret,
                    UpgradeType.Crew,
                    UpgradeType.Modification,
                    UpgradeType.Title
                },
                seImageNumber: 37,
                tags: new List<Tags>
                {
                    Tags.Spectre
                },
                legality: new List<Legality>() { Legality.ExtendedLegal }
            );
        }
    }

    public class ZebOrreliosXWA : ZebOrrelios
    {
        public ZebOrreliosXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 3;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 6;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Talent,
                UpgradeType.Crew,
                UpgradeType.Modification,
                UpgradeType.Turret,
                UpgradeType.Title
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}
