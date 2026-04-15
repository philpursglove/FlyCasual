using Abilities.SecondEdition;
using ActionsList;
using Content;
using Movement;
using Ship;
using System;
using System.Collections.Generic;

namespace Ship.SecondEdition.VCX100LightFreighter
{
    public class HeraSyndullaLegendsAndRelics : VCX100LightFreighter
    {
        public HeraSyndullaLegendsAndRelics() : base()
        {
            PilotInfo = new PilotCardInfo25(
                pilotName: "Hera Syndulla",
                pilotTitle: "New Republic General",
                faction: Faction.Rebel,
                initiative: 6,
                cost: 25, // TODO: Update
                loadoutValue: 50, // TODO: Update
                isLimited: true,
                abilityType: typeof(HeraSyndullaLegendsAndRelicsAbility),
                legality: new List<Legality>() { Legality.XWA }
            );

            PilotNameCanonical = "herasyndulla-legendsandrelics";
        }
    }
}

namespace Abilities.SecondEdition
{
    public class HeraSyndullaLegendsAndRelicsAbility : GenericAbility
    {
        // After you fully execute a red maneuver, you may coordinate a friendly ship at range 1-3.

        public override void ActivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully += RegisterAbility;
        }

        private void RegisterAbility(GenericShip ship)
        {
            if (HostShip.AssignedManeuver.ColorComplexity == MovementComplexity.Complex)
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, AskUseAbility);
            }
        }

        private void AskUseAbility(object sender, EventArgs e)
        {
            HeraSyndullaLegendsAndRelicsCoordinateAction action = new();
            action.IsRealAction = false;

            HostShip.AskPerformFreeAction(
                action,
                Triggers.FinishTrigger,
                HostShip.PilotInfo.PilotName,
                descriptionLong: $"You may coordinate a friendly ship at range 1-3."
            );
        }

        private class HeraSyndullaLegendsAndRelicsCoordinateAction : CoordinateAction
        {
            public HeraSyndullaLegendsAndRelicsCoordinateAction() : base()
            {
                IsRealAction = false;
            }
        }
    }
}