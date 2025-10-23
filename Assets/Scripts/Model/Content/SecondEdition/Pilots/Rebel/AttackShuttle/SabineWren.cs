using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.AttackShuttle
{
    public class SabineWren : AttackShuttle
    {
        public SabineWren() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Sabine Wren",
                "Spectre-5",
                Faction.Rebel,
                3,
                4,
                6,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.SabineWrenPilotAbility),
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Turret,
                    UpgradeType.Crew,
                    UpgradeType.Modification,
                    UpgradeType.Title
                },
                seImageNumber: 36,
                tags: new List<Tags>
                {
                    Tags.Mandalorian,
                    Tags.Spectre
                },
                legality: new List<Legality>() { Legality.ExtendedLegal }
            );
        }
    }

    public class SabineWrenXWA : SabineWren
    {
        public SabineWrenXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 10;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 13;
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