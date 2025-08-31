using Abilities.SecondEdition;
using System;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class TopCover : GenericUpgrade
    {
        public TopCover()
        {
            UpgradeInfo = new UpgradeCardInfo(
                name: "Top Cover",
                type: UpgradeType.Talent,
                cost: 0,
                abilityType: typeof(TopCoverAbility));
            IsHidden = true;
            IsWIP = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class TopCoverAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            throw new NotImplementedException();
        }

        public override void DeactivateAbility()
        {
            throw new NotImplementedException();
        }
    }
}
