using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.DroidTriFighter
{
    public class ColicoidInterceptor : DroidTriFighter
    {
        public ColicoidInterceptor()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Colicoid Interceptor",
                "",
                Faction.Separatists,
                1,
                3,
                4,
                extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                tags: new List<Tags>
                {
                    Tags.Droid
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class ColicoidInterceptorXWA : ColicoidInterceptor
    {
        public ColicoidInterceptorXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 8;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 5;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Sensor,
                UpgradeType.Modification,
                UpgradeType.Modification,
                UpgradeType.Cannon,
                UpgradeType.Missile,
                UpgradeType.Configuration
            };
        }
    }
}