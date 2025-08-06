using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEFoFighter
    {
        public class EpsilonSquadronCadet : TIEFoFighter
        {
            public EpsilonSquadronCadet() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Epsilon Squadron Cadet",
                    "",
                    Faction.FirstOrder,
                    1,
                    3,
                    2,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Tech
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class EpsilonSquadronCadetXWA : EpsilonSquadronCadet
        {
            public EpsilonSquadronCadetXWA() : base()
            {
                var pilot = (PilotCardInfo25) PilotInfo;
                pilot.LegalityInfo = new List<Legality> {Legality.XWA};
                pilot.Cost = 3;
                pilot.LoadoutValue = 13;
                pilot.ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Tech,
                    UpgradeType.Sensor,
                    UpgradeType.Modification
                };
            }
        }
    }
}
