using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship
{
    namespace SecondEdition.FiresprayClassPatrolCraft
    {
        public class BobaFettAaD : FiresprayClassPatrolCraft
        {
            public BobaFettAaD() : base()
            {
                PilotCardInfo pilotInfo = new PilotCardInfo25(
                    pilotName: "Boba Fett",
                    pilotTitle: "Armed and Dangerous",
                    faction: Faction.Scum,
                    initiative: 5,
                    cost: 18,
                    loadoutValue: 0,
                    isLimited: true,
                    abilityType: typeof(BobaFettAaDAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Gunner,
                        UpgradeType.Device,
                        UpgradeType.Title
                    },
                    isStandardLayout: true,
                    tags: new List<Tags> { Tags.BountyHunter },
                    skinName: "Boba Fett",
                    legality: new List<Legality> { Legality.XWA }
                );

                MustHaveUpgrades.Add(typeof(HomingBeacon));
                MustHaveUpgrades.Add(typeof(FennecShandGunner));
                MustHaveUpgrades.Add(typeof(SeismicCharges));
                MustHaveUpgrades.Add(typeof(SlaveISeparatistsAbility));

                PilotNameCanonical = "bobafett-armedanddangerous";
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class BobaFettAaDAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
        }
        public override void DeactivateAbility()
        {
        }
    }
}