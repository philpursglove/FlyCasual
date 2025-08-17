using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.StarViperClassAttackPlatform
    {
        public class BlackSunEnforcer : StarViperClassAttackPlatform
        {
            public BlackSunEnforcer() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Black Sun Enforcer",
                    "",
                    Faction.Scum,
                    2,
                    5,
                    6,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Tech
                    },
                    seImageNumber: 182,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class BlackSunEnforcerXWA : BlackSunEnforcer
        {
            public BlackSunEnforcerXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 7;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Illicit,
                    UpgradeType.Tech
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}