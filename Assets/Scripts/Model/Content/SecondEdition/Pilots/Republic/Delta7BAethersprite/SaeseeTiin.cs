using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.Delta7BAethersprite
{
    public class SaeseeTiin7B : Delta7BAethersprite
    {
        public SaeseeTiin7B()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Saesee Tiin",
                "Prophetic Pilot",
                Faction.Republic,
                4,
                6,
                9,
                isLimited: true,
                force: 2,
                abilityType: typeof(Abilities.SecondEdition.SaeseeTiinAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                    UpgradeType.Astromech,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Jedi,
                    Tags.LightSide
                },
                legality: new List<Legality>
                {
                    Legality.StandardBanned,
                    Legality.ExtendedLegal
                },
                skinName: "Saesee Tiin"
            );

            PilotNameCanonical = "saeseetiin-delta7baethersprite";
        }
    }

    public class SaeseeTiin7BXWA : SaeseeTiin7B
    {
        public SaeseeTiin7BXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 5;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}
