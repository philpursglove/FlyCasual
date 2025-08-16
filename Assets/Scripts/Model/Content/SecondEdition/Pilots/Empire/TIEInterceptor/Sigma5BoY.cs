using Abilities.SecondEdition;
using ActionsList;
using Content;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEInterceptor
    {
        public class Sigma5BoY : TIEInterceptor
        {
            public Sigma5BoY() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Sigma 5",
                    "Battle of Yavin",
                    Faction.Imperial,
                    4,
                    4,
                    0,
                    charges: 2,
                    isLimited: true,
                    abilityType: typeof(Sigma5BoYAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Sensor
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    isStandardLayout: true,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                ShipInfo.Hull++;

                AutoThrustersAbility oldAbility = (AutoThrustersAbility)ShipAbilities.First(n => n.GetType() == typeof(AutoThrustersAbility));
                ShipAbilities.Remove(oldAbility);
                ShipAbilities.Add(new SensitiveControlsBoYRealAbility());

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.SensorJammer));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Elusive));

                PilotNameCanonical = "sigma5-battleofyavin";
            }
        }

        public class Sigma5BoYXWA : Sigma5BoY
        {
            public Sigma5BoYXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class Sigma5BoYAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackHitAsAttacker += RegisterSigma5Ability;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackHitAsAttacker -= RegisterSigma5Ability;
        }

        public void RegisterSigma5Ability()
        {
            if (HostShip.State.Charges > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.OnAttackHit, AskPerformEvadeAction);
            }
        }

        private void AskPerformEvadeAction(object sender, System.EventArgs e)
        {
            if (HostShip.State.Charges > 0)
            {
                HostShip.BeforeActionIsPerformed += PayChargeCost;

                HostShip.AskPerformFreeAction
                (
                    new EvadeAction() { HostShip = HostShip },
                    CleanUp,
                    HostShip.PilotInfo.PilotName,
                    "You may spend 1 charge to perform an Evade action"
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void PayChargeCost(GenericAction action, ref bool isFreeAction)
        {
            HostShip.SpendCharge();
            HostShip.BeforeActionIsPerformed -= PayChargeCost;
        }

        private void CleanUp()
        {
            HostShip.BeforeActionIsPerformed -= PayChargeCost;
            Triggers.FinishTrigger();
        }
    }
}