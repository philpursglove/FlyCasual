using Abilities.SecondEdition;
using ActionsList;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.ResistanceTransportPod
{
    public class BB8 : ResistanceTransportPod
    {
        public BB8()
        {
            PilotInfo = new PilotCardInfo25
            (
                "BB-8",
                "Full of Surprises",
                Faction.Resistance,
                3,
                2,
                4,
                isLimited: true,
                abilityType: typeof(BB8TransportPodAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {

                    UpgradeType.Tech,
                    UpgradeType.Tech,
                    UpgradeType.Crew,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Droid
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            ShipInfo.ActionIcons.SwitchToDroidActions();
        }
    }

    public class BB8XWA : BB8
    {
        public BB8XWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 7;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Crew,
                UpgradeType.Modification,
                UpgradeType.Tech
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class BB8TransportPodAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnSystemsAbilityActivation += RegisterOwnTrigger;
            HostShip.OnCheckSystemsAbilityActivation += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnSystemsAbilityActivation -= RegisterOwnTrigger;
            HostShip.OnCheckSystemsAbilityActivation -= CheckAbility;
        }

        private void CheckAbility(GenericShip ship, ref bool flag)
        {
            flag = true;
        }

        private void RegisterOwnTrigger(GenericShip ship)
        {
            // Always register
            RegisterAbilityTrigger(TriggerTypes.OnSystemsAbilityActivation, AskToPerformReposition);
        }

        private void AskToPerformReposition(object sender, EventArgs e)
        {
            Sounds.PlayShipSound("BB-8-Sound");

            HostShip.AskPerformFreeAction(
                new List<GenericAction>()
                {
                    new BarrelRollAction(){Color = Actions.ActionColor.Red},
                    new BoostAction(){Color = Actions.ActionColor.Red}
                },
                Triggers.FinishTrigger,
                HostShip.PilotInfo.PilotName,
                "During the System Phase, you may perform a red Barrel Roll or Boost action",
                HostShip
            );
        }
    }
}