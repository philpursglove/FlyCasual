using Obstacles;
using Remote;
using Ship;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesStayDetector : MonoBehaviour
{

    public bool checkCollisions = false;

    public bool OverlapsShip = false;

    public List<GenericShip> OverlappedShips = new();
    public List<GenericRemote> OverlappedRemotes = new();
    public List<GenericObstacle> OverlappedAsteroids = new();
    public List<Collider> OverlappedMines = new();

    public bool OffTheBoard = false;

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (checkCollisions)
        {
            GameManagerScript Game = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
            if (collisionInfo.CompareTag("Obstacle"))
            {
                GenericObstacle obstacle = ObstaclesManager.GetChosenObstacle(collisionInfo.transform.name);
                if (!OverlappedAsteroids.Contains(obstacle))
                {
                    OverlappedAsteroids.Add(obstacle);
                }
            }
            else if (collisionInfo.CompareTag("Mine"))
            {
                if (!OverlappedMines.Contains(collisionInfo))
                {
                    OverlappedMines.Add(collisionInfo);
                }
            }
            else if (collisionInfo.name == "OffTheBoard")
            {
                OffTheBoard = true;
            }
            else if (collisionInfo.name == "ObstaclesStayDetector")
            {
                if (!this.CompareTag(collisionInfo.tag))
                {
                    Game.Movement.CollidedWith = collisionInfo;
                    OverlapsShip = true;

                    if (!OverlappedShips.Contains(Roster.GetShipById(collisionInfo.tag)))
                    {
                        OverlappedShips.Add(Roster.GetShipById(collisionInfo.tag));
                    }
                }
            }
            else if (collisionInfo.name == "RemoteCollider"
                && !this.CompareTag(collisionInfo.tag)
                && !OverlappedRemotes.Contains(Roster.GetShipById(collisionInfo.tag) as GenericRemote))
            {
                OverlappedRemotes.Add(Roster.GetShipById(collisionInfo.tag) as GenericRemote);

            }
        }
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        if (checkCollisions
            && collisionInfo.name == "ObstaclesStayDetector"
            && !this.CompareTag(collisionInfo.tag)
            && OverlappedShips.Contains(Roster.GetShipById(collisionInfo.tag)))
        {
            OverlappedShips.Remove(Roster.GetShipById(collisionInfo.tag));
        }
    }
}