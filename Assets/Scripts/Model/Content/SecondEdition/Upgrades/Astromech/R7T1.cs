using Actions;
using ActionsList;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class R7T1 : GenericUpgrade
    {
        public R7T1() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "R7-T1",
                UpgradeType.Astromech,
                cost: 1, // TODO: Update cost
                isLimited: true,
                addActionLink: new LinkedActionInfo(typeof(TargetLockAction), typeof(BoostAction)),
                restrictions: new UpgradeCardRestrictions(
                    new ActionBarRestriction(typeof(TargetLockAction), ActionColor.White),
                    new BaseSizeRestriction(Ship.BaseSize.Small),
                    new FactionRestriction(Faction.Resistance, Faction.Rebel)
                )
            );
        }
    }
}