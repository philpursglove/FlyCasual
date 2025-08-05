using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ARC170Starfighter
    {
        public class OddBall : ARC170Starfighter
        {
            public OddBall() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "\"Odd Ball\"",
                    "CC-2237",
                    Faction.Republic,
                    5,
                    4,
                    7,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.OddBallAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Sensor,
                        UpgradeType.Torpedo,
                        UpgradeType.Torpedo,
                        UpgradeType.Astromech,
                        UpgradeType.Gunner,
                        UpgradeType.Gunner,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Clone
                    },
                    skinName: "Red",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "oddball-arc170starfighter";
            }
        }

        public class OddBallXWA : OddBall
        {
            public OddBallXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 5;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 17;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
