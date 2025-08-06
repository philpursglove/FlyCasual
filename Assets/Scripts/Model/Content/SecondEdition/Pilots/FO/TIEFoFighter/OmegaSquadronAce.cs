using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEFoFighter
    {
        public class OmegaSquadronAce : TIEFoFighter
        {
            public OmegaSquadronAce() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Omega Squadron Ace",
                    "",
                    Faction.FirstOrder,
                    3,
                    3,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Tech,
                        UpgradeType.Modification,
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

        public class OmegaSquadronAceXWA : OmegaSquadronAce
        {
            public OmegaSquadronAceXWA(): base()
            {
                var pilot = (PilotCardInfo25) PilotInfo;
                pilot.LegalityInfo = new List<Legality> {Legality.XWA};
                pilot.Cost = 3;
                pilot.LoadoutValue = 10;
                pilot.ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Sensor,
                    UpgradeType.Tech,
                    UpgradeType.Modification,
                    UpgradeType.Modification
                };
            }
        }
    }
}
