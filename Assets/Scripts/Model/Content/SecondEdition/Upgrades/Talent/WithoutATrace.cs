using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class WithoutATrace : GenericUpgrade
    {
        public WithoutATrace()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Without a Trace",
                UpgradeType.Talent,
                abilityType: typeof(Abilities.SecondEdition.WithoutATraceAbility)            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class WithoutATraceAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
        }
        public override void DeactivateAbility()
        {
        }
    }
}