using UnityEngine;
using System.Collections;

/// <summary>
/// Anythign that exists withsome kind of health
/// </summary>
public abstract class Being : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 currentPosition;
    public int movementSpeed = 1;

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
        Vector3 wayPoint = new Vector3();
        wayPoint = Pathfinding.GetNextWaypoint(currentPosition, finalDestinationPos);

        //This can be replaced by getting the direction of travel and moving the movementSpeed amount of units in that direction.
        //This will look like: velocity = directionVector * movementSpeed
        //if velocity.x + current pos.x is greater that intended destination and currentpos.x is less, assume you will overshoot 
        //and change velocity to make it small enough to hit
        Vector3 distanceToTravel = wayPoint - currentPosition;
        velocity = new Vector3(distanceToTravel.x / movementSpeed, distanceToTravel.y / movementSpeed, distanceToTravel.z / movementSpeed);
        //Moves the appropriate speed towards that waypoint
        controller.Move(velocity * Time.deltaTime);
    }

    public void SetFinalDestiantion(Vector3 finalDestinationPos)
    {
        Move(finalDestinationPos);
    }
}
