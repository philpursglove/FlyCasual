using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using System.Linq;
using ActionsList;
using Ship;
using Upgrade;

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
                    isStandardLayout:true
                );
                PilotNameCanonical = "soontirfel-battleorderendor";

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.FeedbackEmitter));

                AutoThrustersAbility oldAbility = (AutoThrustersAbility)ShipAbilities.First(n => n.GetType() == typeof(AutoThrustersAbility));
                ShipAbilities.Remove(oldAbility);
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
                    delegate { HostShip.SpendCharges(1); BoostOrBarrelRoll(); },
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

        }
    }
}