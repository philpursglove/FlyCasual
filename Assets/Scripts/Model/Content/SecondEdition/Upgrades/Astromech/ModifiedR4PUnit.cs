using Content;
using Movement;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class ModifiedR4PUnit : GenericUpgrade
    {
        public ModifiedR4PUnit() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Modified R4-P Unit",
                UpgradeType.Astromech,
                cost: 1, //TODO fix cost
                charges: 1,
                abilityType: typeof(Abilities.SecondEdition.ModifiedR4PUnitAbility),
                restriction: new FactionRestriction(Faction.Rebel),
                legalityInfo: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            //TODO: Fix ImageUrl
            ImageUrl = "https://raw.githubusercontent.com/sampson-matt/FlyCasualLegacyCustomCards/refs/heads/main/BattleOverEndor/ModifiedR4PUnit.jpg";
        }
    }

    public class ModifiedR4PUnitXwa : ModifiedR4PUnit
    {
        public ModifiedR4PUnitXwa() : base()
        {
            UpgradeInfo.Cost = 1;
            UpgradeInfo.LegalityInfo = new List<Legality> { Legality.XWA };
            UpgradeInfo.Restrictions.AddRestriction(new FactionRestriction(Faction.Rebel));
            IsHidden = false;
        }
    }
}

namespace Abilities.SecondEdition
{
    //Before you execute a red maneuver, you may spend 1 charge. If you do, while you execute that maneuver, reduce its difficulty.
    public class ModifiedR4PUnitAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnManeuverIsRevealed += RegisterAskChangeManeuver;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnManeuverIsRevealed -= RegisterAskChangeManeuver;
        }

        private void RegisterAskChangeManeuver(GenericShip ship)
        {
            RegisterAbilityTrigger(TriggerTypes.OnMovementActivationStart, AskAbility);
        }

        private void AskAbility(object sender, EventArgs e)
        {
            if (HostUpgrade.State.Charges > 0 && HostShip.AssignedManeuver.ColorComplexity == MovementComplexity.Complex)
            {
                AskToUseAbility(
                    HostUpgrade.UpgradeInfo.Name,
                    AlwaysUseByDefault,
                    UseAbility,
                    descriptionLong: "Do you want to spend 1 Charge to reduce difficulty of your maneuver?",
                    imageHolder: HostUpgrade
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void UseAbility(object sender, EventArgs e)
        {
            if (HostUpgrade.State.Charges > 0)
            {
                HostShip.AssignedManeuver.ColorComplexity = GenericMovement.ReduceComplexity(HostShip.AssignedManeuver.ColorComplexity);
                HostUpgrade.State.SpendCharge();
            }
            SubPhases.DecisionSubPhase.ConfirmDecision();
        }
    }
}