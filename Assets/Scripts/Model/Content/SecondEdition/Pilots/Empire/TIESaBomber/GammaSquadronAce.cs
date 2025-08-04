using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIESaBomber
    {
        public class GammaSquadronAce : TIESaBomber
        {
            public GammaSquadronAce() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Gamma Squadron Ace",
                    "",
                    Faction.Imperial,
                    3,
                    4,
                    8,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Torpedo,
                        UpgradeType.Gunner,
                        UpgradeType.Device,
                        UpgradeType.Device,
                        UpgradeType.Modification
                    },
                    seImageNumber: 111,
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                ModelInfo.SkinName = "Gamma Squadron";
            }
        }

        public class GammaSquadronAceXWA : GammaSquadronAce
        {
            public GammaSquadronAceXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 10;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Device,
                    UpgradeType.Device,
                    UpgradeType.Torpedo
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
