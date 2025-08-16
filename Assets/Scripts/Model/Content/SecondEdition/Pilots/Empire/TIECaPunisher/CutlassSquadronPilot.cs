using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIECaPunisher
    {
        public class CutlassSquadronPilot : TIECaPunisher
        {
            public CutlassSquadronPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Cutlass Squadron Pilot",
                    "",
                    Faction.Imperial,
                    2,
                    5,
                    6,
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Torpedo,
                        UpgradeType.Missile,
                        UpgradeType.Gunner,
                        UpgradeType.Device,
                        UpgradeType.Modification
                    },
                    seImageNumber: 141,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class CutlassSquadronPilotXWA : CutlassSquadronPilot
        {
            public CutlassSquadronPilotXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 13;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                        UpgradeType.Sensor,
                        UpgradeType.Gunner,
                        UpgradeType.Modification,
                        UpgradeType.Device,
                        UpgradeType.Missile,
                        UpgradeType.Torpedo
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
