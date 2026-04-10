using Content;
using SubPhases;
using System;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class Captive : GenericUpgrade
    {
        public Captive() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Captive",
                UpgradeType.Crew,
                cost: 1, //TODO Fix cost
                isLimited: true,
                restriction: new FactionRestriction(Faction.Imperial, Faction.FirstOrder, Faction.Separatists),
                abilityType: typeof(Abilities.SecondEdition.CaptiveCrewAbility),
                legalityInfo: new List<Legality> { Legality.XWA },
                charges: 1,
                regensCharges: true,
                limited: 1
            );

            //TODO ImageUrl
        }
    }
}

namespace Abilities.SecondEdition
{
    public class CaptiveCrewAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackStartAsDefender += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsDefender -= CheckAbility;
        }

        private void CheckAbility()
        {
            if (HostUpgrade.State.Charges > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.OnAttackStart, AskToDeplete);
            }
        }

        private void AskToDeplete(object sender, EventArgs e)
        {
            AskToUseAbility("Captive",
                AlwaysUseByDefault,
                DepleteAttacker,
                descriptionLong: "Do you want to assign a Deplete token to the attacker?",
                imageHolder: HostUpgrade,
                callback: Triggers.FinishTrigger);

        }

        private void DepleteAttacker(object sender, EventArgs e)
        {
            Combat.Attacker.Tokens.AssignToken(typeof(Tokens.DepleteToken), DecisionSubPhase.ConfirmDecision);
            HostUpgrade.State.SpendCharge();
        }
    }
}