using Abilities.SecondEdition;
using Actions;
using ActionsList;
using Arcs;
using BoardTools;
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
        IShipWeapon OriginalWeapon;
        
        public override void ActivateAbility()
        {
            HostShip.OnAttackStartAsAttacker += RegisterSaturationRocketAbility;
            HostShip.OnCombatCheckExtraAttack += RegisterBonusAttack;
            Phases.Events.OnRoundEnd += ClearVariables;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsAttacker -= RegisterSaturationRocketAbility;
            HostShip.OnCombatCheckExtraAttack -= RegisterBonusAttack;
            Phases.Events.OnRoundEnd -= ClearVariables;
        }

        public void RegisterSaturationRocketAbility()
        {
            if(Combat.ChosenWeapon == HostUpgrade)
            {
                OriginalWeapon = Combat.ChosenWeapon;    

                RegisterAbilityTrigger(
                    TriggerTypes.OnAttackStart, 
                    CheckFiringArc
                );
            }
        }

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

        public void RegisterBonusAttack(GenericShip ship)
        {
            if (OriginalWeapon == HostUpgrade
                && !IsBonusAttack 
                && HostUpgrade.State.Charges > 0)
            {
                RegisterAbilityTrigger(
                    TriggerTypes.OnCombatCheckExtraAttack,
                    AskBonusAttack
                    );
            }
        }

        public void AskBonusAttack(object sender, System.EventArgs e)
        {
            AskToUseAbility(
                HostUpgrade.UpgradeInfo.Name,
                NeverUseByDefault,
                UseBonusAttack,
                showSkipButton: false,
                descriptionLong: "Do you want to spend an additional charge for a bonus attack?"
                );
        }

        public void UseBonusAttack(object sender, System.EventArgs e)
        {
            IsBonusAttack = true;
            
            Messages.ShowInfo(HostUpgrade.UpgradeInfo.Name + " spent a charge to get a bonus attack.");

            //Combat.GenerateIntentToAttackCommand()

            HostUpgrade.State.RestoreCharge();

            Combat.StartSelectAttackTarget(
                HostShip,
                FinishAdditionalAttack,
                extraAttackFilter: BonusAttackWithWeapon,
                abilityName: HostUpgrade.UpgradeInfo.Name,
                description: $"Select a target for bonus attack"
            );
        }

        private bool BonusAttackWithWeapon(GenericShip target, IShipWeapon weapon, bool isSilent)
        {
            bool isValidWeapon = weapon == HostUpgrade;

            if(!isValidWeapon && !isSilent) Messages.ShowError($"Bonus attack must be with {HostUpgrade.UpgradeInfo.Name}.");

            return isValidWeapon;
        }

        private void FinishAdditionalAttack()
        {
            //if bonus attack was skipped, refund charge
            if (Selection.ThisShip.IsAttackSkipped)
            {
                HostUpgrade.State.RestoreCharge();
            }

            SubPhases.DecisionSubPhase.ConfirmDecision();
        }

        public void ClearVariables()
        {
            IsBonusAttack = false;
            OriginalWeapon = null;
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
