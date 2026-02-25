using ActionList;
using Actions;
using ActionsList;
using Arcs;
using Movement;
using Ship;
using Ship.CardInfo;
using SubPhases;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.SecondEdition.TIESkStriker
{
    public class TIESkStriker : GenericShip
    {
        public TIESkStriker() : base()
        {
            ShipInfo = new ShipCardInfo25
            (
                "TIE/sk Striker",
                BaseSize.Small,
                new FactionData
                (
                    new Dictionary<Faction, Type>
                    {
                        { Faction.Imperial, typeof(Duchess) }
                    }
                ),
                new ShipArcsInfo(ArcType.Front, 3), 2, 4, 0,
                new ShipActionsInfo
                (
                    new ActionInfo(typeof(FocusAction)),
                    new ActionInfo(typeof(EvadeAction)),
                    new ActionInfo(typeof(BarrelRollAction))
                ),
                new ShipUpgradesInfo()
            );

            ShipAbilities.Add(new Abilities.SecondEdition.AdaptiveAileronsAbility());

            ModelInfo = new ShipModelInfo
            (
                "TIE Striker",
                "Gray",
                new Vector3(-3.45f, 7.15f, 5.55f),
                2f
            );

            DialInfo = new ShipDialInfo
            (
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Forward, ManeuverBearing.KoiogranTurn, MovementComplexity.Complex),

                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.SegnorsLoop, MovementComplexity.Complex),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.SegnorsLoop, MovementComplexity.Complex),

                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal)
            );

            SoundInfo = new ShipSoundInfo
            (
                new List<string>()
                {
                    "TIE-Fly1",
                    "TIE-Fly2",
                    "TIE-Fly3",
                    "TIE-Fly4",
                    "TIE-Fly5",
                    "TIE-Fly6",
                    "TIE-Fly7"
                },
                "TIE-Fire", 3
            );

            ShipIconLetter = 'T';
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AdaptiveAileronsAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnManeuverIsReadyToBeRevealed += RegisterAdaptiveAileronsAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnManeuverIsReadyToBeRevealed -= RegisterAdaptiveAileronsAbility;
        }

        private void RegisterAdaptiveAileronsAbility(GenericShip ship)
        {
            // AI doesn't know how to boost
            if (HostShip.Owner is Players.GenericAiPlayer) return;

            RegisterAbilityTrigger(TriggerTypes.OnManeuverIsReadyToBeRevealed, CheckCanUseAbility);
        }

        private void CheckCanUseAbility(object sender, EventArgs e)
        {
            if (HostShip.PilotInfo.PilotName == "\"Duchess\"")
            {
                DoAdaptiveAileronsAbility(false);
            }
            else if (HostShip.IsStressed)
            {
                Triggers.FinishTrigger();
            }
            else
            {
                DoAdaptiveAileronsAbility(true);
            }
        }

        private void DoAdaptiveAileronsAbility(bool IsForced)
        {
            HostShip.OnActionIsReallyFailed += CleanupFailedAction;

            HostShip.AskPerformFreeAction(
                new AdaptiveAileronsAction() { HostShip = TargetShip, Color = ActionColor.White },
                Cleanup,
                "Adaptive Ailerons",
                $"You {(IsForced ? "must" : "may")} perform a maneuver",
                HostShip,
                isForced: IsForced
            );
        }

        private void CleanupFailedAction(GenericAction action)
        {
            // If action fails, current subphase doesn't get completed correctly
            if (Phases.CurrentSubPhase is BoostPlanningSubPhase)
                DecisionSubPhase.ConfirmDecision();
        }

        private void Cleanup()
        {
            HostShip.OnActionIsReallyFailed -= CleanupFailedAction;
            Triggers.FinishTrigger();
        }
    }
}

namespace ActionList
{
    public class AdaptiveAileronsAction : BoostAction
    {
        public AdaptiveAileronsAction() : base()
        {
            this.Name = "Adaptive Ailerons";
        }
    }
}