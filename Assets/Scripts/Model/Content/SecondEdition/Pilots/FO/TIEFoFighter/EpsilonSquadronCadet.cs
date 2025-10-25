using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIEFoFighter
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
            (PilotInfo as PilotCardInfo25).Cost = 6;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 0;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType> () { };
            (PilotInfo as PilotCardInfo25).Limited = 4;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> {Legality.XWA};
        }
    }
}