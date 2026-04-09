using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class Captive : GenericUpgrade
    {
        public Captive() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Captive",
                UpgradeType.Crew,
                cost: 1, //TODO Fix cost
                isLimited: true,
                restriction: new FactionRestriction(Faction.Imperial, Faction.FirstOrder, Faction.Separatists),
                abilityType: typeof(Abilities.SecondEdition.CaptiveCrewAbility),
                legalityInfo: new List<Legality> { Legality.XWA }
            );
            IsHidden = false;

            //TODO ImageUrl
        }
    }
}

namespace Abilities.SecondEdition
{
    public class CaptiveCrewAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnDefenceStartAsDefender += UseAbility();
        }

        public override void DeactivateAbility()
        {
            HostShip.OnDefenceStartAsDefender -= UseAbility();
        }

        private GenericShip.EventHandler UseAbility()
        {
            throw new System.NotImplementedException();
        }


    }
}
