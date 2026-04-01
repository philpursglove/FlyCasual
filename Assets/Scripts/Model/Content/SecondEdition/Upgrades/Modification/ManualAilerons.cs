using BoardTools;
using Movement;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class ManualAilerons : GenericUpgrade
    {
        public ManualAilerons()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Manual Ailerons",
                UpgradeType.Modification,
                abilityType: typeof(Abilities.SecondEdition.ManualAileronsAbility),
                charges: 2
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class ManualAileronsAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnGetAvailableDecloakTemplates += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnGetAvailableDecloakTemplates -= CheckAbility;
        }

        private void CheckAbility(List<ManeuverTemplate> availableTemplates)
        {
            if (HostUpgrade.State.Charges > 0)
            {
                // Ask the player if they want to spend a charge to use a 2-speed template instead of the 1-speed template to barrel roll or boost
                AskToUseAbility("Manual Ailerons",
                    NeverUseByDefault,
                    descriptionLong:
                    "You may spend 1 charge to use a 2-speed template instead of the 1-speed template to barrel roll or boost",
                    useAbility: delegate { UseManualAilerons(availableTemplates); }
                );
            }
        }

        private void UseManualAilerons(List<ManeuverTemplate> availableTemplates)
        {
            HostUpgrade.State.SpendCharge();

            if (availableTemplates.Any(n => n.Name == "Straight 2"))
            {
                availableTemplates.RemoveAll(n => n.Name == "Straight 2");
                availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Left, ManeuverSpeed.Speed2));
                availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Right, ManeuverSpeed.Speed2));
                availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.SideslipBank, ManeuverDirection.Left, ManeuverSpeed.Speed2));
                availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.SideslipBank, ManeuverDirection.Right, ManeuverSpeed.Speed2));
            }

            Triggers.FinishTrigger();
        }
    }
}