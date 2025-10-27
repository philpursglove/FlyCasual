using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTANR2WYWing
    {
        public class CaiThrenalli : BTANR2WYWing
        {
            public CaiThrenalli() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "C'ai Threnalli",
                    "Tenacious Survivor",
                    Faction.Resistance,
                    2,
                    10,
                    12,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.CaiThrenalliAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Astromech,
                        UpgradeType.Modification,
                        UpgradeType.Modification,
                        UpgradeType.Tech,
                        UpgradeType.Device,
                        UpgradeType.Turret,
                        UpgradeType.Missile,
                        UpgradeType.Torpedo
                    },
                    tags: new List<Tags>
                    {
                        Tags.YWing
                    },
                    skinName: "Red",
                    legality: new List<Legality> { Legality.XWA }
                );

                PilotNameCanonical = "caithrenalli-btanr2wywing";

                ImageUrl = "https://infinitearenas.com/xw2/images/pilots/caithrenalli-btanr2ywing.png";
            }
        }
    }
}
