using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.RZ1AWing
    {
        public class HeraSyndulla : RZ1AWing
        {
            public HeraSyndulla() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Hera Syndulla",
                    "Phoenix Leader",
                    Faction.Rebel,
                    6,
                    4,
                    5,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.HeraSyndullaABWingAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Sensor,
                        UpgradeType.Missile,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.AWing,
                        Tags.Spectre
                    },
                    skinName: "Hera Syndulla",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "herasyndulla-rz1awing";

                ImageUrl = "https://static.wikia.nocookie.net/xwing-miniatures-second-edition/images/d/dc/Herasyndullaawing.png";
            }
        }

        public class HeraSyndullaXWA : RZ1AWing
        {
            public HeraSyndullaXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Hera Syndulla",
                    "Phoenix Leader",
                    Faction.Rebel,
                    6,
                    4,
                    4,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.HeraSyndullaABWingAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Sensor,
                        UpgradeType.Modification,
                        UpgradeType.Missile,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.AWing,
                        Tags.Spectre
                    },
                    skinName: "Hera Syndulla",
                    legality: new List<Legality> { Legality.XWA }
                );

                PilotNameCanonical = "herasyndulla-rz1awing";

                ImageUrl = "https://static.wikia.nocookie.net/xwing-miniatures-second-edition/images/d/dc/Herasyndullaawing.png";
            }
        }
    }
}