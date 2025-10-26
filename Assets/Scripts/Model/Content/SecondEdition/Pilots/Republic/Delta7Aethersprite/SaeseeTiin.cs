using Content;
using Movement;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.Delta7Aethersprite
{
    public class SaeseeTiin : Delta7Aethersprite
    {
        public SaeseeTiin()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Saesee Tiin",
                "Prophetic Pilot",
                Faction.Republic,
                4,
                4,
                8,
                isLimited: true,
                force: 2,
                abilityType: typeof(Abilities.SecondEdition.SaeseeTiinAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                    UpgradeType.Astromech,
                    UpgradeType.Configuration,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Jedi,
                    Tags.LightSide
                },
                legality: new List<Legality>
                {
                    Legality.StandardBanned,
                    Legality.ExtendedLegal
                },
                skinName: "Saesee Tiin"
            );
        }
    }

    public class SaeseeTiinXWA : SaeseeTiin
    {
        public SaeseeTiinXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 10;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 7;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                    UpgradeType.ForcePower,
                    UpgradeType.ForcePower,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                    UpgradeType.Configuration,
            };
        }
    }
}

namespace Abilities.SecondEdition
{
    //After a friendly ship at range 0-2 reveals its dial, you may spend 1 force. 
    //If you do, set its dial to another maneuver of the same speed and difficulty.
    public class SaeseeTiinAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnManeuverIsRevealedGlobal += RegisterAskChangeManeuver;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnManeuverIsRevealedGlobal -= RegisterAskChangeManeuver;
        }

        private void RegisterAskChangeManeuver(GenericShip ship)
        {
            if (HostShip.State.Force > 0
                && ship.Owner == HostShip.Owner
                && new BoardTools.DistanceInfo(ship, HostShip).Range < 3)
            {
                TargetShip = ship;
                RegisterAbilityTrigger(TriggerTypes.OnManeuverIsRevealed, AskChangeManeuver);
            }
        }

        private void AskChangeManeuver(object sender, System.EventArgs e)
        {
            Messages.ShowInfoToHuman(HostName + ": You may change the maneuver");
            TargetShip.Owner.ChangeManeuver(ManeuverSelected, Triggers.FinishTrigger, IsSameComplexityAndSpeed);
        }

        private void ManeuverSelected(string maneuverString)
        {
            if (maneuverString != TargetShip.AssignedManeuver.ToString())
            {
                HostShip.State.SpendForce(
                    1,
                    delegate { ShipMovementScript.SendAssignManeuverCommand(maneuverString); }
                );
            }
            else
            {
                ShipMovementScript.SendAssignManeuverCommand(maneuverString);
            }
        }

        private bool IsSameComplexityAndSpeed(string maneuverString)
        {
            ManeuverHolder movementStruct = new ManeuverHolder(maneuverString);

            return movementStruct.ColorComplexity == TargetShip.AssignedManeuver.ColorComplexity
                && movementStruct.SpeedIntUnsigned == TargetShip.AssignedManeuver.Speed;
        }
    }
}
