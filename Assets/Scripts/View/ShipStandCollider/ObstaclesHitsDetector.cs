using Obstacles;
using Remote;
using Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesHitsDetector : MonoBehaviour {

    public bool checkCollisions = false;

    public List<GenericObstacle> OverlappedAsteroids = new();
    public List<Collider> OverlappedMines = new();
    public List<GenericRemote> RemotesMovedThrough = new();
    public List<GenericShip> ShipsMovedThrough = new();

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (checkCollisions)
        {
            if (collisionInfo.CompareTag("Obstacle"))
            {
                GenericObstacle obstacle = ObstaclesManager.GetChosenObstacle(collisionInfo.transform.name);
                if (!OverlappedAsteroids.Contains(obstacle))
                {
                    OverlappedAsteroids.Add(obstacle);
                }
            }
            else if(collisionInfo.CompareTag("Mine"))
            {
                if (!OverlappedMines.Contains(collisionInfo))
                {
                    OverlappedMines.Add(collisionInfo);
                }
            }
            else if (collisionInfo.name == "RemoteCollider")
            {
                if (!this.CompareTag(collisionInfo.tag))
                {
                    if (!RemotesMovedThrough.Contains(Roster.GetShipById(collisionInfo.tag) as GenericRemote))
                    {
                        RemotesMovedThrough.Add(Roster.GetShipById(collisionInfo.tag) as GenericRemote);
                    }
                }
            }
            else if (collisionInfo.name == "ObstaclesStayDetector")
            {
                if (!this.CompareTag(collisionInfo.tag))
                {
                    if (!ShipsMovedThrough.Contains(Roster.GetShipById(collisionInfo.tag)))
                    {
                        ShipsMovedThrough.Add(Roster.GetShipById(collisionInfo.tag));
                    }
                }
            }
        }
    }
}