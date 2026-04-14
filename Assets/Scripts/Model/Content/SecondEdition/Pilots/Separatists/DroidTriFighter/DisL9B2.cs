using Abilities.SecondEdition;
using ActionsList;
using Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ship.SecondEdition.DroidTriFighter
{
    public class DisL9B2 : DroidTriFighter
    {
        public DisL9B2() : base()
        {
            PilotInfo = new PilotCardInfo25(
                pilotName: "DIS-L9B2",
                pilotTitle: "Calculated Risk",
                faction: Faction.Separatists,
                initiative: 6,
                cost: 50, // TODO: Update
                loadoutValue: 50, // TODO: Update
                isLimited: true,
                charges: 1,
                regensCharges: 1,
                abilityType: typeof(DisL9B2Ability),
                legality: new List<Legality>() { Legality.XWA }
            );

            ShipInfo.ActionIcons.RemoveLinkedAction(typeof(BarrelRollAction), typeof(EvadeAction));
            ShipInfo.ActionIcons.AddLinkedAction(new Actions.LinkedActionInfo(typeof(BarrelRollAction), typeof(CalculateAction)));
        }
    }
}

namespace Abilities.SecondEdition
{
    // At the start of the Engagement Phase, if there is an enemy ship in your bullseye, you MUST spend 1 charge.
    // During the Engagement Phase, if your charge is active, treat your initiative value as 1.

    public class DisL9B2Ability : GenericAbility
    {
        public override void ActivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers -= RegisterAbility;
        }

        private void RegisterAbility()
        {
            RegisterAbilityTrigger(TriggerTypes.OnCombatPhaseStart, CheckChargeState);
        }

        private void CheckChargeState(object sender, EventArgs e)
        {
            if(HostShip.State.Charges > 0)
            {
                if (HasShipInBullseye())
                {
                    HostShip.SpendCharge();
                }
                else
                {
                    Messages.ShowInfoToHuman($"{HostShip.PilotInfo.PilotName} engages at initiative 1.");
                    HostShip.State.CombatActivationAtInitiative = 1;
                }                
            }

            Triggers.FinishTrigger();
        }

        private bool HasShipInBullseye()
        {
            return Roster.AllShips.Values.Any(s => HostShip.SectorsInfo.IsShipInSector(s, Arcs.ArcType.Bullseye));
        }
    }
}