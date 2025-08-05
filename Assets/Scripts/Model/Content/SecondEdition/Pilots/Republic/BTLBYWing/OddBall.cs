using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTLBYWing
    {
        public class OddBall : BTLBYWing
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
                    18,
                    isLimited: true,
                    abilityText: "After you fully execute a red maneuver or perform a red action, if there is an enemy ship in your bullseye arc, you may acquire a lock on that ship.",
                    abilityType: typeof(Abilities.SecondEdition.OddBallAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Turret,
                        UpgradeType.Torpedo,
                        UpgradeType.Torpedo,
                        UpgradeType.Gunner,
                        UpgradeType.Astromech,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Clone,
                        Tags.YWing
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "oddball-btlbywing";
            }
        }

        public class OddBallXWA : OddBall
        {
            public OddBallXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 13;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}