using Abilities.SecondEdition;
using Arcs;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace UpgradeList.SecondEdition
{
    public class FennecShand : GenericUpgrade
    {
        public FennecShand() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Fennec Shand",
                UpgradeType.Crew,
                cost: 1, // TODO: Update cost
                isLimited: true,
                charges: 2,
                restriction: new FactionRestriction(Faction.Scum),
                abilityType: typeof(FennecShandAbility),
                legalityInfo: new List<Legality>() { Legality.XWA }
            );

            // TODO: Update NameCanonical & ImageUrl
        }
    }
}

namespace Abilities.SecondEdition
{
    // After you fully execute a maneuver, or perform a barrel roll or boost action, you may spend 1 charge. If you do, choose an enemy ship in your bullseye.
    // That ship gains 1 strain token, and you may acquire a lock on it.
    public class FennecShandAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully += AskUseAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully -= AskUseAbility;
        }

        private void AskUseAbility(GenericShip ship)
        {
            RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, UseAbility);
        }

        private void UseAbility(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostUpgrade.UpgradeInfo.Name,
                AlwaysUseByDefault,
                StrainAndTargetLockShip,
                callback: Triggers.FinishTrigger,
                descriptionLong: $"You may spend 1 charge. If you do, you may strain 1 ship and acquire a target lock on it."
            );
        }

        private void StrainAndTargetLockShip(object sender, EventArgs e)
        {
            HostShip.OnTargetLockIsAcquired += ApplyStrain;

            HostShip.ChooseTargetToAcquireTargetLock(
                Triggers.FinishTrigger,
                "Choose a target to acquire lock and apply 1 strain.",
                HostUpgrade,
                IsInBullseye
            );
        }

        private void ApplyStrain(ITargetLockable target)
        {
            HostShip.OnTargetLockIsAcquired -= ApplyStrain;

            (target as GenericShip).Tokens.AssignToken(typeof(StrainToken), delegate { });
        }

        private bool IsInBullseye(GenericShip ship)
        {
            return HostShip.SectorsInfo.IsShipInSector(ship, ArcType.Bullseye);
        }
    }
}