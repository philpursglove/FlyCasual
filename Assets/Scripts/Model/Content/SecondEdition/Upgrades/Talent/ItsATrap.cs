using System;


namespace UpgradesList.SecondEdition
{
    public class ItsATrap : GenericUpgrade
    {
        public ItsATrap() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "It's a Trap!",
                UpgradeType.Talent,
                cost: 0,
                abilityType: typeof(ItsATrapAbility)
            );

            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class ItsATrapAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackStartAsDefender += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsDefender -= RegisterAbility;
        }

        private void RegisterAbility()
        {
            AddDiceModification(
                "It's a Trap! ability",
                IsDiceModificationAvailable,
                GetDiceModificationAiPriority,
                DiceModificationType.Reroll,
                1,
                new List<DieSide> { DieSide.Blank }
            );
        }

        private bool IsDiceModificationAvailable()
        {
            List<GenericShip> friendlyShipsInRangeOne = Board.GetShipsAtRange(HostShip, new Vector2(0, 1), Team.Type.Friendly).Where(ship => ship != HostShip).ToList();
            List<GenericShip> enemyShipsInRangeOne = Board.GetShipsAtRange(HostShip, new Vector2(0, 1), Team.Type.Enemy);

            return friendlyShipsInRangeOne.Count > enemyShipsInRangeOne.Count && Combat.DiceRollDefence.Blanks > 0;
        }

        private int GetDiceModificationAiPriority()
        {
            return 1000;
        }
    }
}
