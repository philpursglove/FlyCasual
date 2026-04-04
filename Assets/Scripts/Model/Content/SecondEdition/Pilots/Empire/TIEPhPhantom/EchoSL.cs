using ActionsList;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship.SecondEdition.TIEPhPhantom
{
    public class EchoSL : TIEPhPhantom
    {
        public EchoSL() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "\"Echo\"",
                "Copycat",
                Faction.Imperial,
                4,
                5,
                0,
                isLimited: true,
                isStandardLayout: true,
                abilityType: typeof(Abilities.SecondEdition.EchoSLAbility),
                tags: new List<Tags>
                {
                    Tags.Tie
                },
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Talent,
                    UpgradeType.Modification
                },
                charges: 1,
                regensCharges: 1,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            ImageUrl = "https://infinitearenas.com/xw2/images/quickbuilds/echo-ssl.png";

            PilotNameCanonical = "echo-ssl";

            MustHaveUpgrades.Add(typeof(SilentHunter));
            MustHaveUpgrades.Add(typeof(StealthGambit));
            MustHaveUpgrades.Add(typeof(ManualAilerons));
        }
    }

    public class EchoSLXWA : EchoSL
    {
        public EchoSLXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 14;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };

            ImageUrl = "https://infinitearenas.com/xw2xwa/images/quickbuilds/echo-ssl.png";
        }
    }
}


namespace Abilities.SecondEdition
{
    public class EchoSLAbility : GenericAbility
    {
        // After an enemy ship at range 0-1 performs an action on its action bar,
        // you may spend 1 charge to perform the same action, treating it as white.

        GenericAction savedAction;

        public override void ActivateAbility()
        {
            GenericShip.OnActionIsPerformedGlobal += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnActionIsPerformedGlobal -= CheckAbility;
        }

        private void CheckAbility(GenericAction action)
        {
            if (HostShip.State.Charges > 0 &&
                !HostShip.IsStressed &&
                !Tools.IsFriendly(action.HostShip, HostShip) &&
                HostShip.GetRangeToShip(action.HostShip) < 2 &&
                action.IsInActionBar &&
                HostShip.ActionBar.HasAction(action.GetType()))
            {
                savedAction = action;
                savedAction.Color = Actions.ActionColor.White;
                RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AskToUseAbility);
            }
        }

        private void AskToUseAbility(object sender, EventArgs e)
        {
            if (HostShip.State.Charges < 1) return; // Linked actions may queue multiple abilities here

            HostShip.OnActionIsPerformed += SpendCharge;

            Selection.ThisShip = HostShip;

            HostShip.AskPerformFreeAction(
                savedAction,
                CleanUp,
                HostShip.PilotInfo.PilotName,
                descriptionLong: $"You may spend 1 charge to gain the same action that {savedAction.HostShip.PilotInfo.PilotName} just performed"
            );
        }

        private void SpendCharge(GenericAction action)
        {
            HostShip.SpendCharge();
        }

        private void CleanUp()
        {
            HostShip.OnActionIsPerformed -= SpendCharge;

            Selection.ThisShip = savedAction.HostShip;

            Triggers.FinishTrigger();
        }
    }
}
