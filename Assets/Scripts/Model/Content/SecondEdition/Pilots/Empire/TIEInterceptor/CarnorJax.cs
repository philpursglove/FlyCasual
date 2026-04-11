using Abilities.SecondEdition;
using ActionsList;
using Arcs;
using BoardTools;
using Content;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIEInterceptor
{
    public class CarnorJax : TIEInterceptor
    {
        public CarnorJax() : base()
        {
            // TODO: Update points, loadout, Upgrade Icons, and Tags
            PilotInfo = new PilotCardInfo25
            (
                "Carnor Jax",
                "Thyrsian Sun Guard",
                Faction.Imperial,
                5,
                50,
                14,
                isLimited: true,
                force: 1,
                regensForce: 1,
                abilityType: typeof(CarnorJaxAbility),
                extraUpgradeIcons: new List<UpgradeType>()
                {
                },
                tags: new List<Tags>
                {
                },
                legality: new List<Legality> { Legality.XWA }
            );

            //IsWIP = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class CarnorJaxAbility : GenericAbility
    {
        // While an enemy ship at range 0-1 in your front arc defends or performs an attack, before the Roll Attack Dice step,
        // you may spend 1 force. If you do, that ship's dice cannot be modified.

        GenericShip targetShip;

        public override void ActivateAbility()
        {
            GenericShip.OnAttackStartAsAttackerGlobal += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnAttackStartAsAttackerGlobal += CheckAbility;
        }

        private void CheckAbility()
        {
            targetShip = Tools.IsAnotherTeam(HostShip, Combat.Defender) ? targetShip = Combat.Defender : Combat.Attacker;

            if (Tools.IsFriendly(HostShip, targetShip)) return; // Possible if abilities temporarily turn friendlies into enemies

            ShotInfoArc shotArc = new(HostShip, targetShip, new ArcFront(HostShip.ShipBase));

            if (HostShip.GetRangeToShip(targetShip) < 2 && shotArc.InArc)
            {
                RegisterAbilityTrigger(TriggerTypes.OnAttackStart, AskUseAbility);
            }
        }

        private void AskUseAbility(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostShip.PilotInfo.PilotName,
                AlwaysUseByDefault,
                UseCarnorJaxAbility,
                callback: Triggers.FinishTrigger,
                descriptionLong: $"You may spend 1 force to prevent {targetShip.PilotInfo.PilotName}'s dice from being modified.",
                imageHolder: HostShip
            );
        }

        private void UseCarnorJaxAbility(object sender, EventArgs e)
        {
            targetShip.OnTryAddAvailableDiceModification += PreventOwnDiceModification;
            Phases.Events.OnRoundEnd += RemovePreventOwnDiceModification;
            HostShip.State.SpendForce(1, DecisionSubPhase.ConfirmDecision);
        }

        private void PreventOwnDiceModification(GenericShip ship, GenericAction action, ref bool canBeUsed)
        {
            canBeUsed = ship != targetShip && canBeUsed;
        }

        private void RemovePreventOwnDiceModification()
        {
            Phases.Events.OnRoundEnd -= RemovePreventOwnDiceModification;
            targetShip.OnTryAddAvailableDiceModification -= PreventOwnDiceModification;
            targetShip = null;
        }
    }
}