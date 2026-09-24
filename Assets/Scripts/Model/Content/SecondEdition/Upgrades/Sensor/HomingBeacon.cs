using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class HomingBeacon : GenericUpgrade
    {
        public HomingBeacon()
        {
            UpgradeInfo = new UpgradeCardInfo(
                name: "Homing Beacon",
                type: UpgradeType.Sensor,
                cost: 0,
                abilityType: typeof(Abilities.SecondEdition.HomingBeaconAbility),
                legalityInfo: new List<Legality> { Legality.XWA });
        }
    }
}

namespace Abilities.SecondEdition
{
    public class HomingBeaconAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
        }
        public override void DeactivateAbility()
        {
        }
    }
}