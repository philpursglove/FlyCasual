using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.T65XWing
{
    public class CavernAngelsZealot : T65XWing
    {
        public CavernAngelsZealot() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Cavern Angels Zealot",
                "",
                Faction.Rebel,
                1,
                5,
                4,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Astromech,
                    UpgradeType.Illicit,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Partisan,
                    Tags.XWing
                },
                seImageNumber: 12,
                skinName: "Partisan",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class CavernAngelsZealotXWA : CavernAngelsZealot
    {
        public CavernAngelsZealotXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 10;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 10;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Astromech,
                UpgradeType.Illicit,
                UpgradeType.Modification,
                UpgradeType.Missile,
                UpgradeType.Configuration
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}