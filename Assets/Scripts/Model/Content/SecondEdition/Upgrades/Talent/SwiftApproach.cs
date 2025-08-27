using Abilities.SecondEdition;
using ActionsList;
using System;
using System.Collections.Generic;
using Actions;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class SwiftApproach : GenericUpgrade
    {
        public SwiftApproach()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Swift Approach",
                UpgradeType.Talent,
                cost: 0,
                abilityType: typeof(SwiftApproachAbility)
            );

            IsHidden = true;

            IsWIP = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SwiftApproachAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnBombWasDropped += CheckAbility;
            HostShip.OnBombWasLaunched += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnBombWasDropped -= CheckAbility;
            HostShip.OnBombWasLaunched -= CheckAbility;
        }

        private void CheckAbility()
        {
            if (Phases.CurrentPhase.Name != "System") return;

            RegisterAbilityTrigger(TriggerTypes.OnBombWasDropped, AskToPerformReposition);
            RegisterAbilityTrigger(TriggerTypes.OnBombWasLaunched, AskToPerformReposition);
        }

        private void AskToPerformReposition(object sender, EventArgs e)
        {
            HostShip.AskPerformFreeAction(
                new List<GenericAction>()
                {
                    new BarrelRollAction(){CanBePerformedWhileStressed = true, Color = ActionColor.White},
                    new BoostAction(){CanBePerformedWhileStressed = true, Color = ActionColor.White}
                },
                Triggers.FinishTrigger,
                HostUpgrade.NamePostfix,
                "After dropping or launching a device, you may perform a white Barrel Roll or Boost action",
                HostShip
            );
        }
    }
}