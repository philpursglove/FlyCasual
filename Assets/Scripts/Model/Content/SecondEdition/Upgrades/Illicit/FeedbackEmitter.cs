using Ship;
using System;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class FeedbackEmitter : GenericUpgrade
    {
        public FeedbackEmitter()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Feedback Emitter",
                UpgradeType.Illicit,
                abilityType: typeof(Abilities.SecondEdition.FeedbackEmitterAbility),
                charges: 1
                
            );
            IsHidden = true;

        }
    }
}

namespace Abilities.SecondEdition
{
    public class FeedbackEmitterAbility : GenericAbility
    {
        private GenericShip ObjectForAbility;

        public override void ActivateAbility()
        {
            GenericShip.OnTokenIsAssignedGlobal += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnTokenIsAssignedGlobal -= CheckAbility;
        }

        private void CheckAbility(GenericShip ship, GenericToken token)
        {
            if (HostUpgrade.State.Charges > 0
                && token is BlueTargetLockToken
                && ((token as BlueTargetLockToken).OtherTargetLockTokenOwner as GenericShip)?.ShipId == HostShip.ShipId)
            {
                ObjectForAbility = (token as BlueTargetLockToken).OtherTargetLockTokenOwner as GenericShip;
                AskToUseAbility("Feedback Emitter",
                    AlwaysUseByDefault,
                    JamIt,
                    descriptionLong: $"Do you wish to jam {ObjectForAbility}?");
            }
        }

        private void JamIt(object sender, EventArgs e)
        {
            if (ObjectForAbility is GenericShip)
            {
                Messages.ShowInfo($"Feedback Emitter: {ObjectForAbility.PilotInfo.PilotName} is Jammed");

                HostUpgrade.State.LoseCharge();

                ObjectForAbility.Tokens.AssignToken(
                    new JamToken(ObjectForAbility, HostShip.Owner),
                    Triggers.FinishTrigger
                );
            }
            else
            {
                Messages.ShowInfo($"Feedback Emitter: non-ship object is not Jammed");
                Triggers.FinishTrigger();
            }

        }
    }
}