using Content;
using Org.BouncyCastle.Tls.Crypto.Impl;
using Ship;
using SubPhases;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ARC170Starfighter
    {
        public class GarvenDreis : ARC170Starfighter
        {
            public GarvenDreis() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Garven Dreis",
                    "Red Leader",
                    Faction.Rebel,
                    4,
                    4,
                    7,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.GarvenDreisArcAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Cannon,
                        UpgradeType.Missile,
                        UpgradeType.Gunner,
                        UpgradeType.Astromech,
                        UpgradeType.Modification
                    },
                    seImageNumber: 66,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class GarvenDreisXWA : ARC170Starfighter
        {
            public GarvenDreisXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Garven Dreis",
                    "Red Leader",
                    Faction.Rebel,
                    4,
                    4,
                    3,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.GarvenDreisArcAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Astromech,
                        UpgradeType.Gunner,
                        UpgradeType.Modification,
                        UpgradeType.Cannon,
                        UpgradeType.Missile                        
                    },
                    seImageNumber: 66,
                    legality: new List<Legality> { Legality.XWA }
                );
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GarvenDreisArcAbility : GarvenDreisXWingAbility
    {
        protected override bool FilterAbilityTarget(GenericShip ship)
        {
            // Change the ability to work at r3.
            return FilterByTargetType(ship, new List<TargetTypes>() { TargetTypes.OtherFriendly }) && FilterTargetsByRange(ship, 1, 3);
        }
    }
}