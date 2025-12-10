using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.Delta7BAethersprite
{
    public class ObiWanKenobi7B : Delta7BAethersprite
    {
        public ObiWanKenobi7B()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Obi-Wan Kenobi",
                "Guardian of the Republic",
                Faction.Republic,
                5,
                7,
                15,
                true,
                force: 3,
                abilityType: typeof(Abilities.SecondEdition.ObiWanKenobiAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
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
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            PilotNameCanonical = "obiwankenobi-delta7baethersprite";
        }
    }

    public class ObiWanKenobi7BXWA : ObiWanKenobi7B
    {
        public ObiWanKenobi7BXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 14;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                    UpgradeType.ForcePower,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}