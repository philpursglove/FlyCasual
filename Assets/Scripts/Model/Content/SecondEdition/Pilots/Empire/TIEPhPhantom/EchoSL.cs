using ActionsList;
using Content;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using Tokens;
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
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
            ImageUrl = "https://infinitearenas.com/xw2/images/quickbuilds/echo-tiephphantom.png";
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
        }
    }

}


namespace Abilities.SecondEdition
{
    public class EchoSLAbility : GenericAbility
    {
        public GenericAction Action { get; set; }
        public override void ActivateAbility()
        {
            // Ability implementation goes here
            GenericShip.OnActionIsPerformedGlobal += CheckAbility;
        }

        private void CheckAbility(GenericAction action)
        {
            // Is the action by an enemy ship?
            bool actionIsByEnemy = action.HostShip.Owner.PlayerNo != HostShip.Owner.PlayerNo;

            //Is it range 0-1
            bool inRange = new BoardTools.DistanceInfo(HostShip, action.HostShip).Range < 2;

            //Do we have a charge left
            bool chargeAvailable = HostShip.State.Charges > 0;
            //Are we already stressed
            bool stressed = HostShip.Tokens.HasToken<StressToken>();

            if (actionIsByEnemy && inRange && chargeAvailable && !stressed)
            {
                Action = action;
                RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AskToUseAbility);
            }
        }

        private void AskToUseAbility(object sender, EventArgs e)
        {
            AskToUseAbility("Echo's ability",
                NeverUseByDefault,
                descriptionLong: "You may spend 1 charge to gain the same action that ship just performed",
                useAbility: UseAbility);

        }

        private void UseAbility(object sender, EventArgs e)
        {
            HostShip.SpendCharge();
            ActionHostActionSubPhase subphase = Phases.StartTemporarySubPhaseNew<ActionHostActionSubPhase>(
                "Echo's ability",
                Triggers.FinishTrigger
            );
            subphase.Start();

        }

        public override void DeactivateAbility()
        {
            // Ability deactivation logic goes here
            GenericShip.OnActionIsPerformedGlobal -= CheckAbility;
        }
    }

    internal class ActionHostActionSubPhase : GenericSubPhase
    {
    }
}
