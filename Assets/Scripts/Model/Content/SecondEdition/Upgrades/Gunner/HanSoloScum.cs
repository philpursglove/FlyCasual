using Actions;
using ActionsList;
using Content;
using Ship;
using UnityEngine;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class HanSoloScum : GenericUpgrade
    {
        public HanSoloScum() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Han Solo",
                UpgradeType.Gunner,
                cost: 9,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.HanSoloScumGunnerAbility),
                restriction: new FactionRestriction(Faction.Scum),
                seImageNumber: 163,
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            Avatar = new AvatarInfo(
                Faction.Scum,
                new Vector2(309, 1),
                new Vector2(75, 75)
            );

            NameCanonical = "hansolo-gunner";
        }
    }

    public class HanSoloScumXWA : HanSoloScum
    {
        public HanSoloScumXWA() : base()
        {
            UpgradeInfo.Cost = 7;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class HanSoloScumGunnerAbility : GenericAbility
    {
        // Before you engage, you may perform a red (Focus) action.

        public override void ActivateAbility()
        {
            HostShip.OnCombatActivation += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnCombatActivation -= CheckAbility;
        }

        private void CheckAbility(GenericShip ship)
        {
            RegisterAbilityTrigger(TriggerTypes.OnCombatActivation, UseAbility);
        }

        private void UseAbility(object sender, System.EventArgs e)
        {
            HostShip.AskPerformFreeAction(
                new FocusAction() { Color = ActionColor.Red },
                Triggers.FinishTrigger,
                HostUpgrade.UpgradeInfo.Name,
                "Before you engage, you may perform a red Focus action.",
                HostUpgrade
            );
        }
    }
}