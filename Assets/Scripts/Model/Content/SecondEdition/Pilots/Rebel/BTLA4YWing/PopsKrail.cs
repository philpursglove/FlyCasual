using Abilities.SecondEdition;
using ActionsList;
using Content;
using Ship;
using SubPhases;
using System.Collections.Generic;
using System.Linq;
using Tokens;
using Upgrade;

namespace Ship.SecondEdition.BTLA4YWing
{
    public class PopsKrail : BTLA4YWing
    {
        public PopsKrail() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "\"Pops\" Krail",
                "Gold Five",
                Faction.Rebel,
                3,
                4,
                16,
                isLimited: true,
                abilityType: typeof(PopsKrailAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Turret,
                    UpgradeType.Torpedo,
                    UpgradeType.Astromech,
                    UpgradeType.Device,
                    UpgradeType.Missile,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.YWing
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class PopsKrailXWA : PopsKrail
    {
        public PopsKrailXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 9;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 11;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Astromech,
                UpgradeType.Modification,
                UpgradeType.Device,
                UpgradeType.Turret,
                UpgradeType.Torpedo
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class PopsKrailAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully += CheckPopsKrailPilotAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementFinishSuccessfully -= CheckPopsKrailPilotAbility;
        }

        private void CheckPopsKrailPilotAbility(GenericShip ship)
        {
            if (BoardTools.Board.IsOffTheBoard(ship)) return;

            if (ship.AssignedManeuver.ColorComplexity == Movement.MovementComplexity.Normal)
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, PopsKrailPilotAbility);
            }
        }

        private void PopsKrailPilotAbility(object sender, System.EventArgs e)
        {
            SelectTargetForAbility(
                GrantFreeFocusAction,
                FilterAbilityTargets,
                GetAiAbilityPriority,
                HostShip.Owner.PlayerNo,
                HostShip.PilotInfo.PilotName,
                "Choose a ship.\nIt may perform a focus action.",
                HostShip
            );
        }

        private bool FilterAbilityTargets(GenericShip ship)
        {
            return FilterByTargetType(ship, new List<TargetTypes>() { TargetTypes.This, TargetTypes.OtherFriendly }) && FilterTargetsByRange(ship, 0, 1);
        }

        private int GetAiAbilityPriority(GenericShip ship)
        {
            int result = 0;

            result += NeedTokenPriority(ship);
            result += ship.PilotInfo.Cost + ship.UpgradeBar.GetUpgradesOnlyFaceup().Sum(n => n.UpgradeInfo.Cost);

            return result;
        }

        private int NeedTokenPriority(GenericShip ship)
        {
            if (!ship.Tokens.HasToken(typeof(FocusToken)) && !ship.IsStressed) return 100;
            return 0;
        }

        private void GrantFreeFocusAction()
        {
            Selection.ThisShip = TargetShip;

            RegisterAbilityTrigger(TriggerTypes.OnFreeActionPlanned, PerformFreeFocusAction);

            Triggers.ResolveTriggers(TriggerTypes.OnFreeActionPlanned, SelectShipSubPhase.FinishSelection);
        }

        protected virtual void PerformFreeFocusAction(object sender, System.EventArgs e)
        {
            TargetShip.AskPerformFreeAction(
                new FocusAction(),
                delegate
                {
                    Selection.ThisShip = HostShip;
                    Phases.CurrentSubPhase.Resume();
                    Triggers.FinishTrigger();
                },
                HostShip.PilotInfo.PilotName,
                "You may perform a focus action",
                HostShip
            );
        }
    }
}