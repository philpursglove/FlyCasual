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

namespace Ship.SecondEdition.TIEDDefender
{
    public class TIEDDefender : GenericShip
    {
        public TIEDDefender() : base()
        {
            ShipInfo = new ShipCardInfo25
            (
                "TIE/D Defender",
                BaseSize.Small,
                new FactionData
                (
                    new Dictionary<Faction, Type>
                    {
                        { Faction.Imperial, typeof(CountessRyad) }
                    }
                ),
                new ShipArcsInfo(ArcType.Front, 3), 3, 3, 4,
                new ShipActionsInfo
                (
                    new ActionInfo(typeof(FocusAction)),
                    new ActionInfo(typeof(BarrelRollAction)),
                    new ActionInfo(typeof(TargetLockAction)),
                    new ActionInfo(typeof(EvadeAction)),
                    new ActionInfo(typeof(BoostAction))
                ),
                new ShipUpgradesInfo()
            );

            ShipAbilities.Add(new Abilities.SecondEdition.FullThrottleAbility());

            ModelInfo = new ShipModelInfo
            (
                "TIE Defender",
                "Gray",
                new Vector3(-3.7f, 7.9f, 5.55f),
                2f
            );

            DialInfo = new ShipDialInfo
            (
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Complex),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Complex),

                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Complex),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Complex),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Forward, ManeuverBearing.KoiogranTurn, MovementComplexity.Complex),

                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),

                new ManeuverInfo(ManeuverSpeed.Speed4, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed4, ManeuverDirection.Forward, ManeuverBearing.KoiogranTurn, MovementComplexity.Normal),

                new ManeuverInfo(ManeuverSpeed.Speed5, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy)
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

            ShipIconLetter = 'D';
        }
    }
}


namespace Abilities.SecondEdition
{
    public class FullThrottleAbility : GenericAbility
    {
        public override string Name { get { return "Full Throttle"; } }

        public override void ActivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully += CheckTIEx7Ability;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully -= CheckTIEx7Ability;
        }

        private void CheckTIEx7Ability(GenericShip ship)
        {
            if (ship.AssignedManeuver.Speed > 2)
            {
                Triggers.RegisterTrigger(new Trigger()
                {
                    Name = "Full Throttle",
                    TriggerType = TriggerTypes.OnMovementFinish,
                    TriggerOwner = HostShip.Owner.PlayerNo,
                    EventHandler = AskTIEx7Ability,
                    Sender = HostUpgrade,
                });
            }
        }

        private void AskTIEx7Ability(object sender, System.EventArgs e)
        {
            if (Selection.ThisShip.CanPerformAction(new EvadeAction()))
            {
                TIEx7DecisionSubPhase newSubPhase = (TIEx7DecisionSubPhase)Phases.StartTemporarySubPhaseNew("TIE/x7 decision", typeof(TIEx7DecisionSubPhase), Triggers.FinishTrigger);
                newSubPhase.AbilityInstance = this;
                newSubPhase.Start();
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        public bool IsAlwaysUseAbility()
        {
            return alwaysUseAbility;
        }

        public void SetIsAlwaysUseAbility()
        {
            alwaysUseAbility = true;
        }
    }

    public class ChissEngineeringAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully += CheckTargetLockAbility;
            HostShip.OnAttackStartAsAttacker += RegisterAttackAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully -= CheckTargetLockAbility;
            HostShip.OnAttackStartAsAttacker -= RegisterAttackAbility;
        }

        private void RegisterAttackAbility()
        {
            if (HostShip.State.ShieldsCurrent < 1)
                return;

            if (HostShip.IsStressed)
                return;

            if (Combat.ShotInfo.Range == 1)
                return;

            RegisterAbilityTrigger(TriggerTypes.OnAttackStart, delegate
            {
                AskToUseAbility(
                    "Chiss Engineering",
                    AlwaysUseByDefault,
                    UseAttackAbility,
                    descriptionLong: "Do you want to spend 1 Shield to apply the range 1 bonus?",
                    imageHolder: HostShip
                );
            });
        }

        private void UseAttackAbility(object sender, EventArgs e)
        {
            Rules.DistanceBonus.OnCheckAllowRangeOneBonus += ApplyRangeOneBonus;
            HostShip.LoseShield();
            DecisionSubPhase.ConfirmDecision();
        }

        private void ApplyRangeOneBonus(ref bool isActive)
        {
            Rules.DistanceBonus.OnCheckAllowRangeOneBonus -= ApplyRangeOneBonus;

            Messages.ShowInfo($"{HostShip.PilotInfo.PilotName}: Spent 1 Shield to apply the Range 1 bonus");
            isActive = true;
        }


        private void CheckTargetLockAbility(GenericShip ship)
        {
            if (ship.AssignedManeuver.Speed > 2)
            {
                Triggers.RegisterTrigger(new Trigger()
                {
                    Name = "Chiss Engineering",
                    TriggerType = TriggerTypes.OnMovementFinish,
                    TriggerOwner = HostShip.Owner.PlayerNo,
                    EventHandler = AskPerformLockAction,
                    Sender = HostReal,
                });
            }
        }

        private void AskPerformLockAction(object sender, System.EventArgs e)
        {
            HostShip.AskPerformFreeAction(
                new TargetLockAction(),
                Triggers.FinishTrigger,
                "Chiss Engineering",
                "After you fully execute a speed 3-5 maneuver, you may perform a Lock action",
                HostShip
            );
        }
    }
}

namespace SubPhases
{
    public class TIEx7DecisionSubPhase : DecisionSubPhase
    {
        public Abilities.SecondEdition.FullThrottleAbility AbilityInstance;

        public override void PrepareDecision(Action callBack)
        {
            DescriptionShort = AbilityInstance.Name;
            DescriptionLong = "Do you want to perform an Evade action?";

            AddDecision("Yes", PerformFreeEvadeAction);
            AddDecision("No", DontPerformFreeEvadeAction);
            AddDecision("Always", AlwaysPerformFreeEvadeAction);

            DefaultDecisionName = "Yes";

            if (!AbilityInstance.IsAlwaysUseAbility())
            {
                callBack();
            }
            else
            {
                PerformFreeEvadeAction(null, null);
            }
        }

        private void PerformFreeEvadeAction(object sender, EventArgs e)
        {
            Selection.ThisShip.AskPerformFreeAction(
                new EvadeAction(),
                DecisionSubPhase.ConfirmDecision,
                DescriptionShort,
                DescriptionLong,
                AbilityInstance.HostShip,
                isForced: true
            );
        }

        private void DontPerformFreeEvadeAction(object sender, EventArgs e)
        {
            ConfirmDecision();
        }

        private void AlwaysPerformFreeEvadeAction(object sender, EventArgs e)
        {
            AbilityInstance.SetIsAlwaysUseAbility();

            PerformFreeEvadeAction(sender, e);
        }
    }
}
