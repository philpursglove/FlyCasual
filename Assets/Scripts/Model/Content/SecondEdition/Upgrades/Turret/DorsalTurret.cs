using Actions;
using ActionsList;
using Arcs;
using Content;
using Ship;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class DorsalTurret : GenericSpecialWeapon
    {
        public DorsalTurret() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Dorsal Turret",
                UpgradeType.Turret,
                cost: 2,
                weaponInfo: new SpecialWeaponInfo(
                    attackValue: 2,
                    minRange: 1,
                    maxRange: 2,
                    arc: ArcType.SingleTurret
                ),
                addArc: new ShipArcInfo(ArcType.SingleTurret),
                addAction: new ActionInfo(typeof(RotateArcAction)),
                abilityType: typeof(Abilities.SecondEdition.DorsalTurretAbility),
                seImageNumber: 31,
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }        
    }

    public class DorsalTurretXWA : DorsalTurret
    {
        public DorsalTurretXWA() : base()
        {
            UpgradeInfo.Cost = 3;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class DorsalTurretAbility : GenericAbility
    {
        public override void ActivateAbility() { }

        public override void ActivateAbilityForSquadBuilder()
        {
            HostShip.ActionBar.AddGrantedAction(new RotateArcAction(), HostUpgrade);
        }

        public override void DeactivateAbility() { }

        public override void DeactivateAbilityForSquadBuilder()
        {
            HostShip.ActionBar.RemoveGrantedAction(typeof(RotateArcAction), HostUpgrade);
        }

    }
}