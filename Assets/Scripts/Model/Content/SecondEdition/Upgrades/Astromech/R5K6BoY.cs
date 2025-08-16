using Content;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class R5K6BoY : GenericUpgrade
    {
        public R5K6BoY() : base()
        {
            IsHidden = true;

            UpgradeInfo = new UpgradeCardInfo
            (
                "R5-K6",
                UpgradeType.Astromech,
                cost: 0,
                abilityType: typeof(Abilities.SecondEdition.R5AstromechAbility),
                charges: 2,
                legalityInfo: new List<Legality>
                {
                    Legality.StandardBanned,
                    Legality.ExtendedLegal
                }
            );
        }
    }
}