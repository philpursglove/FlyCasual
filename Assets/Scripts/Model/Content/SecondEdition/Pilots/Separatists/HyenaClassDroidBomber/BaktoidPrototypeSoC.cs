using Content;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship.SecondEdition.HyenaClassDroidBomber
{
    public class BaktoidPrototypeSoC : HyenaClassDroidBomber
    {
        public BaktoidPrototypeSoC()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Baktoid Prototype",
                "Siege of Coruscant",
                Faction.Separatists,
                1,
                3,
                0,
                limited: 2,
                abilityType: typeof(Abilities.SecondEdition.BaktoidPrototypeAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Missile,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Droid
                },
                isStandardLayout: true,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            MustHaveUpgrades.Add(typeof(HomingMissiles));
            MustHaveUpgrades.Add(typeof(ContingencyProtocol));
            MustHaveUpgrades.Add(typeof(StrutLockOverride));

            PilotNameCanonical = "baktoidprototype-siegeofcoruscant";
        }
    }

    public class BaktoidPrototypeSoCXWA : BaktoidPrototypeSoC
    {
        public BaktoidPrototypeSoCXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 7;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}