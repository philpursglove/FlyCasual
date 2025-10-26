using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.Eta2Actis
{
    public class JediGeneral : Eta2Actis
    {
        public JediGeneral()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Jedi General",
                "",
                Faction.Republic,
                4,
                5,
                4,
                force: 2,
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.ForcePower,
                    UpgradeType.Cannon,
                    UpgradeType.Astromech,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Jedi,
                    Tags.LightSide
                },
                skinName: "Blue",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class JediGeneralXWA : JediGeneral
    {
        public JediGeneralXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 10;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.ForcePower,
                UpgradeType.Talent,
                UpgradeType.Astromech,
                UpgradeType.Modification,
                UpgradeType.Cannon
            };
        }
    }
}