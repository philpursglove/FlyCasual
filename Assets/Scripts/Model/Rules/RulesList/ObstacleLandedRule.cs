using Obstacles;
using Ship;

namespace RulesList
{
    public class AsteroidLandedRule
    {
        static bool RuleIsInitialized = false;

        public AsteroidLandedRule()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            if (!RuleIsInitialized)
            {
                GenericShip.OnPositionFinishGlobal += CheckLandedOnObstacle;
                RuleIsInitialized = true;
            }
        }

        public void CheckLandedOnObstacle(GenericShip ship)
        {
            if (ship.IsLandedOnObstacle)
            {
                foreach (GenericObstacle obstacle in ship.ObstaclesLanded)
                {
                    if (ship.IgnoreObstaclesList.Contains(obstacle)) continue;

                    obstacle.OnLanded(ship);
                }
            }
        }
    }
}
