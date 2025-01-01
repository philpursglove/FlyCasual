using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using ActionsList;
using ActionsList.SecondEdition;
using Ship;
using Tokens;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship
{
    namespace SecondEdition.TIEInterceptor
    {
        public class SoontirFelBoE : TIEInterceptor
        {
            public SoontirFelBoE()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Soontir Fel",
                    "Battle Over Endor",
                    Faction.Imperial,
                    6,
                    5,
                    10,
                    isLimited: true,
                    abilityType: typeof(SoontirFelBoEAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Sensor,
                        UpgradeType.Illicit
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    seImageNumber: 103,
                    skinName: "Red Stripes",
                    charges: 2,
                    isStandardLayout: true
                );
                PilotNameCanonical = "soontirfel-battleoverendor";

                MustHaveUpgrades.Add(typeof(FeedbackEmitter));
                MustHaveUpgrades.Add(typeof(ApexPredator));
                MustHaveUpgrades.Add(typeof(NoEscape));
                MustHaveUpgrades.Add(typeof(BlankSignature));

                ImageUrl =
                    "https://cdn.svc.asmodee.net/production-amgcom/uploads/2024/02/02012024-SWZ99_Transmission-Image_8-768x438.png";

                DefaultUpgrades.Remove(typeof(AutoThrustersAbility));
                ShipAbilities.Add(new SensitiveControlsBoYRealAbility());

            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SoontirFelBoEAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackFinishAsAttacker += UseSoontirFelBoEAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackFinishAsAttacker -= UseSoontirFelBoEAbility;
        }

        private void UseSoontirFelBoEAbility(GenericShip ship)
        {
            // Are there charges left to power the ability?
            if (HostShip.State.Charges >= 2)
            {
                // If there are we prompt to see if they want to use the ability.
                AskToUseAbility(
                    HostShip.PilotInfo.PilotName,
                    AlwaysUseByDefault,
                    delegate
                    {
                        HostShip.SpendCharges(1);
                        BoostOrBarrelRoll();
                    },
                    descriptionLong: "Do you want to spend 1 Charge and gain a Deplete token to boost or barrel roll?",
                    imageHolder: HostShip
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void BoostOrBarrelRoll()
        {
            HostShip.AskPerformFreeAction(
                new List<GenericAction>()
                {
                    new BarrelRollAction() {HostShip = HostShip},
                    new BoostAction() {HostShip = HostShip}
                },
                Triggers.FinishTrigger,
                descriptionShort: "Soontir Fel",
                descriptionLong: "You may perform a barrel roll or boost action",
                imageHolder: HostUpgrade);
            HostShip.Tokens.AssignToken(typeof(DepleteToken), null, null);
        }
    }
}

namespace UpgradesList.SecondEdition
{

    public class ApexPredator : GenericUpgrade
    {
        public ApexPredator()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Apex Predator",
                UpgradeType.Talent
            );
            IsHidden = true;

        }

        public new void ActivateAbility()
        {
            HostShip.OnGenerateDiceModifications += AddApexPredatorReroll;
        }

        public new void DeactivateAbility()
        {
            HostShip.OnGenerateDiceModifications -= AddApexPredatorReroll;
        }

        private void AddApexPredatorReroll(GenericShip ship)
        {
            HostShip.AddAvailableDiceModificationOwn(new ApexPredatorActionEffect());
        }
    }
}

namespace ActionsList.SecondEdition
{
    public class ApexPredatorActionEffect : GenericAction
    {
        public ApexPredatorActionEffect()
        {
            Name = "Apex Predator";
            DiceModificationName = "Apex Predator";
        }

        public override bool IsDiceModificationAvailable()
        {
            return Combat.Attacker.PilotInfo.Initiative > Combat.Defender.PilotInfo.Initiative;
        }

        public override void ActionEffect(System.Action callBack)
        {
            DiceRerollManager diceRerollManager = new DiceRerollManager
            {
                NumberOfDiceCanBeRerolled = 1,
                CallBack = callBack
            };
            diceRerollManager.Start();
        }

        public override int GetDiceModificationPriority()
        {
            int result = 0;

            if (Combat.AttackStep == CombatStep.Attack)
            {
                int attackFocuses = Combat.CurrentDiceRoll.FocusesNotRerolled;
                int attackBlanks = Combat.CurrentDiceRoll.BlanksNotRerolled;
                int numFocusTokens = Selection.ActiveShip.Tokens.CountTokensByType(typeof(FocusToken));
                // Only use Fire Control if the number of dice that need re-rolled is 1.
                if (numFocusTokens > 0)
                {
                    // Slightly above Target Lock.
                    if (attackBlanks == 1) result = 81;
                }
                else
                {
                    // Slightly above Target Lock.
                    if (attackBlanks + attackFocuses == 1) result = 81;
                }
            }

            return result;
        }

    }
}