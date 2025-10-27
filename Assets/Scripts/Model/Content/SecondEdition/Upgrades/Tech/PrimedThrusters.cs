using ActionsList;
using Content;
using Ship;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class PrimedThrusters : GenericUpgrade
    {
        public PrimedThrusters() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Primed Thrusters",
                UpgradeType.Tech,
                cost: 6,
                abilityType: typeof(Abilities.SecondEdition.PrimedThrustersAbility),
                restriction: new BaseSizeRestriction(BaseSize.Small),
                legalityInfo: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                //seImageNumber: 69
            );
        }
    }

    public class PrimedThrustersXWA : PrimedThrusters
    {
        public PrimedThrustersXWA() : base()
        {
            UpgradeInfo.Cost = 5;
            UpgradeInfo.LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class PrimedThrustersAbility : GenericAbility
    {
        private bool set = false;

        public override void ActivateAbility()
        {
            HostShip.OnTokenIsAssigned += UsePrimedThruster;
            HostShip.OnTokenIsRemoved += UsePrimedThruster;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnTokenIsAssigned -= UsePrimedThruster;
            HostShip.OnTokenIsRemoved -= UsePrimedThruster;
        }

        private void UsePrimedThruster(GenericShip ship, GenericToken token)
        {
            if (token is StressToken)
            {
                if (!set && HostShip.Tokens.CountTokensByType(typeof(Tokens.StressToken)) <= 2)
                {
                    HostShip.ActionBar.ActionsThatCanbePreformedwhileStressed.Add(typeof(BoostAction));
                    HostShip.ActionBar.ActionsThatCanbePreformedwhileStressed.Add(typeof(BarrelRollAction));
                    set = true;
                }
                else if (set && HostShip.Tokens.CountTokensByType(typeof(Tokens.StressToken)) > 2)
                {
                    HostShip.ActionBar.ActionsThatCanbePreformedwhileStressed.Remove(typeof(BoostAction));
                    HostShip.ActionBar.ActionsThatCanbePreformedwhileStressed.Remove(typeof(BarrelRollAction));
                    set = false;
                }
            }
        }
    }
}