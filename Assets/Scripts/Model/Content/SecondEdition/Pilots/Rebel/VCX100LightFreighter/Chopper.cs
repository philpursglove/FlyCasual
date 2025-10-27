using Content;
using Ship;
using System.Collections.Generic;
using System.Linq;
using Tokens;
using Upgrade;

namespace Ship.SecondEdition.VCX100LightFreighter
{
    public class Chopper : VCX100LightFreighter
    {
        public Chopper() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "\"Chopper\"",
                "Spectre-3",
                Faction.Rebel,
                2,
                6,
                14,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.ChopperPilotAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Sensor,
                    UpgradeType.Turret,
                    UpgradeType.Torpedo,
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Title
                },
                tags: new List<Tags>
                {
                    Tags.Droid,
                    Tags.Freighter,
                    Tags.Spectre
                },
                seImageNumber: 75,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            ShipInfo.ActionIcons.SwitchToDroidActions();
        }
    }

    public class ChopperXWA : Chopper
    {
        public ChopperXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 16;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 12;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Crew,
                UpgradeType.Crew,
                UpgradeType.Sensor,
                UpgradeType.Gunner,
                UpgradeType.Modification,
                UpgradeType.Turret,
                UpgradeType.Torpedo,
                UpgradeType.Title
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class ChopperPilotAbility : GenericAbility
    {
        protected List<GenericShip> shipsToAssignStress;

        public override void ActivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers += RegisterPilotAbility;
        }

        public override void DeactivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers -= RegisterPilotAbility;
        }

        private void RegisterPilotAbility()
        {
            RegisterAbilityTrigger(TriggerTypes.OnCombatPhaseStart, AssignStressTokens);
        }

        private void AssignStressTokens(object sender, System.EventArgs e)
        {
            shipsToAssignStress = new List<GenericShip>(HostShip.ShipsBumped.Where(n => n.Owner.PlayerNo != HostShip.Owner.PlayerNo));
            AssignStressTokenRecursive();
        }

        private void AssignStressTokenRecursive()
        {
            if (shipsToAssignStress.Count > 0)
            {
                GenericShip shipToAssignStress = shipsToAssignStress[0];
                shipsToAssignStress.Remove(shipToAssignStress);
                Messages.ShowErrorToHuman(shipToAssignStress.PilotInfo.PilotName + " is at range 0 of " + HostShip.PilotInfo.PilotName + " and gains 2 jam tokens");
                shipToAssignStress.Tokens.AssignTokens(() => new JamToken(shipToAssignStress, HostShip.Owner), 2, AssignStressTokenRecursive);
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }
    }
}