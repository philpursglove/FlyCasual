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
            Phases.Events.OnRoundEnd += ClearVariables;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsAttacker -= RegisterSaturationRocketAbility;
            Phases.Events.OnRoundEnd -= ClearVariables;
        }

        public void RegisterSaturationRocketAbility()
        {
            if(Combat.ChosenWeapon == HostUpgrade)
            {
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
                && HostUpgrade.State.Charges > 0)
            {
                if (!IsBonusAttack) HostShip.OnCombatCheckExtraAttack += RegisterBonusAttack;

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
                if (!IsBonusAttack)
                {
                    Triggers.FinishTrigger();
                }
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
            if (!IsBonusAttack 
                && HostUpgrade.State.Charges > 0)
            {
                RegisterAbilityTrigger(
                    TriggerTypes.OnCombatCheckExtraAttack,
                    AskBonusAttack);
            }

            HostShip.OnCombatCheckExtraAttack -= RegisterBonusAttack;
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

            HostUpgrade.State.RestoreCharge();

            HostShip.OnCheckIsForbiddenWeapon += AllowUpgradeOnly;

            Combat.StartSelectAttackTarget(
                HostShip,
                FinishBonusAttack,
                abilityName: HostUpgrade.UpgradeInfo.Name,
                description: $"Select a target for bonus attack",
                showSkipButton: true
            );
        }

        private void AllowUpgradeOnly(GenericShip target, IShipWeapon weapon, ref bool isForbidden)
        {
            isForbidden = weapon != HostUpgrade;
        }

        private void FinishBonusAttack()
        {
            HostShip.OnCheckIsForbiddenWeapon -= AllowUpgradeOnly;

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
