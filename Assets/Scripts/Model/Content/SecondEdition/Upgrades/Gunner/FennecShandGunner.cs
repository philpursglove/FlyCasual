using Content;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class FennecShandGunner : GenericUpgrade
    {
        public FennecShandGunner()
        {
            UpgradeInfo = new UpgradeCardInfo(
                name: "Fennec Shand",
                type: UpgradeType.Gunner,
                cost: 0,
                abilityType: typeof(Abilities.SecondEdition.FennecShandGunnerAbility),
                legalityInfo: new List<Legality> { Legality.XWA });
            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class FennecShandGunnerAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackStartAsAttacker += RegisterFennecShandGunnerAttackAbility;
            HostShip.OnAttackStartAsDefender += RegisterFennecShandGunnerDefenceAbility;
            //TODO does this need corresponding OnAttackEnd handlers to restore agility? 
        }

        private void RegisterFennecShandGunnerDefenceAbility()
        {
            if (ActionsHolder.HasTargetLockOn(HostShip, Selection.AnotherShip))
            {
                Messages.ShowInfo($"Fennec Shand: {Selection.AnotherShip.PilotInfo.PilotName} loses 1 attack die");
                Combat.ChosenWeapon.WeaponInfo.AttackValue--;
            }
        }

        private void RegisterFennecShandGunnerAttackAbility()
        {
            if (ActionsHolder.HasTargetLockOn(HostShip, Selection.AnotherShip))
            {
                Messages.ShowInfo($"Fennec Shand: {Selection.AnotherShip.PilotInfo.PilotName} loses 1 agility");
                Selection.AnotherShip.ShipInfo.Agility--;
            }
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsAttacker -= RegisterFennecShandGunnerAttackAbility;
            HostShip.OnAttackStartAsDefender -= RegisterFennecShandGunnerDefenceAbility;
        }
    }
}