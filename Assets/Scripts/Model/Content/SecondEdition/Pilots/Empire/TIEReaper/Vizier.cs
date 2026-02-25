using Abilities.SecondEdition;
using ActionsList;
using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEReaper
    {
        public class Vizier : TIEReaper
        {
            public Vizier() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "\"Vizier\"",
                    "Ruthless Tactician",
                    Faction.Imperial,
                    2,
                    4,
                    12,
                    isLimited: true,
                    abilityType: typeof(VizierAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Crew,
                        UpgradeType.Crew,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    seImageNumber: 115,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class VizierXWA : Vizier
        {
            public VizierXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class VizierAbility : GenericAbility
    {
        private bool RestrictedAbilityIsActivated;

        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= CheckAbility;
        }

        private void CheckAbility(GenericAction action)
        {
            if (action is ControlledAileronsAction)
            {
                RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AskToPerformCoordinate);
            }
        }

        private void AskToPerformCoordinate(object sender, System.EventArgs e)
        {
            RestrictedAbilityIsActivated = true;
            HostShip.OnActionIsPerformed += CheckActionRestriction;
            HostShip.OnMovementStart += ClearRestrictedAbility;

            HostShip.AskPerformFreeAction(
                new VizierCoordinateAction() { HostShip = HostShip },
                Triggers.FinishTrigger,
                HostShip.PilotInfo.PilotName,
                "After you perform a boost using Controlled Ailerons, you may perform a Coordinate action. If you do, skip your Perform Action step.",
                HostShip
            );
        }

        private void CheckActionRestriction(GenericAction action)
        {
            if (action is VizierCoordinateAction && RestrictedAbilityIsActivated)
            {
                Messages.ShowInfo($"{HostShip.PilotInfo.PilotName} skips their Perform Action step");
                HostShip.IsSkipsActionSubPhase = true;
            }
        }

        private void ClearRestrictedAbility(GenericShip ship)
        {
            HostShip.OnMovementStart -= ClearRestrictedAbility;
            HostShip.OnActionIsPerformed -= CheckActionRestriction;

            RestrictedAbilityIsActivated = false;
        }
    }
}

namespace ActionsList
{
    public class VizierCoordinateAction : CoordinateAction
    {
        public VizierCoordinateAction() : base() { }
    }
}