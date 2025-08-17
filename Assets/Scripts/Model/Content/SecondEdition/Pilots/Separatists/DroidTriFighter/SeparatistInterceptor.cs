using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.DroidTriFighter
{
    public class SeparatistInterceptor : DroidTriFighter
    {
        public SeparatistInterceptor()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Separatist Interceptor",
                "",
                Faction.Separatists,
                3,
                3,
                3,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Missile,
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

    public class SeparatistInterceptorXWA : SeparatistInterceptor
    {
        public SeparatistInterceptorXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 3;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 4;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}