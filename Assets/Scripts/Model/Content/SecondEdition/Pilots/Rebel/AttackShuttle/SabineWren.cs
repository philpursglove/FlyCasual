using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.AttackShuttle
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

        public class SabineWrenXWA : AttackShuttle
        {
            public SabineWrenXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Sabine Wren",
                    "Spectre-5",
                    Faction.Rebel,
                    3,
                    4,
                    11,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.SabineWrenPilotAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Crew,
                        UpgradeType.Modification,
                        UpgradeType.Turret,
                        UpgradeType.Title
                    },
                    seImageNumber: 36,
                    tags: new List<Tags>
                    {
                        Tags.Mandalorian,
                        Tags.Spectre
                    },
                    legality: new List<Legality>() { Legality.XWA }
                );
            }
        }
    }
}