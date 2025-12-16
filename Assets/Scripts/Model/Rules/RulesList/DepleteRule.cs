using Movement;
using Ship;
using System.Collections.Generic;
using System.Linq;
using Tokens;

namespace RulesList
{
    public class DepleteRule
    {
        public void CheckForDepletedDebuff(ref int count)
        {
            if (Combat.Attacker.IsDepleted)
            {
                Combat.Attacker.Tokens.GetTokens<DepleteToken>().FirstOrDefault().WasApplied = true;
                Messages.ShowInfo("Depleted: Attacker rolls -1 attack die");
                count--;
            }
        }

        public void TryRemoveDepleteTokenAfterAttack(GenericShip ship)
        {
            List<DepleteToken> depleteTokens = Combat.Attacker.Tokens.GetTokens<DepleteToken>().Where(t => t.WasApplied).ToList();

            if (depleteTokens.Count > 0)
            {
                foreach(DepleteToken token in depleteTokens) {
                    Triggers.RegisterTrigger(
                        new Trigger()
                        {
                            Name = "Remove Deplete token",
                            TriggerOwner = Combat.Attacker.Owner.PlayerNo,
                            TriggerType = TriggerTypes.OnAttackFinish,
                            EventHandler = delegate { RemoveDepleteToken(Combat.Attacker, token); }
                        }
                    );
                }
            }
        }

        public void TryRemoveDepleteTokenAfterManeuver(GenericShip ship)
        {
            if (Selection.ThisShip.IsDepleted && Selection.ThisShip.GetLastManeuverColor() == MovementComplexity.Easy)
            {
                Triggers.RegisterTrigger(
                    new Trigger()
                    {
                        Name = "Remove Deplete token",
                        TriggerOwner = Selection.ThisShip.Owner.PlayerNo,
                        TriggerType = TriggerTypes.OnMovementExecuted,
                        EventHandler = delegate { RemoveDepleteToken(Selection.ThisShip); }
                    }
                );
            }
        }

        private void RemoveDepleteToken(GenericShip ship)
        {
            ship.Tokens.RemoveToken(typeof(DepleteToken), Triggers.FinishTrigger);
        }

        private void RemoveDepleteToken(GenericShip ship, GenericToken token)
        {
            ship.Tokens.RemoveToken(token, Triggers.FinishTrigger);
        }
    }
}

