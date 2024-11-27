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
            HostShip.OnShipIsDestroyed += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnShipIsDestroyed -= CheckAbility;
        }

        private void CheckAbility(GenericShip ship, bool flag)
        {
            List<GenericShip> friendlyShipsAtRange = Board.GetShipsAtRange(ship, new UnityEngine.Vector2(0, 3), Team.Type.Friendly).Where<GenericShip>(s => s.UpgradeBar.HasUpgradeInstalled(typeof(ContingencyProtocol)) && s != HostShip).ToList();

            if(friendlyShipsAtRange.Count > 0)
            {
                SelectTargetForAbility(
                    ActivateContingencyProtocol,
                    FilterTargets,
                    GetAiPriority,
                    HostShip.Owner.PlayerNo,
                    name: HostUpgrade.UpgradeInfo.Name,
                    description: "Selected ship may perform an action even while stressed",
                    imageSource: HostUpgrade);
            }
        }

        private void ActivateContingencyProtocol()
        {
            SelectShipSubPhase.FinishSelectionNoCallback();

            //Selection.ChangeActiveShip(Selection.AnotherShip);

            Selection.AnotherShip.OnCanPerformActionWhileStressed += AllowActionsWhileStressed;

            Selection.AnotherShip.AskPerformFreeAction
            (
                Selection.AnotherShip.GetAvailableActions(),
                FinishAbility,
                descriptionShort: HostUpgrade.UpgradeInfo.Name,
                descriptionLong: "You may perform an action even while stressed"
            );
        }

        private bool FilterTargets(GenericShip ship)
        {
            bool result = false;

            if (ship != HostShip && Tools.IsFriendly(HostShip, ship))
            {
                if(new DistanceInfo(HostShip, ship).Range <= 3)
                {
                    if (ship.UpgradeBar.HasUpgradeInstalled(typeof(ContingencyProtocol)))
                    {
                        result = true;
                    }
                }                
            }

            return result;
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
            Selection.AnotherShip.OnCanPerformActionWhileStressed -= AllowActionsWhileStressed;

            //Selection.ChangeActiveShip(HostShip);
        }
    }
}
