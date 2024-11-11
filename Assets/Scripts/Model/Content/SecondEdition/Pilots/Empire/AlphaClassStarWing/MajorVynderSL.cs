using Abilities.SecondEdition;
using Actions;
using ActionsList;
using Arcs;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.AlphaClassStarWing
    {
        public class MajorVynderSL : AlphaClassStarWing
        {
            public MajorVynderSL() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Major Vynder",
                    "Helping Hand",
                    Faction.Imperial,
                    4,
                    5,
                    0,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.MajorVynderSLAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Sensor,
                        UpgradeType.Cannon,
                        UpgradeType.Missile
                    },
                    isStandardLayout: true                    
                );

                PilotNameCanonical = "majorvynder-ssl";

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.LongRangeScanners));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.IonCannon));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.HeavyPlasmaMissiles));

                ShipInfo.ActionIcons.AddLinkedAction(new LinkedActionInfo(typeof(SlamAction), typeof(TargetLockAction), ActionColor.Red));
                ShipInfo.ActionIcons.AddLinkedAction(new LinkedActionInfo(typeof(SlamAction), typeof(ReloadAction), ActionColor.Red));

                ShipAbilities.Add(new AlphaClassStarWingSLAbility());
            }
        }
    }
}

namespace UpgradesList.SecondEdition
{
    public class LongRangeScanners :GenericUpgrade
    {
        public LongRangeScanners() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                    "Long-Range Scanners",
                    UpgradeType.Sensor,
                    cost: 0,
                    charges: 2,
                    abilityType: typeof(Abilities.SecondEdition.LongRangeScannersAbility),
                    restriction: new AbilityPresenceRestriction(typeof(MajorVynderSLAbility))
                );

            ImageUrl = "https://infinitearenas.com/xw2/images/pilots/majorvynder-ssl.png";
        }
    }

    public class HeavyPlasmaMissiles : GenericSpecialWeapon
    {
        public HeavyPlasmaMissiles() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                    "Heavy Plasma Missiles",
                    UpgradeType.Missile,
                    cost: 0,
                    weaponInfo: new SpecialWeaponInfo(
                        attackValue: 3,
                        minRange: 1,
                        maxRange: 3,
                        charges: 2,
                        arc: ArcType.Front,
                        requiresToken: typeof(BlueTargetLockToken)
                    ),
                    abilityType: typeof(Abilities.SecondEdition.HeavyPlasmaMissilesAbility),
                    restriction: new AbilityPresenceRestriction(typeof(MajorVynderAbility))
                );

            ImageUrl = "https://infinitearenas.com/xw2/images/pilots/majorvynder-ssl.png";
        }
    }
}

namespace Abilities.SecondEdition
{
    public class MajorVynderSLAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            
        }

        public override void DeactivateAbility()
        {
            
        }
    }

    public class LongRangeScannersAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.BeforeActionIsPerformed += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.BeforeActionIsPerformed -= RegisterAbility;
        }

        private void RegisterAbility(GenericAction action, ref bool isValid)
        {
            if (HostUpgrade.State.Charges > 0 && action.GetType() == typeof(TargetLockAction))
            {
                RegisterAbilityTrigger(TriggerTypes.BeforeActionIsPerformed, AskUseMajorVynderAbility);
            }

            isValid = true;
        }

        private void AskUseMajorVynderAbility(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostShip.PilotInfo.PilotName,
                NeverUseByDefault,
                UseMajorVynderAbility,
                descriptionLong: "Do you want to spend 1 Charge to acquire locks at any range?",
                imageHolder: HostShip
            );
        }

        private void UseMajorVynderAbility(object sender, EventArgs e)
        {
            HostUpgrade.State.SpendCharge();
            HostShip.SetTargetLockRange(0, int.MaxValue);

            DecisionSubPhase.ConfirmDecision();
        }
    }

    public class HeavyPlasmaMissilesAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackHitAsAttacker += RegisterHeavyPlasmaMissileAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackHitAsAttacker -= RegisterHeavyPlasmaMissileAbility;
        }

        public void RegisterHeavyPlasmaMissileAbility()
        {
            if(Combat.ChosenWeapon == HostUpgrade)
            {
                RegisterAbilityTrigger(
                    TriggerTypes.OnAttackHit,
                    RemoveTargetShield
                );
            }
        }

        public void RemoveTargetShield(object sender, EventArgs e)
        {
            if(Combat.Defender != null && Combat.ChosenWeapon == HostUpgrade && Combat.Defender.State.ShieldsCurrent > 0)
            {
                DamageSourceEventArgs shieldDamage = new DamageSourceEventArgs()
                {
                    Source = HostUpgrade.UpgradeInfo.Name,
                    DamageType = DamageTypes.CardAbility
                };

                Combat.Defender.Damage.SufferRegularDamage(shieldDamage, Triggers.FinishTrigger);
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }
    }
}
