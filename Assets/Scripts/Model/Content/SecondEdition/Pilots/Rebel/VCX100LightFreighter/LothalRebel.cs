using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.VCX100LightFreighter
{
    public class LothalRebel : VCX100LightFreighter
    {
        public LothalRebel() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Lothal Rebel",
                "",
                Faction.Rebel,
                2,
                7,
                8,
                tags: new List<Tags>
                {
                    Tags.Freighter
                },
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Turret,
                    UpgradeType.Torpedo,
                    UpgradeType.Gunner
                },
                seImageNumber: 76,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class LothalRebelXWA : LothalRebel
    {
        public LothalRebelXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 15;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Crew,
                UpgradeType.Crew,
                UpgradeType.Sensor,
                UpgradeType.Gunner,
                UpgradeType.Modification,
                UpgradeType.Turret,
                UpgradeType.Torpedo
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}