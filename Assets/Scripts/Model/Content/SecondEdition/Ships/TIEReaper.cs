using System.Collections.Generic;
using Movement;
using ActionsList;
using Actions;
using Arcs;
using Upgrade;
using System;
using Ship.CardInfo;
using Ship;
using Tokens;
using System.Linq;
using SubPhases;
using UnityEngine;

namespace Ship
{
    namespace SecondEdition.TIEReaper
    {
        public class TIEReaper : GenericShip
        {
            public TIEReaper() : base()
            {
                ShipInfo = new ShipCardInfo25
                (
                    "TIE Reaper",
                    BaseSize.Medium,
                    new FactionData
                    (
                        new Dictionary<Faction, Type>
                        {
                            { Faction.Imperial, typeof(ScarifBasePilot) }
                        }
                    ),
                    new ShipArcsInfo(ArcType.Front, 3), 1, 6, 2,
                    new ShipActionsInfo
                    (
                        new ActionInfo(typeof(FocusAction)),
                        new ActionInfo(typeof(EvadeAction)),
                        new ActionInfo(typeof(JamAction)),
                        new ActionInfo(typeof(CoordinateAction), ActionColor.Red)
                    ),
                    new ShipUpgradesInfo()
                );

                ShipAbilities.Add(new Abilities.SecondEdition.ControlledAileronsAbility());

                ModelInfo = new ShipModelInfo
                (
                    "TIE Reaper",
                    "Gray",
                    previewScale: 2f
                );

                DialInfo = new ShipDialInfo
                (
                    new ManeuverInfo(ManeuverSpeed.Speed0, ManeuverDirection.Stationary, ManeuverBearing.Stationary, MovementComplexity.Complex),

                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Complex),
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Complex),
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.SegnorsLoop, MovementComplexity.Complex),
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.SegnorsLoop, MovementComplexity.Complex),

                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Complex),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Complex),

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

                ShipIconLetter = 'V';
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class ControlledAileronsAbility : GenericAbility
    {
        private GenericMovement SavedManeuver;

        private static readonly List<string> ChangedManeuversCodes = new List<string>() { "1.L.B", "1.F.S", "1.R.B" };
        private Dictionary<string, MovementComplexity> SavedManeuverColors;
        bool doAilerons = false;
        private Vector3 preAileronsPosition;

        public override void ActivateAbility()
        {
            HostShip.OnManeuverIsReadyToBeRevealed += RegisterControlledAileronsAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnManeuverIsReadyToBeRevealed -= RegisterControlledAileronsAbility;
        }

        private void RegisterControlledAileronsAbility(GenericShip ship)
        {
            // AI doesn't know how to boost
            if (HostShip.Owner.GetType().IsSubclassOf(typeof(Players.GenericAiPlayer))) return;

            RegisterAbilityTrigger(TriggerTypes.OnManeuverIsReadyToBeRevealed, CheckCanUseAbility);
        }

        private void CheckCanUseAbility(object sender, EventArgs e)
        {
            if (HostShip.Tokens.HasToken(typeof(StressToken)))
            {
                Triggers.FinishTrigger();
            }
            else
            {
                DoControlledAileronsAbility();
            }
        }

        private void DoControlledAileronsAbility()
        {
            SavedManeuver = HostShip.AssignedManeuver;

            SavedManeuverColors = new Dictionary<string, MovementComplexity>();
            foreach (var changedManeuver in ChangedManeuversCodes)
            {
                SavedManeuverColors.Add(changedManeuver, HostShip.DialInfo.PrintedDial.First(n => n.Key.ToString() == changedManeuver).Value);
                HostShip.Maneuvers[changedManeuver] = MovementComplexity.Normal;
            }

            Triggers.RegisterTrigger(
                new Trigger()
                {
                    Name = "Ailerons Planning",
                    TriggerType = TriggerTypes.OnAbilityDirect,
                    TriggerOwner = Selection.ThisShip.Owner.PlayerNo,
                    EventHandler = AskToUseControlledAilerons
                }
            );

            Triggers.ResolveTriggers(TriggerTypes.OnAbilityDirect, ExecuteSelectedManeuver);
        }

        private void AskToUseControlledAilerons(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostShip.PilotInfo.PilotName,
                NeverUseByDefault,
                UseAilerons,
                DontUseAilerons,
                descriptionLong: "Do you want to activate your Controlled Ailerons?",
                imageHolder: HostShip
            );
        }

        private void DontUseAilerons(object sender, EventArgs e)
        {
            DecisionSubPhase.ConfirmDecision();
        }

        private void UseAilerons(object sender, EventArgs e)
        {
            DecisionSubPhase.ConfirmDecisionNoCallback();
            SelectAdaptiveAileronsManeuver(sender, e);
        }

        private void SelectAdaptiveAileronsManeuver(object sender, EventArgs e)
        {
            doAilerons = true;
            HostShip.Owner.ChangeManeuver(
                ShipMovementScript.SendAssignManeuverCommand,
                Triggers.FinishTrigger,
                AdaptiveAileronsFilter
            );
        }

        private void RestoreManeuverColors(GenericShip ship)
        {
            foreach (var changedManeuver in ChangedManeuversCodes)
            {
                HostShip.Maneuvers[changedManeuver] = SavedManeuverColors[changedManeuver];
            }
        }

        private void ExecuteSelectedManeuver()
        {
            preAileronsPosition = HostShip.GetPosition();

            if (doAilerons)
            {
                HostShip.AssignedManeuver.IsRevealDial = false;
                HostShip.AssignedManeuver.GrantedBy = "Ailerons";
                HostShip.CanPerformActionsWhenBumped = true;
                HostShip.CanPerformActionsWhenOverlapping = true;
                ShipMovementScript.LaunchMovement(FinishAdaptiveAileronsAbility);
            }
            else
            {
                FinishAdaptiveAileronsAbility();
            }
        }

        private void FinishAdaptiveAileronsAbility()
        {
            if (HostShip.IsBumped || HostShip.IsLandedOnObstacle || HostShip.IsHitObstacles)
            {
                Messages.ShowErrorToHuman("Controlled Ailerons boost fails");

                HostShip.SetPosition(preAileronsPosition);
            }

            doAilerons = false;
            HostShip.CanPerformActionsWhenBumped = false;
            HostShip.CanPerformActionsWhenOverlapping = false;
            RestoreManeuverColors(HostShip);
            Phases.CurrentSubPhase.IsReadyForCommands = true;
            //ship may have flown off the board; only assign saved maneuver if ship is exists
            if (Roster.GetShipById("ShipId:" + Selection.ThisShip.ShipId) != null)
            {
                ManeuverSelectionSubphase subphase = Phases.StartTemporarySubPhaseNew<ManeuverSelectionSubphase>(
                    "Select a maneuver",
                    Triggers.FinishTrigger
                );
                subphase.RequiredPlayer = Selection.ThisShip.Owner.PlayerNo;
                subphase.Start();
                subphase.IsReadyForCommands = true;

                ShipMovementScript.AssignManeuver(Selection.ThisShip.ShipId, SavedManeuver.ToString());
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private bool AdaptiveAileronsFilter(string maneuverString)
        {
            GenericMovement movement = ShipMovementScript.MovementFromString(maneuverString, HostShip);
            if (movement.ManeuverSpeed != ManeuverSpeed.Speed1) return false;
            if (movement.Bearing == ManeuverBearing.Straight || movement.Bearing == ManeuverBearing.Bank) return true;

            return false;
        }

    }
}
