using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.Delta7BAethersprite
{
    public class PloKoon7B : Delta7BAethersprite
    {
        public PloKoon7B()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Plo Koon",
                "Serene Mentor",
                Faction.Republic,
                5,
                7,
                19,
                isLimited: true,
                force: 2,
                abilityType: typeof(Abilities.SecondEdition.PloKoonAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                    UpgradeType.Talent,
                    UpgradeType.Astromech,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Jedi,
                    Tags.LightSide
                },
                skinName: "Plo Koon",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            PilotNameCanonical = "plokoon-delta7baethersprite";
        }
    }

    public class PloKoon7BXWA : PloKoon7B
    {
        public PloKoon7BXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 14;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                    UpgradeType.ForcePower,
                    UpgradeType.Astromech,
                    UpgradeType.Modification
                };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}