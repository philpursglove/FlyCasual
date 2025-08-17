using ActionsList;
using BoardTools;
using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.RogueClassStarfighter
    {
        public class CadBaneSeparatist : RogueClassStarfighter
        {
            public CadBaneSeparatist() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Cad Bane",
                    "Needs No Introduction",
                    Faction.Separatists,
                    4,
                    4,
                    13,
                    isLimited: true,
                    charges: 1,
                    regensCharges: 1,
                    abilityType: typeof(Abilities.SecondEdition.CadBaneSeparatistAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Cannon,
                        UpgradeType.Cannon,
                        UpgradeType.Missile,
                        UpgradeType.Illicit,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Title
                    },
                    tags: new List<Tags>()
                    {
                        Tags.BountyHunter
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "cadbane-separatistalliance";
            }
        }

        public class CadBaneSeparatistXWA : CadBaneSeparatist
        {
            public CadBaneSeparatistXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 11;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Illicit,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Cannon,
                    UpgradeType.Cannon,
                    UpgradeType.Missile,
                    UpgradeType.Title,
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class CadBaneSeparatistAbility : GenericAbility
    {
        private GenericShip PreviousCurrentShip { get; set; }
        public override void ActivateAbility()
        {
            GenericShip.OnShipIsDestroyedGlobal += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnShipIsDestroyedGlobal -= CheckAbility;
        }
        private void CheckAbility(GenericShip ship, bool flag)
        {
            if (!(Phases.CurrentPhase is MainPhases.CombatPhase) || HostShip.State.Charges < 1)
                return;

            DistanceInfo distanceInfo = new DistanceInfo(HostShip, ship);
            if (distanceInfo.Range > 3) return;

            RegisterAbilityTrigger(TriggerTypes.OnShipIsDestroyed, PerformAction);
        }

        private void PerformAction(object sender, System.EventArgs e)
        {
            var ship = Selection.ThisShip;
            Roster.HighlightPlayer(HostShip.Owner.PlayerNo);
            Selection.ChangeActiveShip(HostShip);

            CameraScript.RestoreCamera();

            HostShip.OnCanPerformActionWhileStressed += TemporaryAllowAnyActionsWhileStressed;
            HostShip.OnCheckCanPerformActionsWhileStressed += TemporaryAllowActionsWhileStressed;
            HostShip.OnActionIsPerformed += DisallowActionsWhileStressed;
            HostShip.OnActionIsSkipped += DisallowActionsWhileStressedAlt;

            List<GenericAction> actions = Selection.ThisShip.GetAvailableActions();

            Messages.ShowInfoToHuman(HostName + ": you may spend 1 charge to perform an action");

            HostShip.BeforeActionIsPerformed += SpendCharge;

            HostShip.AskPerformFreeAction(
                actions,
                delegate {
                    Roster.HighlightPlayer(ship.Owner.PlayerNo);
                    Selection.ChangeActiveShip(ship);
                    Triggers.FinishTrigger();
                },
                HostShip.PilotInfo.PilotName,
                "After another ship at range 0-3 is destroyed, you may spend 1 charge to perform an action, even while stressed",
                HostShip
            );
        }

        private void SpendCharge(GenericAction action, ref bool isFreeAction)
        {
            HostShip.BeforeActionIsPerformed -= SpendCharge;
            HostShip.SpendCharge();
        }

        private void DisallowActionsWhileStressed(GenericAction action)
        {
            HostShip.OnCanPerformActionWhileStressed -= TemporaryAllowAnyActionsWhileStressed;
            HostShip.OnCheckCanPerformActionsWhileStressed -= TemporaryAllowActionsWhileStressed;
            HostShip.OnActionIsPerformed -= DisallowActionsWhileStressed;
        }

        private void DisallowActionsWhileStressedAlt(GenericShip ship)
        {
            HostShip.OnCanPerformActionWhileStressed -= TemporaryAllowAnyActionsWhileStressed;
            HostShip.OnCheckCanPerformActionsWhileStressed -= TemporaryAllowActionsWhileStressed;
            HostShip.OnActionIsPerformed -= DisallowActionsWhileStressed;
            HostShip.OnActionIsSkipped -= DisallowActionsWhileStressedAlt;
        }

        private void TemporaryAllowAnyActionsWhileStressed(GenericAction action, ref bool isAllowed)
        {
            isAllowed = true;
        }

        private void TemporaryAllowActionsWhileStressed(ref bool isAllowed)
        {
            isAllowed = true;
        }
    }
}