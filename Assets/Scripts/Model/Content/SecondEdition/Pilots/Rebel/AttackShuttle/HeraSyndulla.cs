using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.AttackShuttle
    {
        public class HeraSyndulla : AttackShuttle
        {
            public HeraSyndulla() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Hera Syndulla",
                    "Spectre-2",
                    Faction.Rebel,
                    5,
                    4,
                    9,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.HeraSyndullaAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Turret,
                        UpgradeType.Crew,
                        UpgradeType.Modification,
                        UpgradeType.Title
                    },
                    seImageNumber: 34,
                    tags: new List<Tags>
                    {
                        Tags.Spectre
                    },
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class HeraSyndullaXWA : AttackShuttle
        {
            public HeraSyndullaXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Hera Syndulla",
                    "Spectre-2",
                    Faction.Rebel,
                    5,
                    4,
                    14,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.HeraSyndullaAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Crew,
                        UpgradeType.Modification,
                        UpgradeType.Turret,
                        UpgradeType.Title
                    },
                    seImageNumber: 34,
                    tags: new List<Tags>
                    {
                        Tags.Spectre
                    },
                    legality: new List<Legality>() { Legality.XWA }
                );
            }
        }
    }
}