using Actions;
using ActionsList;
using Content;
using System;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship.SecondEdition.HyenaClassDroidBomber
{
    public class DBS32CSoC : HyenaClassDroidBomber
    {
        public DBS32CSoC()
        {
            PilotInfo = new PilotCardInfo25
            (
                "DBS-32C",
                "Siege of Coruscant",
                Faction.Separatists,
                3,
                3,
                0,
                charges: 2,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.DBS32CSoCAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Torpedo,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Droid
                },
                isStandardLayout: true,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            ShipInfo.ActionIcons.RemoveActions(typeof(ReloadAction));
            ShipInfo.ActionIcons.AddActions(new ActionInfo(typeof(JamAction), ActionColor.Red));

            MustHaveUpgrades.Add(typeof(PlasmaTorpedoes));
            MustHaveUpgrades.Add(typeof(ContingencyProtocol));
            MustHaveUpgrades.Add(typeof(StrutLockOverride));

            PilotNameCanonical = "dbs32c-siegeofcoruscant";
        }
    }

    public class DBS32CSoCXWA : DBS32CSoC
    {
        public DBS32CSoCXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 4;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class DBS32CSoCAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= CheckAbility;
        }

        private void CheckAbility(GenericAction action)
        {
            if (action is CalculateAction && HostShip.State.Charges > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AskToPerformJamAction);
            }
        }

        private void AskToPerformJamAction(object sender, EventArgs e)
        {
            HostShip.BeforeActionIsPerformed += CheckSpendCharge;

            HostShip.AskPerformFreeAction
            (
                new JamAction(),
                FinishAbility,
                descriptionShort: HostShip.PilotInfo.PilotName,
                descriptionLong: "You may spend 1 Charge to perform a Jam action"
            );
        }

        private void FinishAbility()
        {
            HostShip.BeforeActionIsPerformed -= CheckSpendCharge;
            Triggers.FinishTrigger();
        }

        private void CheckSpendCharge(GenericAction action, ref bool data)
        {
            if (action is JamAction) HostShip.State.Charges--;
        }
    }
}