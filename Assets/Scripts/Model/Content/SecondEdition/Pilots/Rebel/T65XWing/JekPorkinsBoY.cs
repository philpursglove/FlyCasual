using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.T65XWing
    {
        public class JekPorkinsBoY : T65XWing
        {
            public JekPorkinsBoY() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Jek Porkins",
                    "Battle of Yavin",
                    Faction.Rebel,
                    4,
                    4,
                    0,
                    isLimited: true,
                    abilityType: typeof(JekPorkinsAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Torpedo,
                        UpgradeType.Astromech,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.XWing
                    },
                    skinName: "Jek Porkins",
                    isStandardLayout: true,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                ShipAbilities.Add(new HopeAbility());

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.AdvProtonTorpedoes));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.R5D8));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.UnstableSublightEngines));

                ImageUrl = "https://static.wikia.nocookie.net/xwing-miniatures-second-edition/images/1/1b/Jekporkins-battleofyavin.png";

                PilotNameCanonical = "jekporkins-battleofyavin";
            }
        }

        public class JekPorkinsBoYXWA : JekPorkinsBoY
        {
            public JekPorkinsBoYXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 5;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}