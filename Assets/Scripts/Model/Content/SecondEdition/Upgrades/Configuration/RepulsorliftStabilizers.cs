using Movement;
using Ship;
using SubPhases;
using System;
using System.Linq;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class RepulsorliftStabilizersInactive : GenericDualUpgrade
    {
        public RepulsorliftStabilizersInactive() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Repulsorlift Stabilizers (Inactive)",
                UpgradeType.Configuration,
                cost: 0,
                restriction: new ShipRestriction(typeof(Ship.SecondEdition.HMPDroidGunship.HMPDroidGunship)),
                abilityType: typeof(Abilities.SecondEdition.RepulsorliftStabilizersInactiveAbility)
            );
            SelectSideOnSetup = false;
            AnotherSide = typeof(RepulsorliftStabilizersActive);
            NameCanonical = "repulsorliftstabilizers";
        }
    }

    public class RepulsorliftStabilizersActive : GenericDualUpgrade
    {
        public RepulsorliftStabilizersActive() : base()
        {
            IsHidden = true;

            UpgradeInfo = new UpgradeCardInfo(
                "Repulsorlift Stabilizers (Active)",
                UpgradeType.Configuration,
                cost: 0,
                restriction: new ShipRestriction(typeof(Ship.SecondEdition.HMPDroidGunship.HMPDroidGunship)),
                abilityType: typeof(Abilities.SecondEdition.RepulsorliftStabilizersActiveAbility)
            );

            IsSecondSide = true;
            AnotherSide = typeof(RepulsorliftStabilizersInactive);
            NameCanonical = "repulsorliftstabilizers-sideb";
        }
    }
}

namespace Abilities.SecondEdition
{
    public class RepulsorliftStabilizersInactiveAbility : GenericAbility
    {
        public bool JustFlipped { get; set; }

        public override void ActivateAbility()
        {
            HostShip.AfterGetManeuverColorDecreaseComplexity += ApplyAbility;
            HostShip.OnMovementFinishSuccessfully += RegisterTrigger;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGetManeuverColorDecreaseComplexity -= ApplyAbility;
            HostShip.OnMovementFinishSuccessfully -= RegisterTrigger;
        }

        private void ApplyAbility(GenericShip ship, ref ManeuverHolder movement)
        {
            if (movement.Bearing == ManeuverBearing.Straight)
            {
                movement.ColorComplexity = GenericMovement.ReduceComplexity(movement.ColorComplexity);
                // Update revealed dial in UI
                Roster.UpdateAssignedManeuverDial(HostShip, HostShip.AssignedManeuver);
                Messages.ShowInfoToHuman("Repulsorlift Stabilizers (Inactive): Difficulty of straight maneuvers is reduced");
            }
        }

        private void RegisterTrigger(GenericShip ship)
        {
            if (!JustFlipped)
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, AskToFlip);
            }
            else
            {
                JustFlipped = false;
            }
        }

        private void AskToFlip(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostUpgrade.UpgradeInfo.Name,
                NeverUseByDefault,
                DoFlipSide,
                descriptionLong: "Do you want to flip this card?",
                imageHolder: HostUpgrade
            );
        }

        protected void DoFlipSide(object sender, EventArgs e)
        {
            (HostUpgrade as GenericDualUpgrade).Flip();
            DecisionSubPhase.ConfirmDecision();
        }
    }

    public class RepulsorliftStabilizersActiveAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnManeuverIsRevealed += CheckRevealedManeuved;
            HostShip.OnMovementFinishSuccessfully += RegisterMovementFinishTrigger;
            HostShip.OnMovementFinish += CheckMovementType;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnManeuverIsRevealed -= CheckRevealedManeuved;
            HostShip.OnMovementFinishSuccessfully -= RegisterMovementFinishTrigger;
            HostShip.OnMovementFinish -= CheckMovementType;
        }

        private void CheckRevealedManeuved(GenericShip ship)
        {
            if (ship.RevealedManeuver != null
            )
            {
                if (ship.RevealedManeuver.Bearing == ManeuverBearing.Bank)
                {
                    GenericMovement movement = new SideslipBankMovement(
                        HostShip.RevealedManeuver.Speed,
                        HostShip.RevealedManeuver.Direction,
                        ManeuverBearing.SideslipBank,
                        HostShip.RevealedManeuver.ColorComplexity
                    );

                    Messages.ShowInfo("Maneuver is changed to Sideslip");
                    HostShip.SetAssignedManeuver(movement);
                }
                else if (ship.RevealedManeuver.Bearing == ManeuverBearing.Turn)
                {
                    GenericMovement movement = new SideslipTurnMovement(
                        HostShip.RevealedManeuver.Speed,
                        HostShip.RevealedManeuver.Direction,
                        ManeuverBearing.SideslipTurn,
                        HostShip.RevealedManeuver.ColorComplexity
                    );

                    Messages.ShowInfo("Maneuver is changed to Sideslip");
                    HostShip.SetAssignedManeuver(movement);
                }
            }
        }

        private void RegisterMovementFinishTrigger(GenericShip ship)
        {
            if (HostShip.AssignedManeuver.Bearing != ManeuverBearing.SideslipBank
                && HostShip.AssignedManeuver.Bearing != ManeuverBearing.SideslipTurn)
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, AskToFlip);
            }
        }

        private void AskToFlip(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostUpgrade.UpgradeInfo.Name,
                NeverUseByDefault,
                DoFlipSide,
                descriptionLong: "Do you want to flip this card?",
                imageHolder: HostUpgrade
            );
        }

        protected void DoFlipSide(object sender, EventArgs e)
        {
            (HostUpgrade as GenericDualUpgrade).Flip();
            DecisionSubPhase.ConfirmDecision();
        }

        private void CheckMovementType(GenericShip ship)
        {
            if (ship.AssignedManeuver.Bearing == ManeuverBearing.SideslipBank
                || ship.AssignedManeuver.Bearing == ManeuverBearing.SideslipTurn)
            {
                (HostUpgrade as GenericDualUpgrade).Flip();
                ((HostShip.UpgradeBar.GetInstalledUpgrade(UpgradeType.Configuration) as UpgradesList.SecondEdition.RepulsorliftStabilizersInactive)
                    .UpgradeAbilities.First() as RepulsorliftStabilizersInactiveAbility)
                    .JustFlipped = true;
            }
        }
    }
}
