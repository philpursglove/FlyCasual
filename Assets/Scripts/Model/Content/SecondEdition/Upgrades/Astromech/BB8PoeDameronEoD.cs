using ActionsList;
using Content;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class BB8PoeDameronEoD : GenericUpgrade
    {
        public BB8PoeDameronEoD() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "BB-8",
                UpgradeType.Astromech,
                charges: 2,
                cost: 0,
                isLimited: true,
                restriction: new FactionRestriction(Faction.Resistance),
                abilityType: typeof(Abilities.SecondEdition.BB8PoeDameronEoDAbility),
                legalityInfo: new() { Legality.XWA }
            );

            IsHidden = true;
            ImageUrl = "https://infinitearenas.com/xw2xwa/images/pilots/poedameron-evacuationofdqar.png";
        }
    }
}

namespace Abilities.SecondEdition
{
    // During the System Phase, you may spend 1 charge to perform a barrel roll or boost action.
    // Before you engage, you may spend 1 charge and gain a strain token. If you do, you may remove 1 disarm token.
    public class BB8PoeDameronEoDAbility : GenericAbility
    {
        protected List<GenericAction> AbilityActions = new() { new BarrelRollAction(), new BoostAction() };
        protected GenericShip selectedShip;

        public override void ActivateAbility()
        {
            HostShip.OnSystemsPhaseStart += PlanAction;
            HostShip.OnCombatActivation += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnSystemsPhaseStart -= PlanAction;
            HostShip.OnCombatActivation -= RegisterAbility;
        }

        private void PlanAction(GenericShip ship)
        {
            RegisterAbilityTrigger(TriggerTypes.OnSystemsPhaseStart, AskPerformAction);
        }

        private void AskPerformAction(object sender, EventArgs e)
        {
            if (HostUpgrade.State.Charges < 1)
            {
                Triggers.FinishTrigger();
                return;
            }

            HostShip.OnActionIsPerformed += SpendCharge;

            Selection.ThisShip = HostShip; // System phase doesn't have an active ship set yet

            HostShip.AskPerformFreeAction(
                AbilityActions,
                CleanUp,
                HostUpgrade.UpgradeInfo.Name,
                "You may spend 1 Charge to perform a Barrel Roll action",
                HostUpgrade
            );
        }

        private void SpendCharge(GenericAction action)
        {
            Sounds.PlayShipSound("BB-8-Sound");
            HostUpgrade.State.SpendCharge();
        }

        private void CleanUp()
        {
            HostShip.OnActionIsPerformed -= SpendCharge;
            Triggers.FinishTrigger();
        }

        private void RegisterAbility(GenericShip ship)
        {
            RegisterAbilityTrigger(TriggerTypes.OnCombatActivation, CheckWeaponsDisabled);
        }

        private void CheckWeaponsDisabled(object sender, EventArgs e)
        {
            if (HostShip.Tokens.HasToken<WeaponsDisabledToken>() && HostUpgrade.State.Charges > 0)
            {
                AskToUseAbility(
                    HostUpgrade.UpgradeInfo.Name,
                    AlwaysUseByDefault,
                    UseAbility,
                    descriptionLong: "Do you want to spend 1 charge and receive 1 Strain Token to remove Disarm Token?",
                    imageHolder: HostUpgrade
                );
            } else
            {
                Triggers.FinishTrigger();
            }
        }

        private void UseAbility(object sender, System.EventArgs e)
        {
            Messages.ShowInfo(HostShip.PilotInfo.PilotName + " recieved Strain token to remove a Disarm Token");

            HostShip.Tokens.RemoveToken(
                typeof(WeaponsDisabledToken),
                delegate
                {
                    // Do we want to play a sound here? todo!()
                    HostUpgrade.State.SpendCharge();
                    HostShip.Tokens.AssignToken(typeof(StrainToken), DecisionSubPhase.ConfirmDecision);
                }
            );
        }
    }
}