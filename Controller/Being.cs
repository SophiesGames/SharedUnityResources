using UnityEngine;
using System.Collections;

/// <summary>
/// Anythign that exists with some kind of health
/// </summary>
public abstract class Being : MonoBehaviour
{
    private CharacterController controller;

    public int movementSpeed = 100;
    public int rotationSpeed = 10;
    private Vector3 wayPoint;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Updates Move if required
    /// </summary>
    private void Update()
    {
        //Movement
        if (transform.position != wayPoint)
        {
            Move();
        }
        else
        {
            //Check if there are more Waypoints and make this the new one if so
        }
    }

    /// <summary>
    /// Call this every time a path should be recalculated or a new one given.
    /// Allows for controllers to inject a destination point.
    /// </summary>
    /// <param name="finalDestinationPos"></param>
    public void CalculatePath(Vector3 TargetPosition)
    {
        //In future it can take in a list of waypoints
        wayPoint = Pathfinding.GetNextWaypoint(transform.position, TargetPosition);
    }

    /// <summary>
    /// Called in Update. Rotates to face walking direction, checks for overshooting and then moves controller
    /// </summary>
    /// <param name="destinationPosition"></param>
    protected virtual void Move()
    {
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
