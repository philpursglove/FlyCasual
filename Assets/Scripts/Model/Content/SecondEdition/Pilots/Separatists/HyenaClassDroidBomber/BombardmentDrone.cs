using BoardTools;
using Content;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace Ship.SecondEdition.HyenaClassDroidBomber
{
    public class BombardmentDrone : HyenaClassDroidBomber
    {
        public BombardmentDrone()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Bombardment Drone",
                "Time on Target",
                Faction.Separatists,
                3,
                3,
                8,
                limited: 3,
                abilityType: typeof(Abilities.SecondEdition.BombardmentDroneAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Device,
                    UpgradeType.Device,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Droid
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class BombardmentDroneXWA : BombardmentDrone
    {
        public BombardmentDroneXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 3;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Device,
                UpgradeType.Device,
                UpgradeType.Modification,
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class BombardmentDroneAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnGetAvailableBombLaunchTemplates += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnGetAvailableBombLaunchTemplates -= CheckAbility;
        }

        private void CheckAbility(List<ManeuverTemplate> availableTemplates, GenericUpgrade upgrade)
        {
            List<ManeuverTemplate> dropTemplates = HostShip.GetAvailableBombDropTemplates(upgrade);

            foreach (ManeuverTemplate template in dropTemplates)
            {
                if (!availableTemplates.Any(n => n.Name == template.Name))
                {
                    availableTemplates.Add(template);
                }
            }
        }
    }
}