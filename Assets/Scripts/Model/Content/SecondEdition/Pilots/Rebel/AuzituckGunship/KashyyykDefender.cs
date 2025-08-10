using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.AuzituckGunship
{
    public class KashyyykDefender : AuzituckGunship
    {
        public KashyyykDefender() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Kashyyyk Defender",
                "",
                Faction.Rebel,
                1,
                5,
                6,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Modification
                },
                seImageNumber: 33,
                legality: new List<Legality>() { Legality.ExtendedLegal }
            );
        }
    }

    public class KashyyykDefenderXWA : KashyyykDefender
    {
        public KashyyykDefenderXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 5;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 16;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Crew,
                UpgradeType.Crew,
                UpgradeType.Modification
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}
