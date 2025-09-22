using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class Malice : GenericUpgrade
    {
        public Malice() : base()
        {
            UpgradeInfo = new UpgradeCardInfo
            (
                "Malice",
                UpgradeType.ForcePower,
                cost: 4,
                restriction: new TagRestriction(Content.Tags.DarkSide),
                abilityType: typeof(Abilities.SecondEdition.MaliceAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class MaliceAbility : GenericAbility
    {
        public bool abilityUsed = false;

        public override void ActivateAbility()
        {
            AddDiceModification(
                HostUpgrade.UpgradeInfo.Name,
                IsDiceModificationAvailable,
                GetDiceModificationAiPriority,
                DiceModificationType.Change,
                1,
                new List<DieSide>() { DieSide.Focus, DieSide.Success },
                DieSide.Crit,
                payAbilityCost: payForce
            );

            GenericShip.OnFaceupCritCardReadyToBeDealtGlobal += CheckRecoverForce;
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();

            GenericShip.OnFaceupCritCardReadyToBeDealtGlobal -= CheckRecoverForce;
        }

        private void CheckRecoverForce(GenericShip ship, GenericDamageCard crit, EventArgs e)
        {
            if (abilityUsed
                && Combat.Defender != null
                && (Tools.IsSameShip(Combat.Attacker, HostShip))
                && Combat.CurrentCriticalHitCard.IsFaceup && Combat.CurrentCriticalHitCard.Type == CriticalCardType.Pilot)
            {
                abilityUsed = false;
                HostShip.State.RestoreForce(2);
            }
        }

        private void RecoverForce(object sender, EventArgs e)
        {
            abilityUsed = false;
            HostShip.State.RestoreForce(2);
            Triggers.FinishTrigger();
        }

        private bool IsDiceModificationAvailable()
        {
            return (Combat.AttackStep == CombatStep.Attack) && (HostShip.State.Force >= 1) && (Combat.Attacker == HostShip);
        }

        private void payForce(Action<bool> callback)
        {
            if (HostShip.State.Force > 1)
            {
                HostShip.State.SpendForce
                (
                    1,
                    delegate
                    {
                        abilityUsed = true;
                        callback(true);
                    }
                );
            }
            else
            {
                callback(false);
            }
        }

        private int GetDiceModificationAiPriority()
        {
            int result = 0;
            if (Combat.DiceRollAttack.RegularSuccesses + Combat.DiceRollAttack.Focuses > 0) result = 100;
            return result;
        }
    }
}