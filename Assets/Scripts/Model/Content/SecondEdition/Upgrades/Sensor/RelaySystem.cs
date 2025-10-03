using System;
using Ship;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class RelaySystem : GenericUpgrade
    {
        public RelaySystem()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Relay System",
                UpgradeType.Sensor,
                abilityType: typeof(Abilities.SecondEdition.RelaySystemAbility)
            );
            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class RelaySystemAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnTokenIsAssignedGlobal += CheckReceiveTargetLockAbility;

            HostShip.OnAttackHitAsAttacker += CheckPassOnTargetLockAbility;
        }

        private void CheckPassOnTargetLockAbility()
        {
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnTokenIsAssignedGlobal -= CheckReceiveTargetLockAbility;

            HostShip.OnAttackHitAsAttacker -= CheckPassOnTargetLockAbility;
        }

        private ITargetLockable _lockedObject;

        private void CheckReceiveTargetLockAbility(GenericShip ship, GenericToken token)
        {
            if ((token is BlueTargetLockToken) && (ship.Owner.PlayerNo == HostShip.Owner.PlayerNo) &&
                (ship.GetRangeToShip(HostShip) < 3))
            {
                _lockedObject = ((BlueTargetLockToken)token).OtherTargetLockTokenOwner;

                AskToUseAbility("Relay System",
                    NeverUseByDefault,
                    descriptionLong: $"You may acquire a lock",
                    useAbility: AcquireTargetLock);
            }
        }

        private void AcquireTargetLock(object sender, EventArgs e)
        {
            if (HostShip.Tokens.HasToken<BlueTargetLockToken>())
            {
                BlueTargetLockToken existingToken = (BlueTargetLockToken)HostShip.Tokens.GetToken(typeof(BlueTargetLockToken));
                HostShip.RemoveToken(existingToken);
            }

            ActionsHolder.AcquireTargetLock(HostShip, _lockedObject, () => { }, () => { }, false);
        }
    }
}