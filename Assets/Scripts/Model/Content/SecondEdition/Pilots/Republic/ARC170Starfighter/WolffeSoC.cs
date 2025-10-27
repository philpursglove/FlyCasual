using Content;
using System.Collections.Generic;
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
                    isStandardLayout: true,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Wolfpack));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.VeteranTailGunner));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Q7Astromech));

                ShipAbilities.Add(new Abilities.SecondEdition.BornForThisAbility());

                PilotNameCanonical = "wolffe-siegeofcoruscant";

                ModelInfo.SkinName = "Wolffe";
            }
        }

        public class WolffeSoCXWA : WolffeSoC
        {
            public WolffeSoCXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 12;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}