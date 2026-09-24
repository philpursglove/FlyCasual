using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content;
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
        }

        public override void DeactivateAbility()
        {
        }
    }
}