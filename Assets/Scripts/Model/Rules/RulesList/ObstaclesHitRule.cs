using Obstacles;
using Ship;
using System.Collections.Generic;

namespace RulesList
{
    public class ObstaclesHitRule
    {
        static bool RuleIsInitialized = false;

        public ObstaclesHitRule()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            if (!RuleIsInitialized)
            {
                GenericShip.OnPositionFinishGlobal += CheckHits;
                RuleIsInitialized = true;
            }
        }

        public void CheckHits(GenericShip ship)
        {
            if (ship.IsHitObstacles)
            {
                List<GenericObstacle> ProcessedObstacles = new();

                foreach (GenericObstacle obstacle in ship.ObstaclesHit)
                {
                    if (ship.IgnoreObstaclesList.Contains(obstacle)) continue;

                    if (ProcessedObstacles.Contains(obstacle)) continue;

                    // If ship started on an obstacle and is no longer on the obstacle after moving, do not process
                    if (ship.PreviousObstaclesLanded.Contains(obstacle) && !ship.ObstaclesLanded.Contains(obstacle)) continue;

                    Triggers.RegisterTrigger(new Trigger()
                    {
                        Name = "Apply effect of hit obstacle",
                        TriggerOwner = ship.Owner.PlayerNo,
                        TriggerType = TriggerTypes.OnPositionFinish,
                        EventHandler = delegate { obstacle.OnHit(ship); }
                    });

                    ProcessedObstacles.Add(obstacle);
                }
            }
        }
    }
}