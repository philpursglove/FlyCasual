using Obstacles;
using Ship;
using SubPhases;
using System;

namespace Obstacles
{
    public class LooseCargo: GenericObstacle
    {
        public LooseCargo(string name, string shortName) : base(name, shortName)
        {
            
        }

        public override string GetTypeName => "Debris";

        public override void OnHit(GenericShip ship)
        {
            Messages.ShowErrorToHuman($"{ship.PilotInfo.PilotName} hit {Name} during movement, Strain token is assigned");
            ship.Tokens.AssignToken(
                typeof(Tokens.StrainToken), 
                delegate { StartToRoll(ship); }
            );
        }

        private void StartToRoll(GenericShip ship)
        {
            Messages.ShowErrorToHuman($"{ship.PilotInfo.PilotName} hit {Name} during movement, rolling for effect");

            LoosCargoHitCheckSubPhase newPhase = (LoosCargoHitCheckSubPhase)Phases.StartTemporarySubPhaseNew(
                $"Damage from {Name} collision",
                typeof(LoosCargoHitCheckSubPhase),
                delegate
                {
                    Phases.FinishSubPhase(typeof(LoosCargoHitCheckSubPhase));
                    Triggers.FinishTrigger();
                });
            newPhase.TheShip = ship;
            newPhase.TheObstacle = this;
            newPhase.Start();
        }

        public override void AfterObstacleRoll(GenericShip ship, DieSide side, Action callback)
        {
            if (side == DieSide.Crit
                || (side == DieSide.Success && Editions.Edition.Current.RuleSet.GetType() == typeof(Editions.RuleSets.RuleSet25)))
            {
                ship.Tokens.AssignToken(
                    typeof(Tokens.StressToken),
                    callback
                );
            }
            else
            {
                NoEffect(callback);
            }
        }

        private void NoEffect(Action callback)
        {
            Messages.ShowInfoToHuman("No additional effect");
            callback();
        }
    }
}

namespace SubPhases
{

    public class LoosCargoHitCheckSubPhase : DiceRollCheckSubPhase
    {
        private GenericShip prevActiveShip = Selection.ActiveShip;
        public GenericObstacle TheObstacle { get; set; }

        public override void Prepare()
        {
            DiceKind = DiceKind.Attack;
            DiceCount = 1;

            AfterRoll = FinishAction;
            Selection.ActiveShip = TheShip;
        }

        protected override void FinishAction()
        {
            HideDiceResultMenu();
            Selection.ActiveShip = prevActiveShip;

            TheObstacle.AfterObstacleRoll(TheShip, CurrentDiceRoll.DiceList[0].Side, CallBack);
        }
    }
}