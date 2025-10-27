using Actions;
using ActionsList;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class FirstOrderOrdnanceTech : GenericUpgrade
    {
        public FirstOrderOrdnanceTech() : base()
        {
            UpgradeInfo = new UpgradeCardInfo
            (
                "First Order Ordnance Tech",
                UpgradeType.Gunner,
                cost: 3,
                restriction: new FactionRestriction(Faction.FirstOrder),
                addAction: new ActionInfo(typeof(ReloadAction)),
                addActionLink: new LinkedActionInfo(typeof(ReloadAction), typeof(TargetLockAction), linkedColor: ActionColor.White),
                legalityInfo: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class FirstOrderOrdnanceTechXWA : FirstOrderOrdnanceTech
    {
        public FirstOrderOrdnanceTechXWA() : base()
        {
            UpgradeInfo.Cost = 2;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}