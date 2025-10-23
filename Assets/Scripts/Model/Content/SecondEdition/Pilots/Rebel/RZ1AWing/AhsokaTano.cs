using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.RZ1AWing
{
    public class AhsokaTano : RZ1AWing
    {
        public AhsokaTano() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Ahsoka Tano",
                "Fulcrum",
                Faction.Rebel,
                5,
                4,
                7,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.AhsokaTanoRebelAbility),
                force: 3,
                extraUpgradeIcons: new List<UpgradeType>
                {
                        UpgradeType.ForcePower,
                        UpgradeType.ForcePower,
                        UpgradeType.Talent,
                        UpgradeType.Missile,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                        Tags.AWing,
                        Tags.LightSide
                },
                abilityText: "After you fully execute a maneuver, you may choose a friendly ship at range 0-1 and spend 1 Force. That ship may perform an action, even if it is stressed.",
                skinName: "Blue",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            PilotNameCanonical = "ahsokatano-rz1awing";
        }
    }

    public class AhsokaTanoXWA : AhsokaTano
    {
        public AhsokaTanoXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 12;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 12;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                    UpgradeType.ForcePower,
                    UpgradeType.Modification,
                    UpgradeType.Missile,
                    UpgradeType.Configuration
                };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AhsokaTanoRebelAbility : AhsokaTanoAbility
    {
        protected override int ForceCost => 2;
        protected override int MinRange => 1;
        protected override int MaxRange => 2;
    }
}