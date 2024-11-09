using Abilities.SecondEdition;
using Actions;
using ActionsList;
using Arcs;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.AlphaClassStarWing
    {
        public class LieutenantKarsabiSL : AlphaClassStarWing
        {
            public LieutenantKarsabiSL() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Lieutenant Karsabi",
                    "Payload Courier",
                    Faction.Imperial,
                    3,
                    5,
                    0,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.LieutenantKarsabiSLAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Torpedo,
                        UpgradeType.Missile,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    isStandardLayout: true                    
                );

                PilotNameCanonical = "lieutenantkarsabi-ssl";

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.ProtonTorpedoes));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.SaturationRockets));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.ElectronicBaffle));

                ShipInfo.ActionIcons.AddLinkedAction(new LinkedActionInfo(typeof(SlamAction), typeof(TargetLockAction), ActionColor.Red));
                ShipInfo.ActionIcons.AddLinkedAction(new LinkedActionInfo(typeof(SlamAction), typeof(ReloadAction), ActionColor.Red));
            }
        }
    }
}

namespace UpgradesList.SecondEdition
{
    public class SaturationRockets : GenericSpecialWeapon
    {
        public SaturationRockets() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                    "Saturation Rockets",
                    UpgradeType.Missile,
                    cost: 0,
                    isLimited: true,
                    weaponInfo: new SpecialWeaponInfo(
                        attackValue: 3,
                        minRange: 1,
                        maxRange: 2,
                        charges: 4,
                        regensCharges: true,
                        arc: ArcType.FullFront,
                        chargesCost: 2
                    ),
                    abilityType: typeof(Abilities.SecondEdition.SaturationRocketsAbility),
                    restriction: new AbilityPresenceRestriction(typeof(LieutenantKarsabiSLAbility))
                );

            ImageUrl = "https://infinitearenas.com/xw2/images/pilots/lieutenantkarsabi-ssl.png";
        }
    }
}

namespace Abilities.SecondEdition
{
    // TODO: Add extra shot
    public class SaturationRocketsAbility : GenericAbility
    {
        bool IsBonusAttack = false;
        
        public override void ActivateAbility()
        {
            HostShip.OnAttackStartAsAttacker += RegisterSaturationRocketAbility;
            //HostShip.OnAttackFinishAsAttacker += CheckBonusAttack;
            Phases.Events.OnRoundEnd += ClearBonusAttackFlag;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsAttacker -= RegisterSaturationRocketAbility;
            //HostShip.OnAttackFinishAsAttacker -= CheckBonusAttack;
            Phases.Events.OnRoundEnd -= ClearBonusAttackFlag;
        }

        public void RegisterSaturationRocketAbility()
        {
            if(Combat.ChosenWeapon == HostUpgrade)
            {
                // TODO: Don't do this, they show as two simultaneous abilities. We need them to show in order.
                // Check OnExtraAttack and register the ask then.
                RegisterAbilityTrigger(
                    TriggerTypes.OnAttackStart, 
                    CheckFiringArc
                );

                RegisterAbilityTrigger(
                    TriggerTypes.OnCombatCheckExtraAttack,
                    AskBonusAttack
                );
            }
        }

        public void RegisterSaturationRocketAbility(GenericShip ship)
        {
            RegisterSaturationRocketAbility();
        }

        //public void CheckBonusAttack(GenericShip ship)
        //{
        //    if (Combat.ChosenWeapon == this.HostUpgrade && !IsBonusAttack)
        //    {
        //        IsBonusAttack = true;

        //        HostShip.OnCombatCheckExtraAttack += RegisterSaturationRocketAbility;
        //    }
        //}

        public void CheckFiringArc(object sender, System.EventArgs e)
        {
            if(Combat.Defender != null 
                && Combat.ShotInfo.InArcByType(ArcType.Front)
                && HostUpgrade.State.Charges > 0) {
                AskToUseAbility(
                    HostUpgrade.UpgradeInfo.Name,
                    NeverUseByDefault,
                    UseAbility,
                    showSkipButton: false,
                    descriptionLong: "Do you want to spend an additional charge to add an attack die?",
                    imageHolder: HostShip
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        public void UseAbility(object sender, EventArgs e)
        {
            HostShip.AfterGotNumberOfAttackDice += SaturationRocketAddAttackDice;
            HostUpgrade.State.SpendCharge();
            SubPhases.DecisionSubPhase.ConfirmDecision();
        }

        private void SaturationRocketAddAttackDice(ref int value)
        {
            Messages.ShowInfo(HostShip.PilotInfo.PilotName + ": +1 attack die");
            value++;
            HostShip.AfterGotNumberOfAttackDice -= SaturationRocketAddAttackDice;
        }

        public void AskBonusAttack(object sender, System.EventArgs e)
        {
            if(IsBonusAttack && HostUpgrade.State.Charges > 0)
            {
                AskToUseAbility(
                    HostUpgrade.UpgradeInfo.Name,
                    NeverUseByDefault,
                    UseBonusAttack,
                    showSkipButton: false,
                    descriptionLong: "Do you want to spend an additional charge for a bonus attack?",
                    imageHolder: HostShip
                    );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        public void UseBonusAttack(object sender, System.EventArgs e)
        {
            IsBonusAttack = true;
            
            HostUpgrade.State.SpendCharge();

            Messages.ShowInfo(HostUpgrade.UpgradeInfo.Name + " spent a charge to get a bonus attack.");
            
            Combat.StartSelectAttackTarget(
                HostShip,
                null,
                abilityName: HostUpgrade.UpgradeInfo.Name,
                description: $"You may perform a bonus {HostUpgrade.UpgradeInfo.Name} attack",
                imageSource: HostUpgrade

                //GenericShip ship,
                //Action callback,
                //Func<GenericShip, IShipWeapon, bool, bool> extraAttackFilter = null,
                //string abilityName = null,
                //string description = null,
                //IImageHolder imageSource = null,
                //bool showSkipButton = true,
                //Action < Action > payAttackCost = null
            );
        }

        public void ClearBonusAttackFlag()
        {
            IsBonusAttack = false;
        }
    }

    public class LieutenantKarsabiSLAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            
        }

        public override void DeactivateAbility()
        {
            
        }
    }
}
