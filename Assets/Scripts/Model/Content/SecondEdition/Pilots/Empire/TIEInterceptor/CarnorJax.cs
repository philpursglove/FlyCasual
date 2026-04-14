using Abilities.SecondEdition;
using ActionsList;
using Arcs;
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
        }
    }
}

namespace Abilities.SecondEdition
{
    public class CarnorJaxAbility : GenericAbility
    {
        // While an enemy ship at range 0-1 in your front arc defends or performs an attack, before the Roll Attack Dice step,
        // you may spend 1 force. If you do, that ship's dice cannot be modified.

        GenericShip savedShip;
        GenericShip targetShip;

        public override void ActivateAbility()
        {
            GenericShip.OnAttackStartAsAttackerGlobal += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnAttackStartAsAttackerGlobal -= CheckAbility;
        }

        private void CheckAbility()
        {
            targetShip = Tools.IsAnotherTeam(HostShip, Combat.Defender) ? Combat.Defender : Combat.Attacker;

            if (Tools.IsFriendly(HostShip, targetShip)) return; // Sanity check

            if (HostShip.State.Force > 0 && HostShip.GetRangeToShip(targetShip) < 2 && HostShip.SectorsInfo.IsShipInSector(targetShip, ArcType.Front))
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
            savedShip = targetShip;
            savedShip.OnTryAddAvailableDiceModification += PreventOwnDiceModification;
            Phases.Events.OnCombatPhaseEnd_NoTriggers += RemovePreventOwnDiceModification;
            HostShip.State.SpendForce(1, DecisionSubPhase.ConfirmDecision);
        }

        private void PreventOwnDiceModification(GenericShip ship, GenericAction action, ref bool canBeUsed)
        {
            if (Combat.AttackStep == CombatStep.Attack && Combat.Attacker == savedShip ||
                Combat.AttackStep == CombatStep.Defence && Combat.Defender == savedShip)
            {
                // Ability says only targetShip's dice can't be modified, nothing about modifying other ship's dice
                canBeUsed = false;
            }
        }

        private void RemovePreventOwnDiceModification()
        {
            Phases.Events.OnCombatPhaseEnd_NoTriggers -= RemovePreventOwnDiceModification;
            targetShip.OnTryAddAvailableDiceModification -= PreventOwnDiceModification;
            savedShip = null;
        }
    }
}