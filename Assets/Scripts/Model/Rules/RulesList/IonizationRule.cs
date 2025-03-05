using Editions;
using Movement;
using Ship;
using System.Collections.Generic;
using System.Linq;
using Tokens;

namespace RulesList
{
    public class IonizationRule
    {
        static bool RuleIsInitialized = false;

        public IonizationRule()
        {
            if (!RuleIsInitialized)
            {
                GenericShip.OnReadyGetManeuvers += SetIonManeuver;
                GenericShip.OnTokenIsAssignedGlobal += RemoveBlueTargetLocks;
                RuleIsInitialized = true;
            }
        }

        public static void SetIonManeuver(GenericShip ship)
        {
            if (IsIonized(ship))
            {
                ship.OnGetManeuvers += GetIonManeuver;
                ship.OnMovementExecuted += RegisterRemoveIonization;
                Edition.Current.WhenIonized(ship);
            }
        }

        private static void RegisterRemoveIonization(GenericShip ship)
        {
            if (BoardTools.Board.IsOffTheBoard(ship)) return;

            ship.AssignedManeuver.IsIonManeuver = true;

            ship.OnMovementExecuted -= RegisterRemoveIonization;

            Triggers.RegisterTrigger(new Trigger
            {
                Name = "Remove ionization",
                TriggerType = TriggerTypes.OnMovementExecuted,
                TriggerOwner = ship.Owner.PlayerNo,
                EventHandler = RemoveIonization,
                Sender = ship,
                Skippable = true
            });
        }

        private static void RemoveIonization(object sender, System.EventArgs e)
        {
            GenericShip ship = sender as GenericShip;
            Messages.ShowInfo(ship.PilotInfo.PilotName + " isn't ionized anymore");

            ship.ToggleIonized(false);

            ship.Tokens.RemoveAllTokensByType(
                typeof(IonToken),
                Triggers.FinishTrigger
            );

            ship.OnGetManeuvers -= GetIonManeuver;
        }

        public static bool IsIonized(GenericShip ship)
        {
            int ionTokensCount = ship.Tokens.GetAllTokens().Count(n => n is IonToken);
            return (ionTokensCount >= Edition.Current.NegativeTokensToAffectShip[ship.ShipInfo.BaseSize]);
        }

        public static void RemoveBlueTargetLocks(GenericShip ship, GenericToken token)
        {
            if (ship.State.IsIonized)
            {
                foreach(BlueTargetLockToken tlock in ship.Tokens.GetTokens<BlueTargetLockToken>('*'))
                {
                    ship.Tokens.RemoveToken(tlock, delegate { });
                }
            }
        }

        public static void GetIonManeuver(Dictionary<string, MovementComplexity> result)
        {
            // Reset current maneuvers
            result.Clear();

            result.Add("1.L.B", MovementComplexity.Easy);
            result.Add("1.F.S", MovementComplexity.Easy);
            result.Add("1.R.B", MovementComplexity.Easy);
        }
    }
}
