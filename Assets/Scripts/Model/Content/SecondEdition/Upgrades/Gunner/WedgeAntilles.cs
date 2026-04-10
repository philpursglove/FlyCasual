using Conditions;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class WedgeAntilles : GenericUpgrade
    {
        public WedgeAntilles() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Wedge Antilles",
                UpgradeType.Gunner,
                cost: 1,
                isLimited: true,
                restriction: new FactionRestriction(Faction.Resistance),
                abilityType: typeof(Abilities.SecondEdition.WedgeAntillesGunnerAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class WedgeAntillesGunnerAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackStartAsAttacker += AddWedgeAntillesGunnerAbility;
        }
        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsAttacker -= AddWedgeAntillesGunnerAbility;
        }
        public void AddWedgeAntillesGunnerAbility()
        {
            BoardTools.DistanceInfo distanceInfo = new BoardTools.DistanceInfo(HostShip, Combat.Defender);
            if (distanceInfo.Range > 0 && Combat.ArcForShot.IsTurretArc)
            {
                WedgeAntillesCondition condition = new WedgeAntillesCondition(Combat.Defender, HostShip);
                Combat.Defender.Tokens.AssignCondition(condition);
            }
        }
    }
}
