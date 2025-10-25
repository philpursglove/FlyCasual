using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIESeBomber
{
    public class FirstOrderCadet : TIESeBomber
    {
        public FirstOrderCadet() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "First Order Cadet",
                "",
                Faction.FirstOrder,
                3,
                4,
                7,
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Tech,
                    UpgradeType.Torpedo,
                    UpgradeType.Missile,
                    UpgradeType.Gunner,
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

    public class FirstOrderCadetXWA : FirstOrderCadet
    {
        public FirstOrderCadetXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 11;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 19;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Gunner,
                UpgradeType.Modification,
                UpgradeType.Tech,
                UpgradeType.Device,
                UpgradeType.Device,
                UpgradeType.Missile,
                UpgradeType.Torpedo
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}