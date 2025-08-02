using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.GauntletFighter
    {
        public class EzraBridger : GauntletFighter
        {
            public EzraBridger() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Ezra Bridger",
                    "Spectre-6",
                    Faction.Rebel,
                    3,
                    6,
                    12,
                    force: 1,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.EzraBridgerPilotAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.ForcePower,
                        UpgradeType.Talent,
                        UpgradeType.Crew,
                        UpgradeType.Gunner,
                        UpgradeType.Device,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Modification,
                        UpgradeType.Configuration,
                        UpgradeType.Title
                    },
                    tags: new List<Tags>()
                    {
                        Tags.LightSide,
                        Tags.Spectre
                    },
                    skinName: "Red",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "ezrabridger-gauntletfighter";

                ImageUrl = "https://static.wikia.nocookie.net/xwing-miniatures-second-edition/images/3/3f/Ezra-gauntlet.png";
            }
        }

        public class EzraBridgerXWA : GauntletFighter
        {
            public EzraBridgerXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Ezra Bridger",
                    "Spectre-6",
                    Faction.Rebel,
                    3,
                    6,
                    15,
                    force: 1,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.EzraBridgerPilotAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.ForcePower,
                        UpgradeType.ForcePower,
                        UpgradeType.Crew,
                        UpgradeType.Gunner,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Modification,
                        UpgradeType.Device,
                        UpgradeType.Configuration,
                        UpgradeType.Title
                    },
                    tags: new List<Tags>()
                    {
                        Tags.LightSide,
                        Tags.Spectre
                    },
                    skinName: "Red",
                    legality: new List<Legality> { Legality.XWA }
                );

                PilotNameCanonical = "ezrabridger-gauntletfighter";

                ImageUrl = "https://static.wikia.nocookie.net/xwing-miniatures-second-edition/images/3/3f/Ezra-gauntlet.png";
            }
        }
    }
}