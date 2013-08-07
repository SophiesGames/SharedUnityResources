using UnityEngine;
using System.Collections;

/// <summary>
/// Anythign that exists withsome kind of health
/// </summary>
public abstract class Being : MonoBehaviour
{
    private CharacterController controller;

    public int movementSpeed = 100;
    public int rotationSpeed = 10;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Allows for controllers to inject a destination point
    /// </summary>
    /// <param name="finalDestinationPos"></param>
    public void SetDestinationPoint(Vector3 finalDestinationPos)
    {
        Move(finalDestinationPos);
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
        //Creates wayponts based on pathfidning (currently returs the same as 
        Vector3 wayPoint = Pathfinding.GetNextWaypoint(transform.position, finalDestinationPos);

        //Get direction
        Vector3 direction = (wayPoint - transform.position);
        //Make direction vector 1, 0 or -1 
        if (direction.x < 0) direction.x = -1;
        if (direction.x > 0) direction.x = 1;
        if (direction.y < 0) direction.y = -1;
        if (direction.y > 0) direction.y = 1;
        //2d games so z is ignored
        direction.z = 0;

        //Rotate to face waypoint
        Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.back);
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);

        //move in the right direction by speed.
        Vector3 velocity = movementSpeed * direction;

        //Check for overshooting X destination
        bool newPosXGreaterThanWaypointPosx = (transform.position.x + velocity.x) > wayPoint.x;
        bool currPosXLessThanWaypointX = transform.position.x < wayPoint.x;
        if (newPosXGreaterThanWaypointPosx && currPosXLessThanWaypointX) velocity.x = wayPoint.x - transform.position.x;
        //Check for overshooting Y destination
        bool newPosYGreaterThanWaypointPosy = (transform.position.y + velocity.y) > wayPoint.y;
        bool currPosYLessThanWaypointY = transform.position.y < wayPoint.y;
        //Apply it
        if (newPosYGreaterThanWaypointPosy && currPosYLessThanWaypointY) velocity.y = wayPoint.y - transform.position.y;

        //apply movement to controller
        controller.Move(velocity * Time.deltaTime);
    }
}
