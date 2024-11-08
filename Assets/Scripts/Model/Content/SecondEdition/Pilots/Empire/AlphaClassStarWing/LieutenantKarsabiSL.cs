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
    // TODO: Add abilities
    public class SaturationRocketsAbility : GenericAbility
    {
        bool bonusAttack = false;
        
        public override void ActivateAbility()
        {
            //HostShip.OnCombatCheckExtraAttack
            //HostShip.OnGenerateAvailableAttackPaymentList
            
            HostShip.OnAttackStartAsAttacker += RegisterSaturationRocketAbility;
            HostShip.OnAttackFinishAsAttacker += CheckBonusAttack;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsAttacker -= RegisterSaturationRocketAbility;
            HostShip.OnAttackFinishAsAttacker -= CheckBonusAttack;
        }

        public void RegisterSaturationRocketAbility()
        {
            // TODO: Add confirmation to use Saturation Rockets if only option?
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

        public void CheckBonusAttack(GenericShip ship)
        {
            Messages.ShowInfo(HostUpgrade.UpgradeInfo.Name + " spent 1 charge to get a bonus attack.");
            bonusAttack = true;
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
