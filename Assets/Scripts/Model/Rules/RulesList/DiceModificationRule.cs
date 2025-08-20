using ActionsList;
using Ship;
using static Ship.GenericShip;

namespace RulesList
{
    public class DiceModificationRule
    {
        public event EventHandlerActionBool OnAllowRangeZeroAttackModifications;
        public event EventHandlerActionBool OnAllowRangeZeroDefenseModifications;

        public void PreventRangeZeroOwnModifications(GenericShip ship, GenericAction action, ref bool allowed)
        {
            if (IsAttackerRangeZeroDiceModification(ship, action)) allowed = false;

            CallOnAllowRangeZeroAttackModifications(action, ref allowed);
        }

        public void PreventRangeZeroCompareResultsModifications(GenericAction action, ref bool allowed)
        {
            if (IsAttackerRangeZeroDiceModification(Combat.Attacker, action)) allowed = false;

            CallOnAllowRangeZeroDefenseModifications(action, ref allowed);
        }

        private bool IsAttackerRangeZeroDiceModification(GenericShip ship, GenericAction action)
        {
            return Combat.ShotInfo.Range == 0
                && Combat.ShotInfo.Weapon.WeaponType == WeaponTypes.PrimaryWeapon
                && Combat.Attacker == ship
                && !action.IsNotRealDiceModification;
        }

        private void CallOnAllowRangeZeroAttackModifications(GenericAction action, ref bool allowed)
        {
            OnAllowRangeZeroAttackModifications?.Invoke(action, ref allowed);
        }

        private void CallOnAllowRangeZeroDefenseModifications(GenericAction action, ref bool allowed)
        {
            OnAllowRangeZeroDefenseModifications?.Invoke(action, ref allowed);
        }
    }
}
