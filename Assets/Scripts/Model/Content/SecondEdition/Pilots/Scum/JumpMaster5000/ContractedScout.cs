using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.JumpMaster5000
    {
        public class ContractedScout : JumpMaster5000
        {
            public ContractedScout() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Contracted Scout",
                    "",
                    Faction.Scum,
                    2,
                    5,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Cannon,
                        UpgradeType.Torpedo,
                        UpgradeType.Illicit
                    },
                    seImageNumber: 217,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class ContractedScoutXWA : ContractedScout
        {
            public ContractedScoutXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Illicit,
                    UpgradeType.Cannon,
                    UpgradeType.Modification
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).Limited = 2;
            }
        }
    }
}