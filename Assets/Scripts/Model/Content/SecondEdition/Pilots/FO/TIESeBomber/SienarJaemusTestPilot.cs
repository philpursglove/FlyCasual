
using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIESeBomber
{
    public class SienarJeamusTestPilot : TIESeBomber
    {
        public SienarJeamusTestPilot() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Sienar-Jaemus Test Pilot",
                "",
                Faction.FirstOrder,
                2,
                4,
                8,
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Tech,
                    UpgradeType.Tech,
                    UpgradeType.Missile,
                    UpgradeType.Device,
                    UpgradeType.Device,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Tie
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class SienarJeamusTestPilotXWA : SienarJeamusTestPilot
    {
        public SienarJeamusTestPilotXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 9;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Gunner,
                UpgradeType.Modification,
                UpgradeType.Tech,
                UpgradeType.Device,
                UpgradeType.Device,
                UpgradeType.Missile,
                UpgradeType.Missile
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}