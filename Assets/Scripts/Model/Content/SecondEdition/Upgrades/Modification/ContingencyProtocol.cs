using ActionsList;
using BoardTools;
using Ship;
using SubPhases;
using System.Collections.Generic;
using System.Linq;
using Upgrade;
using UpgradesList.SecondEdition;

namespace UpgradesList.SecondEdition
{
    public class ContingencyProtocol : GenericUpgrade
    {
        public ContingencyProtocol() : base()
        {
            IsHidden = true;

            UpgradeInfo = new UpgradeCardInfo
            (
                "Contingency Protocol",
                UpgradeType.Modification,
                cost: 0,
                abilityType: typeof(Abilities.SecondEdition.ContingencyProtocolAbility)
            );

            ImageUrl = "https://i.imgur.com/5MMMAtf.jpg";
        }
        
    }
}

namespace Abilities.SecondEdition
{
    public class ContingencyProtocolAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnShipIsDestroyed += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnShipIsDestroyed -= RegisterAbility;
        }

        private void RegisterAbility(GenericShip ship, bool flag)
        {
            List<GenericShip> friendlyShipsAtRange = Board.GetShipsAtRange(ship, new UnityEngine.Vector2(0, 3), Team.Type.Friendly).Where<GenericShip>(s => s.UpgradeBar.HasUpgradeInstalled(typeof(ContingencyProtocol)) && s != HostShip).ToList();

            if (friendlyShipsAtRange.Count > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.OnShipIsDestroyed, CheckAbility);
            }
        }

        private void CheckAbility(object sender, System.EventArgs e)
        {

            SelectTargetForAbility(
                ActivateContingencyProtocol,
                FilterTargets,
                GetAiPriority,
                HostShip.Owner.PlayerNo,
                name: HostUpgrade.UpgradeInfo.Name,
                description: "Selected ship may perform an action even while stressed",
                imageSource: HostUpgrade
            );
        }

        private void ActivateContingencyProtocol()
        {
            SelectShipSubPhase.FinishSelectionNoCallback();

            Selection.ChangeActiveShip(Selection.HoveredShip);

            Selection.HoveredShip.OnCanPerformActionWhileStressed += AllowActionsWhileStressed;

            Selection.HoveredShip.AskPerformFreeAction
            (
                Selection.HoveredShip.GetAvailableActions(),
                FinishAbility,
                descriptionShort: HostUpgrade.UpgradeInfo.Name,
                descriptionLong: "You may perform an action even while stressed"
            );
        }

        private bool FilterTargets(GenericShip ship)
        {
            return ship != HostShip && Tools.IsFriendly(HostShip, ship) && new DistanceInfo(HostShip, ship).Range <= 3 && ship.UpgradeBar.HasUpgradeInstalled(typeof(ContingencyProtocol));
        }

        private int GetAiPriority(GenericShip ship)
        {
            return 45;
        }

        private void AllowActionsWhileStressed(GenericAction action, ref bool isAllowed)
        {
            isAllowed = true;
        }

        private void FinishAbility()
        {
            Selection.HoveredShip.OnCanPerformActionWhileStressed -= AllowActionsWhileStressed;

            Selection.ChangeActiveShip(HostShip);

            Triggers.FinishTrigger();
        }
    }
}
