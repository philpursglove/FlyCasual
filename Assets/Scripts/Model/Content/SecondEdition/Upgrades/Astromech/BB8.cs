using ActionsList;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class BB8 : GenericUpgrade
    {
        public BB8() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "BB-8",
                UpgradeType.Astromech,
                charges: 2,
                cost: 4,
                isLimited: true,
                restriction: new FactionRestriction(Faction.Resistance),
                abilityType: typeof(Abilities.SecondEdition.BB8Ability),
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class BB8XWA : BB8
    {
        public BB8XWA() : base()
        {
            UpgradeInfo.Cost = 5;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    //Before you execute a blue maneuver, you may spend 1 charge to perform a barrel roll or boost action.
    public class BB8Ability : BBAstromechAbility
    {
        public BB8Ability()
        {
            AbilityActions = new List<GenericAction> { new BarrelRollAction(), new BoostAction() };
        }
    }
}