using Obstacles;
using Players;
using Ship;
using SubPhases;
using System;
using Tokens;

namespace Obstacles
{
    public class ElectroChaffCloud : GenericObstacle
    {
        protected int fuses = 0;

        public GenericPlayer Assigner;

        public int Fuses
        {
            get => fuses;
            set
            {
                var oldValue = fuses;
                var newValue = value;
                FusesChanging?.Invoke(this, oldValue, ref newValue);
                fuses = newValue;
            }
        }
        public delegate void DeviceValueChanging(GenericObstacle deviceGameObject, int oldValue, ref int newValue);
        public event DeviceValueChanging FusesChanging;
        public bool IsFused => Fuses > 0;
        public ElectroChaffCloud(string name, string shortName, GenericPlayer owner) : base(name, shortName)
        {
            Assigner = owner;
            RulesList.TargetLocksRule.OnCheckTargetLockIsDisallowed += DisallowTargetLocks;
        }

        public override string GetTypeName => "Electro-Chaff Cloud";

        public void DisallowTargetLocks(ref bool result, GenericShip attacker, ITargetLockable defender)
        {
            if (result)
            {
                if (attacker != null && attacker.IsLandedOnObstacle && attacker.ObstaclesLanded.Contains(this))
                {
                    result = false;
                }

                if (defender != null && defender is GenericShip ship && ship.IsLandedOnObstacle && ship.ObstaclesLanded.Contains(this))
                {
                    result = false;
                }
            }
        }

        public override void OnHit(GenericShip ship)
        {
            Messages.ShowErrorToHuman(ship.PilotInfo.PilotName + " hit Electro-Chaff Cloud during movement, All lock are broken, Jam token is assigned");
            BreakAllLocks(ship, delegate { ship.Tokens.AssignToken(new JamToken(ship, Assigner), () => StartToRoll(ship)); });
        }

        private void BreakAllLocks(GenericShip ship, Action callback)
        {
            ship.Tokens.RemoveAllTokensByType(typeof(GenericTargetLockToken), callback, '*');
        }

        private void StartToRoll(GenericShip ship)
        {
            Messages.ShowErrorToHuman(ship.PilotInfo.PilotName + " hit Electro-Chaff Cloud during movement, rolling for effect");

            ElectroChaffCloudHitCheckSubPhase newPhase = (ElectroChaffCloudHitCheckSubPhase)Phases.StartTemporarySubPhaseNew(
                "Stress from Electro-Chaff Cloud collision",
                typeof(ElectroChaffCloudHitCheckSubPhase),
                delegate
                {
                    Phases.FinishSubPhase(typeof(ElectroChaffCloudHitCheckSubPhase));
                    Triggers.FinishTrigger();
                });
            newPhase.TheShip = ship;
            newPhase.TheObstacle = this;
            newPhase.Start();
        }

        public override void AfterObstacleRoll(GenericShip ship, DieSide side, Action callback)
        {
            if (side == DieSide.Crit || (side == DieSide.Success)
            )
            {
                Messages.ShowErrorToHuman($"{ship.PilotInfo.PilotName} gains a Stress token");
                ship.Tokens.AssignToken(new StressToken(ship), callback);
            }
            else
            {
                NoEffect(callback);
            }
        }

        private void NoEffect(Action callback)
        {
            Messages.ShowInfoToHuman("No damage");
            callback();
        }
    }
}

namespace SubPhases
{
    public class ElectroChaffCloudHitCheckSubPhase : DiceRollCheckSubPhase
    {
        private readonly GenericShip prevActiveShip = Selection.ActiveShip;
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


