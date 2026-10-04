using Abilities.SecondEdition;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship.SecondEdition.FiresprayClassPatrolCraft
{
    public class BobaFettAaD : FiresprayClassPatrolCraft
    {
        public BobaFettAaD() : base()
        {
            PilotInfo = new PilotCardInfo25(
                pilotName: "Boba Fett",
                pilotTitle: "Armed and Dangerous",
                faction: Faction.Scum,
                initiative: 5,
                cost: 18,
                loadoutValue: 0,
                isLimited: true,
                limited: 1,
                abilityType: typeof(BobaFettAaDAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                        UpgradeType.Sensor,
                        UpgradeType.Gunner,
                        UpgradeType.Device,
                        UpgradeType.Title
                },
                isStandardLayout: true,
                tags: new List<Tags> { Tags.BountyHunter },
                skinName: "Boba Fett",
                legality: new List<Legality> { Legality.XWA }
            );

            MustHaveUpgrades.Add(typeof(HomingBeacon));
            MustHaveUpgrades.Add(typeof(FennecShandGunner));
            MustHaveUpgrades.Add(typeof(SeismicCharges));
            MustHaveUpgrades.Add(typeof(SlaveISeparatistsAbility));

            PilotNameCanonical = "bobafett-armedanddangerous";
        }
    }
}

namespace Abilities.SecondEdition
{
    public class BobaFettAaDAbility : GenericAbility
    {
        GenericShip bonusAttackTarget;
        bool hasActivated = false;
        bool hasPerformedBonusAttack = false;

        public override void ActivateAbility()
        {
            GenericShip.OnAttackFinishGlobal += CheckBobaFettAaDAbility;
            Phases.Events.OnRoundEnd += ResetFlags;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnAttackFinishGlobal -= CheckBobaFettAaDAbility;
            Phases.Events.OnRoundEnd -= ResetFlags;
        }

        private void CheckBobaFettAaDAbility(GenericShip ship)
        {
            if (Combat.Defender.Owner.PlayerNo == HostShip.Owner.PlayerNo &&
                Combat.Attacker.Owner.PlayerNo != HostShip.Owner.PlayerNo &&
                ActionsHolder.HasTargetLockOn(HostShip, Combat.Attacker) &&
                Combat.Defender != HostShip &&
                !hasActivated && !hasPerformedBonusAttack &&
                !HostShip.IsCannotAttackSecondTime)
            {
                hasActivated = HostShip.IsAttackPerformed;
                bonusAttackTarget = Combat.Attacker;
                bonusAttackTarget.OnCombatCheckExtraAttack += RegisterBobaFettAaDAbility;
            }
        }

        private void RegisterBobaFettAaDAbility(GenericShip ship)
        {
            bonusAttackTarget.OnCombatCheckExtraAttack -= RegisterBobaFettAaDAbility;

            RegisterAbilityTrigger(TriggerTypes.OnCombatCheckExtraAttack, UseBobaFettAaDAbility);
        }

        private void UseBobaFettAaDAbility(object sender, EventArgs e)
        {
            Messages.ShowInfo(
                $"{HostShip.PilotInfo.PilotName} can perform a bonus attack against {bonusAttackTarget.PilotInfo.PilotName}.");

            Combat.StartSelectAttackTarget
            (
                HostShip,
                Cleanup,
                IsPrimaryWeaponShot,
                HostShip.PilotInfo.PilotName,
                $"You may perform a bonus attack against {bonusAttackTarget.PilotInfo.PilotName}.",
                HostShip
            );
        }

        public void ResetFlags()
        {
            hasActivated = false;
            hasPerformedBonusAttack = false;
        }

        private bool IsPrimaryWeaponShot(GenericShip ship, IShipWeapon weapon, bool isSilent)
        {
            if (weapon.WeaponType == WeaponTypes.PrimaryWeapon && ship == bonusAttackTarget)
            {
                return true;
            }
            else if (weapon.WeaponType != WeaponTypes.PrimaryWeapon)
            {
                Messages.ShowError(
                    $"{HostShip.PilotInfo.PilotName}'s bonus attack must be performed using primary weapon");
                return false;
            }
            else
            {
                Messages.ShowError(
                    $"{HostShip.PilotInfo.PilotName}'s bonus attack must target {bonusAttackTarget.PilotInfo.PilotName}");
                return false;
            }
        }

        private void Cleanup()
        {
            bonusAttackTarget = null;

            // If the bonus attacked occurred prior to activation, this lets Boba get their normal activation
            HostShip.IsAttackPerformed = hasActivated;

            //if bonus attack was skipped, allow bonus attacks again
            if (HostShip.IsAttackSkipped)
            {
                hasPerformedBonusAttack = false;
                HostShip.IsCannotAttackSecondTime = false;
            }
            else
            {
                hasPerformedBonusAttack = true;
                HostShip.IsCannotAttackSecondTime = true;
            }
            Triggers.FinishTrigger();
        }
    }
}