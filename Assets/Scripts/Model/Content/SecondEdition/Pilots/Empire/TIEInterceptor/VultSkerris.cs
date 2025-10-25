using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEInterceptor
    {
        public class VultSkerris : TIEInterceptor
        {
            public VultSkerris() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Vult Skerris",
                    "Arrogant Ace",
                    Faction.Imperial,
                    5,
                    4,
                    9,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.VultSkerrisDefenderAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    abilityText: " Action: Recover 1 charge take 1 strain. Before you engage you spend 1 charge to perform an action.",
                    charges: 1,
                    regensCharges: -1,
                    skinName: "Skystrike Academy",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "vultskerris-tieininterceptor";
            }
        }

        public class VultSkerrisXWA : VultSkerris
        {
            public VultSkerrisXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Talent,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                };
            }
        }
    }
}