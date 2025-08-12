using BoardTools;
using Content;
using Ship;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class GamutKey : GenericUpgrade
    {
        public GamutKey()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Gamut Key",
                UpgradeType.Crew,
                cost: 8,
                abilityType: typeof(Abilities.SecondEdition.GamutKeyCrewAbility),
                isLimited: true,
                restriction: new FactionRestriction(Faction.Scum),
                charges: 2,
                regensCharges: true,
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class GamutKeyXWA : GamutKey
    {
        public GamutKeyXWA() : base()
        {
            UpgradeInfo.Cost = 7;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GamutKeyCrewAbility : GamutKeyPilotAbility
    {
        protected override string GetAbilityName()
        {
            return HostUpgrade.UpgradeInfo.Name;
        }

        protected override IImageHolder GetAbilityImage()
        {
            return HostUpgrade;
        }

        protected override bool HasEnoughCharges()
        {
            return HostUpgrade.State.Charges >= 2;
        }

        protected override bool FilterTargets(GenericShip ship)
        {
            DistanceInfo distanceInfo = new DistanceInfo(HostShip, ship);
            return distanceInfo.Range <= 1
                && ship.Tokens.CountTokensByShape(TokenShapes.Cirular) > 0;
        }

        protected override void SpendChargesForAbility()
        {
            HostUpgrade.State.SpendCharges(2);
        }
    }
}