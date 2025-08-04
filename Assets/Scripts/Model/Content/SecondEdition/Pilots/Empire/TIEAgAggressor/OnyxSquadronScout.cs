using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEAgAggressor
    {
        public class OnyxSquadronScout : TIEAgAggressor
        {
            public OnyxSquadronScout() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Onyx Squadron Scout",
                    "",
                    Faction.Imperial,
                    3,
                    4,
                    12,
                    seImageNumber: 129,
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Turret,
                        UpgradeType.Missile,
                        UpgradeType.Gunner
                    },
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class OnyxSquadronScoutXWA : OnyxSquadronScout
        {
            public OnyxSquadronScoutXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 14;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Turret,
                    UpgradeType.Missile
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
