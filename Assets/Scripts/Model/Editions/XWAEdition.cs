using Ship;
using Upgrade;

namespace Editions
{
    public class XWAEdition : SecondEdition
    {
        // Return Second Edition to use same UI panels and pieces without duplicating entire chunks
        //public override string Name { get { return "XWA Edition"; } }
        //public override string NameShort { get { return "XWAEdition"; } }
        public override int MaxPoints { get { return 50; } }

        public XWAEdition() : base() { }

        public override string GetPilotImageUrl(GenericShip ship, string filename)
        {
            return "https://infinitearenas.com/xw2xwa/images/pilots/" + ship.PilotNameCanonical + ".png";
        }

        public override string GetUpgradeImageUrl(GenericUpgrade upgrade, string filename = null)
        {
            return "https://infinitearenas.com/xw2xwa/images/upgrades/" + upgrade.NameCanonical + ".png";
        }
    }
}