using Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Upgrade;
using UpgradesList.SecondEdition;
using Ship;
using SubPhases;
using Tokens;
using ActionsList;
using Abilities;

namespace UpgradesList.SecondEdition
{
    public class ForTheCause : GenericUpgrade
    {
        public ForTheCause() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "For The Cause",
                UpgradeType.Talent,
                cost: 0,
                abilityType: typeof(Abilities.SecondEdition.ForTheCauseAbility),
                legalityInfo: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal, Legality.XWA }
            );
            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    // While you defend or perform an attack, you may spend 1 non-blank result to choose a friendly strained or depleted ship at range 1-2.
    // That ship may remove 1 strain or deplete token.
    public class ForTheCauseAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnGenerateDiceModifications += ForTheCauseEffect;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnGenerateDiceModifications -= ForTheCauseEffect;
        }

        private void ForTheCauseEffect(GenericShip host)
        {
            GenericAction newAction = new ActionsList.SecondEdition.ForTheCauseEffect()
            {
                HostShip = host,
                ImageUrl = HostUpgrade.ImageUrl,
                DoDiceModification = UseAbilityAction
            };
            host.AddAvailableDiceModificationOwn(newAction);
        }

        private void UseAbilityAction(Action callback)
        {
            SelectTargetForAbility(
                AskRemoveStrainOrDeplete,
                AnotherFriendlyShipInRange,
                ShipTargetAiPriority,
                HostShip.Owner.PlayerNo,
                HostName,
                "Choose a ship, that ship will remove one strain or deplete token.",
                HostUpgrade,
                true,
                callback
            );
        }

        private bool AnotherFriendlyShipInRange(GenericShip ship)
        {
            return FilterByTargetType(ship, new List<TargetTypes>() { TargetTypes.AnyFriendly })
                && FilterTargetsByRange(ship, 1, 2)
                && (ship.Tokens.HasToken<StrainToken>() || ship.Tokens.HasToken<DepleteToken>());
        }

        private void AskRemoveStrainOrDeplete()
        {
            SubPhases.SelectShipSubPhase.FinishSelectionNoCallback();
            
            if (TargetShip.Tokens.HasToken<DepleteToken>() && TargetShip.Tokens.HasToken<StrainToken>())
            {
                AskToUseAbility(
                    "Choose token to remove",
                    AlwaysUseByDefault, // Ai currently always removes a deplete.
                    useAbility: delegate { RemoveDeplete(); },
                    dontUseAbility: delegate { RemoveStrain(); },
                    descriptionLong: "Do you want to remove Deplete token (otherwise Strain token will be removed)",
                    showSkipButton: false,
                    requiredPlayer: HostShip.Owner.PlayerNo
                );
            }
            else if (TargetShip.Tokens.HasToken<DepleteToken>())
            {
                RemoveDeplete();
            }
            else
            {
                RemoveStrain();
            }
        }

        private void RemoveStrain()
        {
            TargetShip.Tokens.SpendToken(typeof(StrainToken),()=>{});
        }

        private void RemoveDeplete()
        {
            TargetShip.Tokens.SpendToken(typeof(DepleteToken),()=>{});
        }

        private int ShipTargetAiPriority(GenericShip ship)
        {
            return 1; //todo!
        }
    }
}

namespace ActionsList.SecondEdition
{
    public class ForTheCauseEffect : GenericAction
    {
        public ForTheCauseEffect() : base()
        {
            Name = DiceModificationName =  "For The Cause";
        }

        public override void ActionEffect(Action callBack)
        {
            if (Combat.CurrentDiceRoll.HasResult(DieSide.Focus)) {
                Combat.CurrentDiceRoll.RemoveType(DieSide.Focus);
            } 
            else
            if (Combat.CurrentDiceRoll.HasResult(DieSide.Success)) {
                Combat.CurrentDiceRoll.RemoveType(DieSide.Success);
            }
            else
            {
                Combat.CurrentDiceRoll.RemoveType(DieSide.Crit);
            }
            base.ActionEffect(callBack);
        }

        public override bool IsDiceModificationAvailable()
        {
            return Combat.CurrentDiceRoll.HasResult(DieSide.Focus) || Combat.CurrentDiceRoll.HasResult(DieSide.Crit) || Combat.CurrentDiceRoll.HasResult(DieSide.Success);
        }

        public override int GetDiceModificationPriority()
        {
            return 0;
        }
    }
}
