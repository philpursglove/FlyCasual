using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.LambdaClassT4AShuttle
    {
        public class OmicronGroupPilot : LambdaClassT4AShuttle
        {
            public OmicronGroupPilot() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Omicron Group Pilot",
                    "",
                    Faction.Imperial,
                    1,
                    5,
                    8,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Cannon,
                        UpgradeType.Modification
                    },
                    seImageNumber: 145,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class OmicronGroupPilotXWA : OmicronGroupPilot
        {
            public OmicronGroupPilotXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 11;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 15;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Sensor,
                    UpgradeType.Modification,
                    UpgradeType.Cannon
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
