using ActionsList;
using Ship;
using System;

namespace DamageDeckCardSE
{

    public class DamagedSensorArray : GenericDamageCard
    {
        public DamagedSensorArray()
        {
            Name = "Damaged Sensor Array";
            Type = CriticalCardType.Ship;
            ImageUrl = "https://infinitearenas.com/xw2/images/damagecards/19_damagecard.png";
        }

        public override void ApplyEffect(object sender, EventArgs e)
        {
            Host.OnTryAddAction += OnlyCancelCritActionsOrFocus;
            Host.OnGenerateActions += CallAddCancelCritAction;

            Host.Tokens.AssignCondition(typeof(Tokens.DamagedSensorArraySECritToken));
            Triggers.FinishTrigger();
        }

        public override void DiscardEffect()
        {
            base.DiscardEffect();

            Messages.ShowInfo("Damaged Sensor Array has been repaired,  " + Host.PilotInfo.PilotName + " can perform actions as usual");
            Host.Tokens.RemoveCondition(typeof(Tokens.DamagedSensorArraySECritToken));

            Host.OnTryAddAction -= OnlyCancelCritActionsOrFocus;
            Host.OnGenerateActions -= CallAddCancelCritAction;
        }

        private void OnlyCancelCritActionsOrFocus(GenericShip ship, GenericAction action, ref bool result)
        {
            if (!action.IsCritCancelAction && !(action is FocusAction))
            {
                result = false;
            }
        }

    }

}

namespace Tokens
{
    public class DamagedSensorArraySECritToken : CritToken
    {
        public DamagedSensorArraySECritToken(GenericShip host) : base(host)
        {
            Tooltip = "https://infinitearenas.com/xw2/images/damagecards/19_damagecard.png";
        }
    }
}