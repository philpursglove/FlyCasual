using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.RogueClassStarfighter
    {
        public class ViktorHel : RogueClassStarfighter
        {
            public ViktorHel() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Viktor Hel",
                    "Storied Bounty Hunter",
                    Faction.Scum,
                    4,
                    4,
                    11,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.ViktorHelAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Cannon,
                        UpgradeType.Cannon,
                        UpgradeType.Missile
                    },
                    tags: new List<Tags>()
                    {
                        Tags.BountyHunter
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "viktorhel-rogueclassstarfighter";
            }
        }

        public class ViktorHelXWA : ViktorHel
        {
            public ViktorHelXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 11;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                    UpgradeType.Cannon,
                    UpgradeType.Cannon,
                };
            }
        }
    }
}