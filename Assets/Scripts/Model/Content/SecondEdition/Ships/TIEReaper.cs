using Actions;
using ActionsList;
using Arcs;
using Movement;
using Ship;
using Ship.CardInfo;
using System;
using System.Collections.Generic;

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
    // Left in place in case XWA or AMG reintroduce Adaptive Ailerons
    public class AdvancedAileronsAbility : AdaptiveAileronsAbility
    {

        public override void ActivateAbility()
        {
            base.ActivateAbility();

            HostShip.DialInfo.ChangeManeuverComplexity(new ManeuverHolder(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Bank), MovementComplexity.Normal);
            HostShip.DialInfo.ChangeManeuverComplexity(new ManeuverHolder(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Bank), MovementComplexity.Normal);
        }

        public override void DeactivateAbility()
        {
            base.ActivateAbility();

            HostShip.DialInfo.ChangeManeuverComplexity(new ManeuverHolder(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Bank), MovementComplexity.Complex);
            HostShip.DialInfo.ChangeManeuverComplexity(new ManeuverHolder(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Bank), MovementComplexity.Complex);
        }
    }

    public class ControlledAileronsAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnManeuverIsReadyToBeRevealed += RegisterControlledAileronsAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnManeuverIsReadyToBeRevealed -= RegisterControlledAileronsAbility;
        }

        public void RegisterControlledAileronsAbility(GenericShip ship)
        {
            RegisterAbilityTrigger(TriggerTypes.OnManeuverIsReadyToBeRevealed, CheckCanUseAbility);
        }

        private void CheckCanUseAbility(object sender, EventArgs e)
        {
            // AI doesn't know how to boost
            if (HostShip.IsStressed || HostShip.Owner.GetType().IsSubclassOf(typeof(Players.GenericAiPlayer)))
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
            HostShip.AskPerformFreeAction(
                new BoostAction() { HostShip = TargetShip, Color = Actions.ActionColor.White },
                Triggers.FinishTrigger,
                HostShip.PilotInfo.PilotName,
                "Do you want to activate your Controlled Ailerons?",
                HostShip
            );
        }
    }
}
