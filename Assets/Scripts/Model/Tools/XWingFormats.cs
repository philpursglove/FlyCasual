using Ship;
using Upgrade;

namespace Content
{
    public static class XWingFormats
    {
        public static bool IsShipLegalForFormat(GenericShip ship, Legality legality)
        {
            return (ship.ShipInfo as ShipCardInfo25).LegalityInfo.Contains(legality);
        }

        public static bool IsLegalForFormat(GenericShip ship, Legality legality)
        {
            return (ship.PilotInfo as PilotCardInfo25).LegalityInfo.Contains(legality);
        }

        public static bool IsLegalForFormat(GenericUpgrade upgrade, Legality legality)
        {
            return upgrade.UpgradeInfo.LegalityInfo.Contains(legality);
        }

        public static bool IsBanned(GenericShip ship, string legality)
        {
            return IsBanned(ship, Options.GetFormatAsLegality(legality));
        }

        public static bool IsBanned(GenericShip ship, Legality legality)
        {
            switch (legality)
            {
                case Legality.StandardLegal:
                    return (ship.PilotInfo as PilotCardInfo25).LegalityInfo.Contains(Legality.StandardBanned);
                case Legality.ExtendedLegal:
                    return (ship.PilotInfo as PilotCardInfo25).LegalityInfo.Contains(Legality.ExtendedBanned);
                case Legality.XWA:
                    return (ship.PilotInfo as PilotCardInfo25).LegalityInfo.Contains(Legality.XWABanned);
                default:
                    return false;
            }
        }

        public static bool IsBanned(GenericUpgrade upgrade, string legality)
        {
            return IsBanned(upgrade, Options.GetFormatAsLegality(legality));
        }

        public static bool IsBanned(GenericUpgrade upgrade, Legality legality)
        {
            switch (legality)
            {
                case Legality.StandardLegal:
                    return upgrade.UpgradeInfo.LegalityInfo.Contains(Legality.StandardBanned);
                case Legality.ExtendedLegal:
                    return upgrade.UpgradeInfo.LegalityInfo.Contains(Legality.ExtendedBanned);
                case Legality.XWA:
                    return upgrade.UpgradeInfo.LegalityInfo.Contains(Legality.XWABanned);
                default:
                    return false;
            }
        }
    }
}