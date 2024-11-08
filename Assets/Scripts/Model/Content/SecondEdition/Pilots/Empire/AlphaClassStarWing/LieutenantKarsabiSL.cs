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
                    addArc: new ShipArcInfo(ArcType.FullFront),
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
            HostShip.OnAttackStartAsAttacker += CheckFiringArc;            
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsAttacker -= CheckFiringArc;
        }

        public void CheckFiringArc()
        {
            if(Combat.Defender != null && Combat.ShotInfo.InArcByType(ArcType.Front)) {
                AskToUseAbility(
                    HostUpgrade.UpgradeInfo.Name,
                    NeverUseByDefault,
                    AddAttackDie
                );
            }
        }

        public void AddAttackDie(object sender, EventArgs e)
        {
            Messages.ShowInfo(HostShip.PilotInfo.PilotName + " spent 1 charge to add an attack die.");

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
