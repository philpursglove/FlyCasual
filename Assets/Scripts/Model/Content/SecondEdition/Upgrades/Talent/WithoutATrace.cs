using RulesList;
using Ship;
using SubPhases;
using System;
using System.Linq;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class WithoutATrace : GenericUpgrade
    {
        public WithoutATrace()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Without a Trace",
                UpgradeType.Talent,
                abilityType: typeof(Abilities.SecondEdition.WithoutATraceAbility)
            );

            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class WithoutATraceAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnTokenIsAssigned += RegisterAbility;

            TargetLocksRule.OnCheckTargetLockIsDisallowed += ForbidLockOnMe;

        }
        public override void DeactivateAbility()
        {
            HostShip.OnTokenIsAssigned -= RegisterAbility;

            TargetLocksRule.OnCheckTargetLockIsDisallowed -= ForbidLockOnMe;

        }

        private void ForbidLockOnMe(ref bool isAllowed, GenericShip lockSource, ITargetLockable lockTarget)
        {
            if (lockTarget is GenericShip 
                && (lockTarget as GenericShip) == HostShip
                && HostShip.IsCloaked)
            {
                isAllowed = false;
            }
        }

        private void RegisterAbility(GenericShip ship, GenericToken token)
        {
            if (token is CloakToken && HostShip.Tokens.HasTokenByColor(TokenColors.Red))
            {
                RegisterAbilityTrigger(TriggerTypes.OnTokenIsAssigned, AskToUseAbility);
            }
        }

        private void AskToUseAbility(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostShip.PilotInfo.PilotName,
                AlwaysUseByDefault,
                UseAbility,
                descriptionLong: "After cloaking you may remove a red token",
                imageHolder: HostShip
            );
        }

        private void UseAbility(object sender, EventArgs e)
        {
            WithoutATraceRemoveRedTokenAbilityDecisionSubPhase subphase = Phases.StartTemporarySubPhaseNew<WithoutATraceRemoveRedTokenAbilityDecisionSubPhase>(
                "Without A Trace: You may remove 1 red token",
                Triggers.FinishTrigger
            );
            subphase.ImageSource = HostShip;
            subphase.AbilityHostShip = HostShip;
            subphase.Start();
        }
    }
}

namespace SubPhases
{
    public class WithoutATraceRemoveRedTokenAbilityDecisionSubPhase : RemoveRedTokenDecisionSubPhase
    {
        public GenericShip AbilityHostShip;

        public override void PrepareCustomDecisions()
        {
            DescriptionShort = "Without A Trace";
            DescriptionLong = "You may remove 1 red token";

            DecisionOwner = Selection.ThisShip.Owner;
            DefaultDecisionName = decisions.First().Name;
        }
    }
}