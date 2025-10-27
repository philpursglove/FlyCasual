using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class Hate : GenericUpgrade
    {
        public Hate() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Hate",
                UpgradeType.ForcePower,
                cost: 5,
                restriction: new TagRestriction(Tags.DarkSide),
                abilityType: typeof(Abilities.SecondEdition.HateAbility),
                legalityInfo: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class HateXWA : Hate
    {
        public HateXWA() : base()
        {
            UpgradeInfo.Cost = 4;
            UpgradeInfo.LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class HateAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnShieldLost += RegisterHateAbilityShield;
            HostShip.OnDamageCardIsDealt += RegisterHateAbilityHull;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnShieldLost -= RegisterHateAbilityShield;
            HostShip.OnDamageCardIsDealt -= RegisterHateAbilityHull;
        }

        private void RegisterHateAbilityShield()
        {
            RegisterAbilityTrigger(TriggerTypes.OnShieldIsLost, RecoverForceToken);
        }

        private void RegisterHateAbilityHull(GenericShip ship)
        {
            RegisterAbilityTrigger(TriggerTypes.OnDamageCardIsDealt, RecoverForceToken);
        }

        private void RecoverForceToken(object sender, System.EventArgs e)
        {
            if (HostShip.State.Force < HostShip.State.MaxForce)
            {
                HostShip.State.RestoreForce();
            }
            Triggers.FinishTrigger();
        }
    }
}