using System.Collections.Generic;
using Content;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.XiClassLightShuttle
    {
        public class FirstOrderCourier : XiClassLightShuttle
        {
            public FirstOrderCourier() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "First Order Courier",
                    "",
                    Faction.FirstOrder,
                    2,
                    4,
                    10,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Tech,
                        UpgradeType.Tech,
                        UpgradeType.Crew,
                        UpgradeType.Modification
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "firstordercourier-xiclasslightshuttle";
            }
        }
    }
}

