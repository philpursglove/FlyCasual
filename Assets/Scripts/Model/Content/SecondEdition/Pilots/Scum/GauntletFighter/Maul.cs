using Actions;
using ActionsList;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.GauntletFighter
    {
        public class Maul : GauntletFighter
        {
            public Maul() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Maul",
                    "Lord of the Shadow Collective",
                    Faction.Scum,
                    5,
                    8,
                    15,
                    force: 3,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.MaulAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.ForcePower,
                        UpgradeType.Talent,
                        UpgradeType.Crew,
                        UpgradeType.Gunner,
                        UpgradeType.Device,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Modification,
                        UpgradeType.Configuration,
                        UpgradeType.Title
                    },
                    tags: new List<Tags>()
                    {
                        Tags.DarkSide
                    },
                    skinName: "Red Old",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class MaulXWA : Maul
        {
            public MaulXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 18;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 18;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                    UpgradeType.ForcePower,
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                    UpgradeType.Device,
                    UpgradeType.Configuration,
                    UpgradeType.Title
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class MaulAbility : GenericAbility
    {
        private List<GenericShip> TargetShips = new List<GenericShip>();
        public override void ActivateAbility()
        {
            HostShip.BeforeActionIsPerformed += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.BeforeActionIsPerformed -= CheckAbility;
        }

        private void CheckAbility(GenericAction action, ref bool isFree)
        {
            if (action is CoordinateAction && HostShip.State.Force > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.BeforeActionIsPerformed, AskToTreatAsWhite);
            }
        }

        private void AskToTreatAsWhite(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostShip.PilotName,
                NeverUseByDefault,
                TreatActionAsWhite,
                descriptionLong: "Do you want to spend 1 Force and treat action as white? (If you do, you may coordinate up to 2 ships with lower initiative lower than yours assigning 1 strain token to each)",
                imageHolder: HostShip
            );
        }

        private void TreatActionAsWhite(object sender, EventArgs e)
        {
            HostShip.OnCheckActionColor += TreatThisCoordinateActionAsWhite;
            HostShip.OnCheckCoordinateModeModification += SetCustomCoordinateMode;
            HostShip.OnCoordinateTargetIsSelected += CoordinateShipSelected;
            HostShip.Tokens.SpendToken(typeof(ForceToken), SubPhases.DecisionSubPhase.ConfirmDecision);
        }

        private void CoordinateShipSelected(GenericShip ship)
        {
            TargetShips.Add(ship);
            if (TargetShips.Count == 1)
            {
                HostShip.OnActionIsPerformed += RegisterAbility;
            }
        }

        private void RegisterAbility(GenericAction action)
        {
            HostShip.OnActionIsPerformed -= RegisterAbility;
            HostShip.OnCoordinateTargetIsSelected -= CoordinateShipSelected;
            RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AssignStrain);
        }

        private void AssignStrain(object sender, EventArgs e)
        {
            foreach (GenericShip ship in TargetShips)
            {
                ship.Tokens.AssignToken(typeof(StrainToken), delegate { });
            }
            TargetShips.Clear();
            Triggers.FinishTrigger();
        }

        private void SetCustomCoordinateMode(ref CoordinateActionData coordinateActionData)
        {
            coordinateActionData.MaxTargets = 2;
            coordinateActionData.TargetLowerInitiave = true;

            HostShip.OnCheckCoordinateModeModification -= SetCustomCoordinateMode;
        }

        private void TreatThisCoordinateActionAsWhite(GenericAction action, ref ActionColor color)
        {
            if (action is CoordinateAction && color == ActionColor.Red)
            {
                color = ActionColor.White;
                HostShip.OnCheckActionColor -= TreatThisCoordinateActionAsWhite;
            }
        }
    }
}