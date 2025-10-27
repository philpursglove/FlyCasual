using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.Mg100StarFortress
{
    public class CobaltSquadronBomber : Mg100StarFortress
    {
        public CobaltSquadronBomber() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Cobalt Squadron Bomber",
                "",
                Faction.Resistance,
                1,
                6,
                12,
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Sensor,
                    UpgradeType.Gunner,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Tech,
                    UpgradeType.Device,
                    UpgradeType.Device
                },
                legality: new List<Legality>() { Legality.ExtendedLegal }
            );

            ModelInfo.SkinName = "Cobalt";
        }
    }

    public class CobaltSquadronBomberXWA : CobaltSquadronBomber
    {
        public CobaltSquadronBomberXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 15;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 20;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Tech,
                    UpgradeType.Device,
                    UpgradeType.Device
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}