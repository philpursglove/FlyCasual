using Obstacles;
using Remote;
using Ship;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesStayDetectorForced: MonoBehaviour {

    public bool checkCollisionsNow = false;

    public bool OverlapsShipNow
    {
        get { return OverlappedShipsNow.Count > 0; }
    }

    public bool OverlapsAsteroidNow
    {
        get { return OverlappedAsteroidsNow.Count > 0; }
    }

    public List<GenericShip> OverlappedShipsNow = new ();
    public List<GenericRemote> OverlappedRemotesNow = new ();
    public bool OffTheBoardNow = false;
    public List<Collider> OverlappedMinesNow = new();
    public List<GenericObstacle> OverlappedAsteroidsNow = new ();
    public bool OverlapsCurrentShipNow { get; private set; }

    private GenericShip theShip; 
    public GenericShip TheShip {
        get {
            return theShip ?? Selection.ThisShip;
        }
        set {
            theShip = value;
        }
    }

    public void ReCheckCollisionsStart()
    {
        OverlappedShipsNow = new ();
        OverlappedRemotesNow = new ();
        OffTheBoardNow = false;
        OverlappedMinesNow = new ();
        OverlappedAsteroidsNow = new ();
        OverlapsCurrentShipNow = false;

        checkCollisionsNow = true;
    }

    public void ReCheckCollisionsFinish()
    {
        checkCollisionsNow = false;
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (checkCollisionsNow)
        {
            if (collisionInfo.CompareTag("Obstacle"))
            {
                GenericObstacle obstacle = ObstaclesManager.GetChosenObstacle(collisionInfo.transform.name);
                if (!OverlappedAsteroidsNow.Contains(obstacle)) OverlappedAsteroidsNow.Add(obstacle);
            }
            else if (collisionInfo.CompareTag("Mine"))
            {
                if (!OverlappedMinesNow.Contains(collisionInfo)) OverlappedMinesNow.Add(collisionInfo);
            }
            else if (collisionInfo.name.StartsWith("OffTheBoard"))
            {
                OffTheBoardNow = true;
            }
            else if (collisionInfo.name == "ObstaclesStayDetector")
            {
                if (!collisionInfo.CompareTag(TheShip.GetTag()))
                {
                    GenericShip ship = Roster.GetShipById(collisionInfo.tag);
                    if (ship != null && !OverlappedShipsNow.Contains(ship)) OverlappedShipsNow.Add(ship);
                }
                else if (collisionInfo.CompareTag(TheShip.GetTag()))
                {
                    OverlapsCurrentShipNow = true;
                }
            }
            else if (collisionInfo.name == "RemoteCollider")
            {
                if (!this.CompareTag(collisionInfo.tag))
                {
                    if (!OverlappedRemotesNow.Contains(Roster.GetShipById(collisionInfo.tag) as GenericRemote))
                    {
                        OverlappedRemotesNow.Add(Roster.GetShipById(collisionInfo.tag) as GenericRemote);
                    }
                }
            }
        }
    }
}
