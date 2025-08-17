using ActionsList;
using Obstacles;
using Ship;
using System.Linq;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class TierfonBellyRun : GenericUpgrade
    {
        public TierfonBellyRun() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Tierfon Belly Run",
                UpgradeType.Talent,
                cost: 1,
                abilityType: typeof(Abilities.SecondEdition.TierfonBellyRunAbility),
                restriction: new TagRestriction(Content.Tags.YWing)
            );

            
        }        
    }
}

namespace Abilities.SecondEdition
{
    public class TierfonBellyRunAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnCheckObstacleDenyAttack += CheckAttackAllowAbility;
            HostShip.OnCheckIsForbiddenWeapon += DenyPrimaryOnAsteroid;

            GenericShip.OnTryAddAvailableDiceModificationGlobal += DenyAttackRerollWhileLanded;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnCheckObstacleDenyAttack -= CheckAttackAllowAbility;
            HostShip.OnCheckIsForbiddenWeapon -= DenyPrimaryOnAsteroid;

            GenericShip.OnTryAddAvailableDiceModificationGlobal -= DenyAttackRerollWhileLanded;
        }

        private void CheckAttackAllowAbility(GenericObstacle obstacle, ref bool isAllowed)
        {
            if (obstacle is Asteroid) isAllowed = true;
        }

        private void DenyPrimaryOnAsteroid(GenericShip ship, IShipWeapon weapon, ref bool isDenied)
        {
            if (weapon.WeaponType == WeaponTypes.PrimaryWeapon
                && HostShip.ObstaclesLanded.Any(n => n is Asteroid))
            {
                isDenied = true;
            }
        }

        private void DenyAttackRerollWhileLanded(GenericShip ship, GenericAction diceModification, ref bool isAllowed)
        {
            if (Tools.IsSameShip(HostShip, Combat.Defender) && HostShip.ObstaclesLanded.Any(n => n is Asteroid))
            {
                if (diceModification.IsReroll) isAllowed = false;
            }
        }
    }
}