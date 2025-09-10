using Actions;
using ActionsList;
using Arcs;
using Movement;
using Ship.CardInfo;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.SecondEdition.ModifiedYT1300LightFreighter
{
    public class ModifiedYT1300LightFreighter : GenericShip
    {
        public ModifiedYT1300LightFreighter() : base()
        {
            ShipInfo = new ShipCardInfo25
            (
                "Modified YT-1300 Light Freighter",
                BaseSize.Large,
                new FactionData
                (
                    new Dictionary<Faction, System.Type>
                    {
                        { Faction.Rebel, typeof(HanSolo) }
                    }
                ),
                new ShipArcsInfo(ArcType.DoubleTurret, 3),
                1, 8, 5,
                new ShipActionsInfo
                (
                    new ActionInfo(typeof(FocusAction)),
                    new ActionInfo(typeof(TargetLockAction)),
                    new ActionInfo(typeof(RotateArcAction)),
                    new ActionInfo(typeof(BoostAction), ActionColor.Red)
                ),
                new ShipUpgradesInfo()
            );

            ModelInfo = new ShipModelInfo
            (
                "YT-1300",
                "YT-1300",
                new Vector3(-3.25f, 7.55f, 5.55f),
                3.5f
            );

            DialInfo = new ShipDialInfo
            (
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal),

                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),

                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.SegnorsLoop, MovementComplexity.Complex),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.SegnorsLoop, MovementComplexity.Complex),

                new ManeuverInfo(ManeuverSpeed.Speed4, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed4, ManeuverDirection.Forward, ManeuverBearing.KoiogranTurn, MovementComplexity.Complex)
            );

            SoundInfo = new ShipSoundInfo
            (
                new List<string>()
                {
                    "Falcon-Fly1",
                    "Falcon-Fly2",
                    "Falcon-Fly3"
                },
                "Falcon-Fire", 2
            );

            ShipIconLetter = 'm';
        }
    }
}

namespace Abilities.SecondEdition
{
    //After you perform a red action, you may roll an attack die. On a hit/crit result, remove 1 stress.
    public class HighStakesAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += CheckActionAbility;
        }
        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= CheckActionAbility;
        }

        private void CheckActionAbility(GenericAction action)
        {
            if (action.IsRed)
            {
                RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AskToRoll);
            }
        }

        private void AskToRoll(object sender, System.EventArgs e)
        {
            AskToUseAbility(
                HostShip.PilotInfo.PilotName,
                AlwaysUseByDefault,
                UseAbility,
                DontUseAbility,
                descriptionLong: "Do you want to roll 1 attack die? (On a \"hit\" or \"crit\" result, remove 1 stress token)",
                imageHolder: HostShip
            );
        }

        private void UseAbility(object sender, System.EventArgs e)
        {
            Phases.StartTemporarySubPhaseOld(
                HostShip.PilotInfo.PilotName + ": Try to remove stress",
                typeof(SubPhases.BraylenStrammCheckSubPhase),
                delegate {
                    //We have a BraylenStrammCheckSubPhase open, so finish it
                    Phases.FinishSubPhase(typeof(SubPhases.BraylenStrammCheckSubPhase));

                    //We have a Decision SubPhase open, so finish it
                    SubPhases.DecisionSubPhase.ConfirmDecisionNoCallback();

                    //The trigger is still active, so finish it.  Must be explicitly finished since ConfirmDecisionNoCallback was used
                    Triggers.FinishTrigger();
                }
            );
        }

        private void DontUseAbility(object sender, System.EventArgs e)
        {
            SubPhases.DecisionSubPhase.ConfirmDecisionNoCallback();
            Triggers.FinishTrigger();
        }
    }
}