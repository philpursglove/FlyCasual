using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ModifiedYT1300LightFreighter
    {
        public class OuterRimSmuggler : ModifiedYT1300LightFreighter
        {
            public OuterRimSmuggler() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Outer Rim Smuggler",
                    "",
                    Faction.Rebel,
                    1,
                    7,
                    6,
                    tags: new List<Tags>
                    {
                        Tags.Freighter,
                        Tags.YT1300
                    },
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Missile,
                        UpgradeType.Gunner
                    },
                    seImageNumber: 72,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class OuterRimSmugglerXWA : OuterRimSmuggler
        {
            public OuterRimSmugglerXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 6;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 11;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Missile
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
