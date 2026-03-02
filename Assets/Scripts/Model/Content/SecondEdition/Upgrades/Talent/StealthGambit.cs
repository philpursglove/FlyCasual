using Arcs;
using Ship;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class StealthGambit : GenericUpgrade
    {
        public StealthGambit() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Stealth Gambit",
                UpgradeType.Talent,
                abilityType: typeof(Abilities.SecondEdition.StealthGambitAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class StealthGambitAbility : GenericAbility
    {
        public bool PerformedRegularAttack { get; set; }

        public override void ActivateAbility()
        {
            GenericShip.OnAttackHitAsAttackerGlobal += CheckAbility;
        }

        private void CheckAbility()
        {
            // Is the attacker on my side and is the defender in my front arc
            if (Combat.Attacker.Owner.PlayerNo == HostShip.Owner.PlayerNo & BoardTools.Board.IsShipInArcByType(HostShip, Combat.Defender, ArcType.Front))
            {
                {
                    // Are we cloaked
                    if (HostShip.IsCloaked)
                    {
                        // Offer to spend a charge to decloak and gain a strain token
                        AskToUseAbility("Stealth Gambit",
                            NeverUseByDefault,
                            descriptionLong: "You may spend 1 charge to decloak and gain 1 strain token",
                            useAbility: delegate { UseStealthGambit(); }
                        );
                    }
                }
            }
        }

        private void UseStealthGambit()
        {
            HostShip.Tokens.AssignToken(new StrainToken(HostShip), delegate
            {
                HostShip.Tokens.RemoveToken(typeof(CloakToken), delegate
                {
                    HostUpgrade.State.SpendCharge();

                    // ship may perform a bonus attack
                    HostShip.OnCombatCheckExtraAttack += StartBonusAttack;
                });
            });
        }

        private void StartBonusAttack(GenericShip ship)
        {
            HostShip.OnCombatCheckExtraAttack -= StartBonusAttack;
            PerformedRegularAttack = HostShip.IsAttackPerformed;

            RegisterAbilityTrigger(TriggerTypes.OnCombatCheckExtraAttack, RegisterBonusAttack);
        }

        private void RegisterBonusAttack(object sender, System.EventArgs e)
        {
            if (!HostShip.IsCannotAttackSecondTime)
            {
                HostShip.IsCannotAttackSecondTime = true;

                Combat.StartSelectAttackTarget(
                    HostShip,
                    FinishBonusAttack,
                    null,
                    HostShip.PilotInfo.PilotName,
                    "You may perform a bonus attack",
                    HostShip
                );
            }
            else
            {
                Messages.ShowErrorToHuman($"{HostShip.PilotInfo.PilotName} cannot perform second bonus attack");
                FinishBonusAttack();
            }
        }

        private void FinishBonusAttack()
        {
            // Restore previous value of "is already attacked" flag
            HostShip.IsAttackPerformed = PerformedRegularAttack;
            //if bonus attack was skipped, allow bonus attacks again
            if (HostShip.IsAttackSkipped) HostShip.IsCannotAttackSecondTime = false;
            Triggers.FinishTrigger();
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnAttackHitAsAttackerGlobal -= CheckAbility;
        }
    }
}