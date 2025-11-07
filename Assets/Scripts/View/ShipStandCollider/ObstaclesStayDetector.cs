using Obstacles;
using Remote;
using Ship;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesStayDetector: MonoBehaviour {

    public bool checkCollisions = false;

    public bool OverlapsShip = false;

    public List<GenericShip> OverlappedShips = new List<GenericShip>();
    public List<GenericRemote> OverlappedRemotes = new List<GenericRemote>();
    public List<GenericObstacle> OverlappedAsteroids = new List<GenericObstacle>();
    public List<Collider> OverlappedMines = new List<Collider>();

    public bool OffTheBoard = false;

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (checkCollisions)
        {
            GameManagerScript Game = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
            if (collisionInfo.tag == "Obstacle")
            {
                GenericObstacle obstacle = ObstaclesManager.GetChosenObstacle(collisionInfo.transform.name);
                if (!OverlappedAsteroids.Contains(obstacle))
                {
                    OverlappedAsteroids.Add(obstacle);
                }
            }
            else if (collisionInfo.tag == "Mine")
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
                if (collisionInfo.tag != this.tag)
                {
                    Game.Movement.CollidedWith = collisionInfo;
                    OverlapsShip = true;
                    if (!OverlappedShips.Contains(Roster.GetShipById(collisionInfo.tag)))
                    {
                        OverlappedShips.Add(Roster.GetShipById(collisionInfo.tag));
                    }
                }
            }
            else if (collisionInfo.name == "RemoteCollider")
            {
                if (collisionInfo.tag != this.tag)
                {
                    if (!OverlappedRemotes.Contains(Roster.GetShipById(collisionInfo.tag) as GenericRemote))
                    {
                        OverlappedRemotes.Add(Roster.GetShipById(collisionInfo.tag) as GenericRemote);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        if (checkCollisions)
        {
            if (collisionInfo.name == "ObstaclesStayDetector")
            {
                if (collisionInfo.tag != this.tag)
                {
                    if (OverlappedShips.Contains(Roster.GetShipById(collisionInfo.tag)))
                    {
                        OverlappedShips.Remove(Roster.GetShipById(collisionInfo.tag));
                    }
                }
            }
        }
    }

}
