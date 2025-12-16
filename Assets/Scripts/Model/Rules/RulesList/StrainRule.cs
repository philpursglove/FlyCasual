using ActionsList;
using Movement;
using NUnit.Framework;
using Players;
using Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using Tokens;
using UnityEngine;

namespace RulesList
{
    public class StrainRule
    {
        public void CheckForStrainedDebuff(ref int count)
        {
            if (Combat.Defender.IsStrained)
            {
                Messages.ShowInfo("Strained: Defender rolls -1 defense die");
                Combat.Defender.Tokens.GetToken<StrainToken>().WasApplied = true;
                count--;
            }
        }

        public void TryRemoveAppliedStrainTokenAfterAttack(GenericShip ship)
        {
            List<StrainToken> strainTokens = Combat.Attacker.Tokens.GetTokens<StrainToken>().Where(t => t.WasApplied).ToList();

            foreach (StrainToken token in strainTokens)
            {
                Triggers.RegisterTrigger(
                    new Trigger()
                    {
                        Name = "Remove Strain token",
                        TriggerOwner = Combat.Defender.Owner.PlayerNo,
                        TriggerType = TriggerTypes.OnAttackFinish,
                        EventHandler = delegate { RemoveAppliedStrainToken(Combat.Defender, token); }
                    }
                );
            }
        }

        private void RemoveAppliedStrainToken(GenericShip ship, StrainToken token)
        {
            ship.Tokens.RemoveToken(token, Triggers.FinishTrigger);
        }

        public void TryRemoveStrainTokenAfterManeuver(GenericShip ship)
        {
            if (Selection.ThisShip.IsStrained && Selection.ThisShip.GetLastManeuverColor() == MovementComplexity.Easy)
            {
                Triggers.RegisterTrigger(
                    new Trigger()
                    {
                        Name = "Remove Strain token",
                        TriggerOwner = Selection.ThisShip.Owner.PlayerNo,
                        TriggerType = TriggerTypes.OnMovementExecuted,
                        EventHandler = delegate { RemoveStrainToken(Selection.ThisShip); }
                    }
                );
            }
        }

        private void RemoveStrainToken(GenericShip ship)
        {
            ship.Tokens.RemoveToken(typeof(StrainToken), Triggers.FinishTrigger);
        }
    }
}

