using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.RZ1AWing
{
    public class PhoenixSquadronPilot : RZ1AWing
    {
        public PhoenixSquadronPilot() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Phoenix Squadron Pilot",
                "",
                Faction.Rebel,
                1,
                4,
                3,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.AWing
                },
                seImageNumber: 22,
                skinName: "Phoenix Squadron",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class PhoenixSquadronPilotXWA : PhoenixSquadronPilot
    {
        public PhoenixSquadronPilotXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 7;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 1;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Modification,
                    UpgradeType.Missile,
                    UpgradeType.Configuration
                };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}