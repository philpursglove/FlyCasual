using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.V19TorrentStarfighter
{
    public class BlueSquadronProtector : V19TorrentStarfighter
    {
        public BlueSquadronProtector()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Blue Squadron Protector",
                "",
                Faction.Republic,
                3,
                4,
                4,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Clone
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class BlueSquadronProtectorXWA : BlueSquadronProtector
    {
        public BlueSquadronProtectorXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 3;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}