using ActionsList;
using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class ExtremeManeuvers : GenericUpgrade
    {
        public ExtremeManeuvers() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Extreme Maneuvers",
                UpgradeType.ForcePower,
                cost: 8,
                abilityType: typeof(Abilities.SecondEdition.ExtremeManeuversAbility),
                restrictions: new UpgradeCardRestrictions(
                    new BaseSizeRestriction(BaseSize.Small),
                    new ActionBarRestriction(typeof(BoostAction))
                ),
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class ExtremeManeuversXWA : ExtremeManeuvers
    {
        public ExtremeManeuversXWA() : base()
        {
            UpgradeInfo.Cost = 5;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class ExtremeManeuversAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnGetAvailableBoostTemplates += ChangeBoostTemplates;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnGetAvailableBoostTemplates -= ChangeBoostTemplates;
        }

        private void ChangeBoostTemplates(List<BoostMove> availableMoves, GenericAction action)
        {
            if (HostShip.IsCanUseForceNow())
            {
                availableMoves.Add(new BoostMove(ActionsHolder.BoostTemplates.LeftTurn1, isPurple: true));
                availableMoves.Add(new BoostMove(ActionsHolder.BoostTemplates.RightTurn1, isPurple: true));
            }
        }
    }
}