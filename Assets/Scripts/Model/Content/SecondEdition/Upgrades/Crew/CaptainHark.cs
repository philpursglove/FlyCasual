using Movement;
using Ship;
using SubPhases;
using System;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class CaptainHark : GenericUpgrade
    {
        public CaptainHark() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Captain Hark",
                UpgradeType.Crew,
                cost: 3,
                isLimited: true,
                restriction: new FactionRestriction(Faction.Imperial),
                charges: 2,
                abilityType: typeof(Abilities.SecondEdition.CaptainHarkCrewAbility)
            );
        }        
    }
}

namespace Abilities.SecondEdition
{
    public class CaptainHarkCrewAbility : GenericAbility
    {
        //After you fully execute a red maneuver, if you are not focused,
        //you may spend 1 Charge to gain 1 focus token. 
        public override void ActivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully += RegisterMovementTrigger;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully -= RegisterMovementTrigger;
        }

        protected void RegisterMovementTrigger(GenericShip ship)
        {
            if (HostShip.GetLastManeuverColor() == MovementComplexity.Complex 
                && !HostShip.Tokens.HasToken(typeof(FocusToken)) 
                && HostUpgrade.State.Charges > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, AskAbility);
            }
        }

        private void AskAbility(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostUpgrade.UpgradeInfo.Name,
                AlwaysUseByDefault,
                GainFocus,
                descriptionLong: "Do you want to spend 1 charge to gain 1 focus token?",
                imageHolder: HostUpgrade,
                requiredPlayer: HostShip.Owner.PlayerNo
            );
        }

        private void GainFocus(object sender, EventArgs e)
        {
            HostUpgrade.State.SpendCharge();
            HostShip.Tokens.AssignToken(typeof(FocusToken), DecisionSubPhase.ConfirmDecision);
        }
    }
}