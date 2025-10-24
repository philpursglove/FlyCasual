using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.Hwk290LightFreighter
{
    public class RebelScout : Hwk290LightFreighter
    {
        public RebelScout() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Rebel Scout",
                "",
                Faction.Rebel,
                2,
                4,
                6,
                extraUpgradeIcons: new List<UpgradeType>
                {
                        UpgradeType.Device,
                        UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                        Tags.Freighter
                },
                seImageNumber: 45,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class RebelScoutXWA : RebelScout
    {
        public RebelScoutXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 7;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 5;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Modification,
                    UpgradeType.Device,
                    UpgradeType.Device
                };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}