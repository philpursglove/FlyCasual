﻿using ActionsList;
using Movement;
using Ship;
using System.Collections.Generic;
using Upgrade;
namespace UpgradesList.SecondEdition
{
    public class PatternAnalyzer : GenericUpgrade
    {
        public PatternAnalyzer() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Pattern Analyzer",
                UpgradeType.Tech,
                cost: 5,
                abilityType: typeof(Abilities.SecondEdition.PatternAnalyzerAbility)
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    public class PatternAnalyzerAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnMovementExecuted += RegisterPatternAnalyzer;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementExecuted -= RegisterPatternAnalyzer;
        }

        private void RegisterPatternAnalyzer(GenericShip ship)
        {
            if (HostShip.GetLastManeuverColor() == MovementComplexity.Complex && (HostShip.AssignedManeuver.Speed == 0 || !HostShip.IsBumped) && !BoardTools.Board.IsOffTheBoard(HostShip))
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementExecuted, UsePatternAnalyzer);
            }
        }

        private void UsePatternAnalyzer(object sender, System.EventArgs e)
        {
            List<GenericAction> actions = Selection.ThisShip.GetAvailableActions();
            HostShip.AskPerformFreeAction(
                actions,
                Triggers.FinishTrigger,
                HostUpgrade.UpgradeInfo.Name,
                "While you fully execute a red maneuver, before the Check Difficulty step, you may perform 1 action",
                HostUpgrade
            );
        }
    }
}