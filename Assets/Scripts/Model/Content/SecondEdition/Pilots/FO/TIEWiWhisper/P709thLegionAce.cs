using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEWiWhisperModifiedInterceptor
    {
        public class P709thLegionAce : TIEWiWhisperModifiedInterceptor
        {
            public P709thLegionAce() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "709th Legion Ace",
                    "",
                    Faction.FirstOrder,
                    4,
                    4,
                    10,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Missile,
                        UpgradeType.Tech,
                        UpgradeType.Tech,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class P709thLegionAceXWA : P709thLegionAce
        {
            public P709thLegionAceXWA() : base()
            {
                var pilot = (PilotCardInfo25)PilotInfo;
                pilot.Cost = 4;
                pilot.LoadoutValue = 5;
                pilot.Legality = new List<Legality> { Legality.XWA };
                pilot.ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Missile,
                    UpgradeType.Tech,
                    UpgradeType.Tech,
                    UpgradeType.Configuration
                };
            }
        }
    }
}
