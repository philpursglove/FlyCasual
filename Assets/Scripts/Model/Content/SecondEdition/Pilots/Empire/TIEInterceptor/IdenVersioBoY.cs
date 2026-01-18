using Abilities.SecondEdition;
using Content;
using Ship;
using SubPhases;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEInterceptor
    {
        public class IdenVersioBoY : TIEInterceptor
        {
            public IdenVersioBoY() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Iden Versio",
                    "Battle of Yavin",
                    Faction.Imperial,
                    4,
                    5,
                    0,
                    charges: 2,
                    regensCharges: 1,
                    isLimited: true,
                    abilityType: typeof(IdenVersioBoYAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    isStandardLayout: true,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                ShipInfo.Shields++;

                AutoThrustersAbility oldAbility = (AutoThrustersAbility)ShipAbilities.First(n => n.GetType() == typeof(AutoThrustersAbility));
                ShipAbilities.Remove(oldAbility);
                ShipAbilities.Add(new SensitiveControlsBoYRealAbility());

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Predator));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Fanatic));

                PilotNameCanonical = "idenversio-battleofyavin";
            }
        }

        public class IdenVersioBoYXWA : IdenVersioBoY
        {
            public IdenVersioBoYXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 11;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class IdenVersioBoYAbility : GenericAbility
    {
        private GenericShip defender;

        public override void ActivateAbility()
        {
            GenericShip.OnTryDamagePreventionGlobal += CheckIdenVersioAbilitySE;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnTryDamagePreventionGlobal -= CheckIdenVersioAbilitySE;
        }

        private void CheckIdenVersioAbilitySE(GenericShip ship, DamageSourceEventArgs e)
        {
            defender = ship;

            // Is the defender on our team? If not return.
            if (!Tools.IsSameTeam(HostShip, ship))
                return;

            if (!(ship.PilotInfo as PilotCardInfo25).Tags.Contains(Tags.Tie))
                return;

            // If the defender is at range one of us we register our trigger to prevent damage.
            BoardTools.DistanceInfo distanceInfo = new BoardTools.DistanceInfo(defender, HostShip);
            if (distanceInfo.Range <= 1)
            {
                RegisterAbilityTrigger(TriggerTypes.OnTryDamagePrevention, UseIdenVersioAbilitySE);
            }
        }

        private void UseIdenVersioAbilitySE(object sender, System.EventArgs e)
        {
            List<Die> hits = defender.AssignedDamageDiceroll.DiceList.Where(d => (d.Side == DieSide.Success || (d.Side == DieSide.Crit && d.IsUncancelable == false))).OrderBy(o => o.Side == DieSide.Success ? 0 : 1).ToList();

            if (HostShip.State.Charges >= 2 && hits.Count > 0)
            {
                // If there are we prompt to see if they want to use the ability.
                AskToUseAbility(
                    HostShip.PilotInfo.PilotName,
                    AlwaysUseByDefault,
                    delegate { HostShip.SpendCharges(2); RemoveHit(hits.First()); },
                    descriptionLong: "Do you want to spend 2 Charges to prevent damage?",
                    imageHolder: HostShip
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void RemoveHit(Die dieToRemove)
        {
            defender.AssignedDamageDiceroll.DiceList.Remove(dieToRemove);

            DecisionSubPhase.ConfirmDecision();
        }


    }
}