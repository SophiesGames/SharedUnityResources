using UnityEngine;
using System.Collections;

/// <summary>
/// Anythign that exists withsome kind of health
/// </summary>
public abstract class Being : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 currentPosition;
    public int movementSpeed = 100;

    //properties
    //public Vector3 finalDestinationPos { get; set; }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentPosition = this.gameObject.transform.position;
    }

    /// <summary>
    /// uses destinationPosition to set its waypoint
    /// Will update to find a new path every fram at first.
    /// This can be optimised alter by giving it a number of frames to run before checking if the path is still the best.
    /// The mfore frequent the update the better however.
    /// </summary>
    /// <param name="destinationPosition"></param>
    public void Move(Vector3 finalDestinationPos)
    {
        //Later on the final destiantion will be differnt from firt port of call
        Vector3 wayPoint = Pathfinding.GetNextWaypoint(currentPosition, finalDestinationPos);

        Vector3 direction = (currentPosition - wayPoint).normalized;
        if (direction.x > 0) direction.x = 1; else direction.x = -1;
        if (direction.y > 0) direction.y = 1; else direction.y = -1;
        if (direction.z > 0) direction.z = 1; else direction.z = -1;
        //move in the right direction by speed.
        Vector3 velocity = movementSpeed * direction;


        ////Check for overshooting X
        //bool newPosXGreaterThanWaypointPosx = (currentPosition.x + velocity.x) > wayPoint.x;
        //bool currPosXLessThanWaypointX = currentPosition.x < wayPoint.x;
        //if (newPosXGreaterThanWaypointPosx && currPosXLessThanWaypointX)velocity.x = wayPoint.x - currentPosition.x;
        ////Check for overshooting Y
        //bool newPosYGreaterThanWaypointPosy = (currentPosition.y + velocity.y) > wayPoint.y;
        //bool currPosYLessThanWaypointY = currentPosition.y < wayPoint.y;
        //if (newPosYGreaterThanWaypointPosy && currPosYLessThanWaypointY) velocity.y = wayPoint.y - currentPosition.y;

        controller.Move(velocity * Time.deltaTime);
    }

    public void SetFinalDestiantion(Vector3 finalDestinationPos)
    {
        Move(finalDestinationPos);
    }
}
