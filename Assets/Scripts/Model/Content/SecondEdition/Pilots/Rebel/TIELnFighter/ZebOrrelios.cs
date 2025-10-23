using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIELnFighter
{
    public class ZebOrrelios : TIELnFighter
    {
        public ZebOrrelios() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "\"Zeb\" Orrelios",
                "Spectre-4",
                Faction.Rebel,
                2,
                3,
                8,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.ZebOrreliosPilotAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Spectre,
                    Tags.Tie
                },
                seImageNumber: 49,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            PilotNameCanonical = "zeborrelios-tielnfighter";

            ModelInfo.ModelName = "TIE Fighter Rebel";
            ModelInfo.SkinName = "Rebel";
        }
    }

    public class ZebOrreliosXWA : ZebOrrelios
    {
        public ZebOrreliosXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 6;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 5;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Modification,
                UpgradeType.Modification
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}