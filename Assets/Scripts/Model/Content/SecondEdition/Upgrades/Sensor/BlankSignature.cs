using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class BlankSignature : GenericUpgrade
    {
        public BlankSignature()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Blank Signature",
                UpgradeType.Sensor,
                regensCharges: true,
                charges:1
            );
            IsHidden = true;

        }

    }
}