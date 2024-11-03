using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using Content;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ARC170Starfighter
    {
        public class WolffeSoC : ARC170Starfighter
        {
            public WolffeSoC() : base()
            {
                PilotInfo = new PilotCardInfo25(
                    "\"Wolffe\"",
                    "Siege of Coruscant",
                    Faction.Republic,
                    4,
                    4,
                    0,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.WolffeAbility),
                    charges: 1,
                    extraUpgradeIcons: new List<UpgradeType>
                    { 
                        UpgradeType.Crew,
                        UpgradeType.Gunner,
                        UpgradeType.Gunner,
                        UpgradeType.Astromech
                    },
                    tags: new List<Tags>
                    {
                        Tags.Clone
                    },
                    isStandardLayout: true
                    
                );

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Wolfpack));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.VeteranTailGunner));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Q7Astromech));

                ShipAbilities.Add(new Abilities.SecondEdition.BornForThisAbility());

                PilotNameCanonical = "wolffe-siegeofcoruscant";

                ModelInfo.SkinName = "Wolffe";
            }
        }
    }
}