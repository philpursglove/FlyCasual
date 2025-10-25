using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.KihraxzFighter
    {
        public class BlackSunAce : KihraxzFighter
        {
            public BlackSunAce() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Black Sun Ace",
                    "",
                    Faction.Scum,
                    3,
                    4,
                    3,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent
                    },
                    seImageNumber: 195,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );

                ModelInfo.SkinName = "Black Sun (White)";
            }
        }

        public class BlackSunAceXWA : BlackSunAce
        {
            public BlackSunAceXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Illicit,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                    UpgradeType.Missile
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
