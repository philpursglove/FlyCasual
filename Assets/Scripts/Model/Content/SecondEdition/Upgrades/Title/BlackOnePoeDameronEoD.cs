using ActionsList;
using Ship;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class BlackOnePoeDameronEoD : GenericUpgrade
    {
        public BlackOnePoeDameronEoD() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Black One",
                UpgradeType.Title,
                cost: 0,
                isLimited: true,
                restrictions: new UpgradeCardRestrictions(
                    new ShipRestriction(typeof(Ship.SecondEdition.T70XWing.T70XWing)),
                    new FactionRestriction(Faction.Resistance)
                ),
                charges: 2,
                abilityType: typeof(Abilities.SecondEdition.BlackOnePoeDameronEoDAbility)
            );

            IsHidden = true;
            ImageUrl = "https://infinitearenas.com/xw2xwa/images/pilots/poedameron-evacuationofdqar.png";
        }        
    }
}

namespace Abilities.SecondEdition
{
    public class BlackOnePoeDameronEoDAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnTryAddAction += RestrictSlam;
            HostShip.OnSlam += LoseCharge;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnTryAddAction -= RestrictSlam;
            HostShip.OnSlam -= LoseCharge;
        }

        private void RestrictSlam(GenericShip ship, GenericAction action, ref bool canBeUsed)
        {
            if (action is SlamAction)
            {
                if (canBeUsed) canBeUsed = HostUpgrade.State.Charges > 0;
            }
        }

        private void LoseCharge()
        {
            if (HostUpgrade.State.Charges > 0)
            {
                HostUpgrade.State.LoseCharge();
            }
        }
    }
}