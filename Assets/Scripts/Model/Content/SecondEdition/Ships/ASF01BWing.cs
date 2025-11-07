using Actions;
using ActionsList;
using Arcs;
using Movement;
using Ship;
using Ship.CardInfo;
using SubPhases;
using System;
using System.Collections.Generic;
using Tokens;
using UnityEngine;

namespace Ship.SecondEdition.ASF01BWing
{
    public class ASF01BWing : GenericShip
    {
        public ASF01BWing() : base()
        {
            ShipInfo = new ShipCardInfo25
            (
                "A/SF-01 B-wing",
                BaseSize.Small,
                new FactionData
                (
                    new Dictionary<Faction, System.Type>
                    {
                        { Faction.Rebel, typeof(TenNumb) }
                    }
                ),
                new ShipArcsInfo(ArcType.Front, 3), 1, 4, 4,
                new ShipActionsInfo
                (
                    new ActionInfo(typeof(FocusAction)),
                    new ActionInfo(typeof(TargetLockAction)),
                    new ActionInfo(typeof(BarrelRollAction))
                ),
                new ShipUpgradesInfo(),
                linkedActions: new List<LinkedActionInfo>
                {
                    new LinkedActionInfo(typeof(FocusAction), typeof(BarrelRollAction))
                }
            );

            DefaultUpgrades.Add(typeof(UpgradesList.SecondEdition.StabilizedSFoilsOpen));

            ModelInfo = new ShipModelInfo
            (
                "B-wing",
                "Teal",
                new Vector3(-3.33f, 6.4f, 5.55f),
                2f
            );

            DialInfo = new ShipDialInfo
            (
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Complex),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Complex),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.TallonRoll, MovementComplexity.Complex),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.TallonRoll, MovementComplexity.Complex),

                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Forward, ManeuverBearing.KoiogranTurn, MovementComplexity.Complex),

                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Complex),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Complex),

                new ManeuverInfo(ManeuverSpeed.Speed4, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Complex)
            );

            SoundInfo = new ShipSoundInfo
            (
                new List<string>()
                {
                    "XWing-Fly1",
                    "XWing-Fly2",
                    "XWing-Fly3"
                },
                "XWing-Laser", 2
            );

            ShipIconLetter = 'b';
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GyroCockpit : GenericAbility
    {
        // After you gain a stress token, you may spend 2 charges to gain an evade token.
        // When you drop a device, you may spend 1 charge to set the template with its middle line aligned with the hashmark on your ship's left or right side instead of your rear guides

        Direction selectedDirection = Direction.Bottom;

        public override void ActivateAbility()
        {
            HostShip.OnTokenIsAssigned += RegisterEvadeAbility;
            HostShip.BeforeBombWillBeDropped += RegisterDeviceDropAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnTokenIsAssigned -= RegisterEvadeAbility;
            HostShip.BeforeBombWillBeDropped -= RegisterDeviceDropAbility;
        }

        private void RegisterEvadeAbility(GenericShip ship, GenericToken token)
        {
            if (token.GetType() == typeof(StressToken))
            {
                RegisterAbilityTrigger(TriggerTypes.OnTokenIsAssigned, AskUseEvadeAbility);
            }
        }

        private void AskUseEvadeAbility(object sender, EventArgs e)
        {
            if (HostShip.State.Charges >= 2)
            {
                AskToUseAbility(
                    descriptionShort: "Do you want to spend 2 charges to gain an evade token",
                    useByDefault: NeverUseByDefault,
                    useAbility: UseEvadeAbility,
                    imageHolder: HostShip
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void UseEvadeAbility(object sender, EventArgs e)
        {
            HostShip.Tokens.AssignToken(new EvadeToken(HostShip), DecisionSubPhase.ConfirmDecision);
            HostShip.SpendCharges(2);
        }

        private void RegisterDeviceDropAbility()
        {
            if (HostShip.State.Charges > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.BeforeBombWillBeDropped, AskToUseDeviceDropAbility);
            }
        }

        private void AskToUseDeviceDropAbility(object sender, EventArgs e)
        {
            AskForDecision(
                descriptionShort: "Gyro-Cockpit",
                descriptionLong: "Spend 1 ship charge to drop device using left or right side instead of rear guides?",
                imageHolder: HostShip,
                decisions: new() {
                    { "Left", UseDeviceAbilityLeft },
                    { "Right", UseDeviceAbilityRight }
                },
                tooltips: new(),
                defaultDecision: "No",
                callback: Triggers.FinishTrigger,
                showSkipButton: true
            );
        }

        private void UseDeviceAbility()
        {
            HostShip.OnGetBombTemplateDirection += GetDeviceDirection;
            HostShip.SpendCharge();
            Triggers.FinishTrigger();
        }

        private void UseDeviceAbilityLeft(object sender, EventArgs e)
        {
            selectedDirection = Direction.Left;
            UseDeviceAbility();
        }

        private void UseDeviceAbilityRight(object sender, EventArgs e)
        {
            selectedDirection = Direction.Right;
            UseDeviceAbility();
        }

        private void GetDeviceDirection(ref Direction direction)
        {
            HostShip.OnGetBombTemplateDirection -= GetDeviceDirection;
            direction = selectedDirection;
            selectedDirection = Direction.Bottom;
        }
    }
}
