using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.Delta7BAethersprite
{
    public class JediKnight7B : Delta7BAethersprite
    {
        public JediKnight7B()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Jedi Knight",
                "",
                Faction.Republic,
                3,
                6,
                3,
                force: 1,
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
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            PilotNameCanonical = "jediknight-delta7baethersprite";
        }
    }

    public class JediKnight7BXWA : JediKnight7B
    {
        public JediKnight7BXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 11;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 4;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}