using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.UT60DUWing
{
    public class PartisanRenegade : UT60DUWing
    {
        public PartisanRenegade() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Partisan Renegade",
                "",
                Faction.Rebel,
                1,
                5,
                6,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Illicit,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Partisan
                },
                seImageNumber: 61,
                skinName: "Partisan",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class PartisanRenegadeXWA : PartisanRenegade
    {
        public PartisanRenegadeXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 11;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 12;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Crew,
                UpgradeType.Crew,
                UpgradeType.Sensor,
                UpgradeType.Illicit,
                UpgradeType.Modification,
                UpgradeType.Configuration
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}