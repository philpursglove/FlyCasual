using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.T65XWing
{
    public class GarvenDreisBoY : T65XWing
    {
        public GarvenDreisBoY() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Garven Dreis",
                "Red Leader",
                Faction.Rebel,
                4,
                4,
                0,
                isLimited: true,
                abilityType: typeof(GarvenDreisXWingAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Torpedo,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.XWing
                },
                isStandardLayout: true,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            ShipAbilities.Add(new HopeAbility());

            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.AdvProtonTorpedoes));
            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.R5K6BoY));

            PilotNameCanonical = "garvendreis-battleofyavin";
        }
    }

    public class GarvenDreisBoYXWA : GarvenDreisBoY
    {
        public GarvenDreisBoYXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 12;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}