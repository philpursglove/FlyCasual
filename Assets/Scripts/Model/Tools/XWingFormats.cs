using Ship;
using Upgrade;

namespace Content
{
    public static class XWingFormats
    {
        public static bool IsShipLegalForFormat(GenericShip ship)
        {
            switch (Options.Format)
            {
                case "Standard":
                case "AMG Standard":
                    return (ship.ShipInfo as ShipCardInfo25).LegalityInfo.Contains(Legality.StandardLegal);
                case "Extended":
                case "AMG Extended":
                    return (ship.ShipInfo as ShipCardInfo25).LegalityInfo.Contains(Legality.ExtendedLegal);
                case "XWA":
                    return (ship.ShipInfo as ShipCardInfo25).LegalityInfo.Contains(Legality.XWA);
                default:
                    return false;
            }
        }

        public static bool IsLegalForFormat(GenericShip ship)
        {
            switch (Options.Format)
            {
                case "Standard":
                case "AMG Standard":
                    return (ship.PilotInfo as PilotCardInfo25).LegalityInfo.Contains(Legality.StandardLegal);
                case "Extended":
                case "AMG Extended":
                    return (ship.PilotInfo as PilotCardInfo25).LegalityInfo.Contains(Legality.ExtendedLegal);
                case "XWA":
                    return (ship.PilotInfo as PilotCardInfo25).LegalityInfo.Contains(Legality.XWA);
                default:
                    return false;
            }
        }

        public static bool IsLegalForFormat(GenericUpgrade upgrade)
        {
            switch (Options.Format)
            {
                case "Standard":
                case "AMG Standard":
                    return upgrade.UpgradeInfo.LegalityInfo.Contains(Legality.StandardLegal);
                case "Extended":
                case "AMG Extended":
                    return upgrade.UpgradeInfo.LegalityInfo.Contains(Legality.ExtendedLegal);
                case "XWA":
                    return upgrade.UpgradeInfo.LegalityInfo.Contains(Legality.XWA);
                default:
                    return false;
            }
        }

        public static bool IsBanned(GenericShip ship)
        {
            switch (Options.Format)
            {
                case "Standard":
                case "AMG Standard":
                    return (ship.PilotInfo as PilotCardInfo25).LegalityInfo.Contains(Legality.StandardBanned);
                case "Extended":
                case "AMG Extended":
                    return (ship.PilotInfo as PilotCardInfo25).LegalityInfo.Contains(Legality.ExtendedBanned);
                case "XWA":
                    return (ship.PilotInfo as PilotCardInfo25).LegalityInfo.Contains(Legality.XWABanned);
                default:
                    return false;
            }
        }

        public static bool IsBanned(GenericUpgrade upgrade)
        {
            switch (Options.Format)
            {
                case "Standard":
                case "AMG Standard":
                    return upgrade.UpgradeInfo.LegalityInfo.Contains(Legality.StandardBanned);
                case "Extended":
                case "AMG Extended":
                    return upgrade.UpgradeInfo.LegalityInfo.Contains(Legality.ExtendedBanned);
                case "XWA":
                    return upgrade.UpgradeInfo.LegalityInfo.Contains(Legality.XWABanned);
                default:
                    return false;
            }
        }
    }
}