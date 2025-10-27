using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.SheathipedeClassShuttle
{
    public class EzraBridger : SheathipedeClassShuttle
    {
        public EzraBridger() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Ezra Bridger",
                "Spectre-6",
                Faction.Rebel,
                3,
                4,
                6,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.EzraBridgerPilotAbility),
                force: 1,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                    UpgradeType.Crew,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                    UpgradeType.Title
                },
                tags: new List<Tags>
                {
                    Tags.Spectre,
                    Tags.LightSide
                },
                seImageNumber: 39,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            PilotNameCanonical = "ezrabridger-sheathipedeclassshuttle";
        }
    }

    public class EzraBridgerXWA : EzraBridger
    {
        public EzraBridgerXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 9;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                    UpgradeType.Astromech,
                    UpgradeType.Crew,
                    UpgradeType.Modification,
                    UpgradeType.Title
                };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}