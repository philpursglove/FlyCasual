using System.Collections.Generic;
using Abilities;
using ActionsList.SecondEdition;
using BoardTools;
using Ship;
using Team;
using Tokens;
using UnityEngine;

public class NoEscapeAbility : GenericAbility
{
    public override void ActivateAbility()
    {
        HostShip.OnGenerateDiceModifications += AddNoEscapeReroll;
    }

    public override void DeactivateAbility()
    {
        HostShip.OnGenerateDiceModifications -= AddNoEscapeReroll;
    }

    private void AddNoEscapeReroll(GenericShip ship)
    {
        HostShip.AddAvailableDiceModificationOwn(new NoEscapeActionEffect());
    }
}

namespace ActionsList.SecondEdition
{
    public class NoEscapeActionEffect : GenericAction
    {
        public NoEscapeActionEffect()
        {
            Name = "No Escape";
            DiceModificationName = "No Escape";
        }

        public override bool IsDiceModificationAvailable()
        {
            var attackingShipsCount = Board.GetShipsAtRange(Combat.Defender, new Vector2(0, 1), Type.Enemy).Count;
            var defendingShipsCount = Board.GetShipsAtRange(Combat.Defender, new Vector2(0, 1), Type.Friendly).Count;

            return attackingShipsCount > defendingShipsCount;
        }

        public override void ActionEffect(System.Action callBack)
        {
            DiceRerollManager diceRerollManager = new DiceRerollManager
            {
                NumberOfDiceCanBeRerolled = 1,
                CallBack = callBack,
                SidesCanBeRerolled = new List<DieSide>{DieSide.Blank}
            };
            diceRerollManager.Start();
        }

        public override int GetDiceModificationPriority()
        {
            int result = 0;

            if (Combat.AttackStep == CombatStep.Attack)
            {
                int attackFocuses = Combat.CurrentDiceRoll.FocusesNotRerolled;
                int attackBlanks = Combat.CurrentDiceRoll.BlanksNotRerolled;
                int numFocusTokens = Selection.ActiveShip.Tokens.CountTokensByType(typeof(FocusToken));
                // Only use Fire Control if the number of dice that need re-rolled is 1.
                if (numFocusTokens > 0)
                {
                    // Slightly above Target Lock.
                    if (attackBlanks == 1) result = 81;
                }
                else
                {
                    // Slightly above Target Lock.
                    if (attackBlanks + attackFocuses == 1) result = 81;
                }
            }

            return result;
        }

    }
}