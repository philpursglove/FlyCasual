using Content;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class DeuteriumPowerCells : GenericUpgrade
    {
        public DeuteriumPowerCells() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Deuterium Power Cells",
                types: new List<UpgradeType> { UpgradeType.Tech, UpgradeType.Modification },
                cost: 6,
                charges: 2,
                restriction: new FactionRestriction(Faction.FirstOrder),
                abilityType: typeof(Abilities.SecondEdition.DeuteriumPowerCellsAbility),
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class DeuteriumPowerCellsXWA : DeuteriumPowerCells
    {
        public DeuteriumPowerCellsXWA() : base()
        {
            UpgradeInfo.Cost = 7;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    // During the System Phase, you may spend 1 charge and gain 1 disarm token to recover 1 shield.
    // Before you would gain 1 non-lock token, if you are not stressed, you may spend 1 charge to gain 1 stress token instead.

    public class DeuteriumPowerCellsAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnSystemsAbilityActivation += CheckRegenerationAbility;
            HostShip.OnCheckSystemsAbilityActivation += CheckAbility;
            HostShip.BeforeTokenIsAssigned += CheckTokenProtection;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnSystemsAbilityActivation -= CheckRegenerationAbility;
            HostShip.OnCheckSystemsAbilityActivation -= CheckAbility;
            HostShip.BeforeTokenIsAssigned -= CheckTokenProtection;
        }

        private void CheckAbility(GenericShip ship, ref bool isAbilityActive)
        {
            isAbilityActive = ((HostShip.State.ShieldsCurrent < HostShip.State.ShieldsMax) && (HostUpgrade.State.Charges > 0));
        }

        private void CheckTokenProtection(GenericShip ship, GenericToken token)
        {
            if (!HostShip.IsStressed && HostUpgrade.State.Charges > 0
                && token is not GenericTargetLockToken
                && token is not StressToken
            )
            {
                RegisterAbilityTrigger(TriggerTypes.OnBeforeTokenIsAssigned, AskToReplaceToken);
            }
        }

        private void AskToReplaceToken(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostUpgrade.UpgradeInfo.Name,
                NeverUseByDefault,
                DoReplaceToken,
                descriptionLong: "Do you want to spend 1 charge to gain 1 stress token instead of " + HostShip.Tokens.TokenToAssign.Name + "?",
                imageHolder: HostUpgrade,
                requiredPlayer: HostShip.Owner.PlayerNo,
                callback: Triggers.FinishTrigger
            );
        }

        private void DoReplaceToken(object sender, EventArgs e)
        {
            HostUpgrade.State.SpendCharge();

            Messages.ShowInfo(HostUpgrade.UpgradeInfo.Name + ": Stress token is assigned instead of planned token");

            HostShip.Tokens.AssignToken(typeof(Tokens.StressToken), delegate
            {
                HostShip.Tokens.TokenToAssign = null;
                DecisionSubPhase.ConfirmDecision();
            });
        }

        private void CheckRegenerationAbility(GenericShip ship)
        {
            if ((HostShip.State.ShieldsCurrent < HostShip.State.ShieldsMax) && (HostUpgrade.State.Charges > 0))
            {
                RegisterAbilityTrigger(TriggerTypes.OnSystemsAbilityActivation, AskToRegen);
            }
        }

        private void AskToRegen(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostUpgrade.UpgradeInfo.Name,
                NeverUseByDefault,
                DoRegen,
                descriptionLong: "Do you want to spend 1 charge and gain 1 disarm token to recover 1 shield?",
                imageHolder: HostUpgrade,
                callback: Triggers.FinishTrigger
            );
        }

        private void DoRegen(object sender, EventArgs e)
        {
            HostUpgrade.State.SpendCharge();

            HostShip.Tokens.AssignToken(
                typeof(WeaponsDisabledToken),
                delegate
                {
                    HostShip.TryRegenShields();
                    Messages.ShowInfo(HostUpgrade.UpgradeInfo.Name + ": " + HostShip.PilotInfo.PilotName + " recovered 1 shield");
                    DecisionSubPhase.ConfirmDecision();
                }
            );
        }
    }
}