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
    public int health;
    public int attackSpeed;

    [HideInInspector]
    public Vector3 directionVector;

    private float colliderHeight;

    private Vector3 wayPoint;
    protected ViewBeing viewParent;

    public enum Direction
    {
        North, NorthWest, West, SouthWest, South, SouthEast, East, NorthEast
    }

    public Direction GetDirection
    {
        get
        {
            //default is south
            Direction curentDirection = Direction.South;

            if (directionVector.x > 0)
            {
                if (directionVector.y > 0)
                {
                    curentDirection = Direction.NorthEast;
                }
                else if (directionVector.y < 0)
                {
                    curentDirection = Direction.SouthEast;
                }
                else
                {
                    curentDirection = Direction.East;
                }
            }
            else if (directionVector.x < 0)
            {

                if (directionVector.y > 0)
                {
                    curentDirection = Direction.NorthWest;
                }
                else if (directionVector.y < 0)
                {
                    curentDirection = Direction.SouthWest;
                }
                else
                {
                    curentDirection = Direction.West;
                }
            }
            else
            {
                if (directionVector.y > 0)
                {
                    curentDirection = Direction.North;
                }
                else if (directionVector.y < 0)
                {
                    curentDirection = Direction.South;
                }
            }
            return curentDirection;
        }
    }
     
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        wayPoint = transform.position;
        FindAndInitialiseView();
        colliderHeight = controller.transform.lossyScale.y;// transform.Find("HitBox").transform.lossyScale.y;
    }

    protected virtual void FindAndInitialiseView()
    {
        viewParent = this.transform.Find("View").GetComponent<ViewBeing>();//(IBeingView)this.transform.Find("View").GetComponent(typeof(IBeingView));
        //do a check if this returns null, tell it to implement IBeingViewInterface
        viewParent.InitialiseView(this);
    }

    /// <summary>
    /// Updates Move if required
    /// </summary>
    private void Update()
    {
        //Movement
        //!HandyFunc.ApproximatelyEqual(
        if (transform.position != wayPoint)
        {
            Move();
        }
        else if (false)//Check if there are more Waypoints and make this is the new one if so
        {

        }
        else //nothing being done so idle
        {
            viewParent.IdleAnimation();
            //it should be idling in attack mode inbet ween hits. THen fix it to ahve attack stance
        }
    }

    /// <summary>
    /// Call this every time a path should be recalculated or a new one given.
    /// Allows for controllers to inject a destination point.
    /// </summary>
    /// <param name="finalDestinationPos"></param>
    public void CalculatePath(Vector3 targetPosition)
    {
        //In future it can take in a list of waypoints
        wayPoint = Pathfinding.GetNextWaypoint(transform.position, targetPosition);
    }

    public void AttackTarget()
    {

    }

    /// <summary>
    /// Called in Update. Rotates to face walking direction, checks for overshooting and then moves controller
    /// </summary>
    /// <param name="destinationPosition"></param>
    protected virtual void Move()
    {
        //Get direction
        directionVector = (wayPoint - transform.position);
        //Make direction vector 1, 0 or -1 
        if (directionVector.x < 0 ) directionVector.x = -1;
        else if (directionVector.x > 0 ) directionVector.x = 1;
        else directionVector.x = 0;
        if (directionVector.y < 0 ) directionVector.y = -1;
        else if (directionVector.y > 0 ) directionVector.y = 1;
        else directionVector.y = 0;
        //2d games so z is ignored
        directionVector.z = 0;

        //move in the right direction by speed.
        Vector3 velocity = movementSpeed * directionVector;

        //Check for overshooting X destination (it has to be checked with delta time as that makes it much smaller)
        bool newPosXGreaterThanWaypointPosx = (transform.position.x + (velocity.x * Time.deltaTime)) > wayPoint.x;
        bool currPosXLessThanWaypointX = transform.position.x < wayPoint.x;
        if (newPosXGreaterThanWaypointPosx && currPosXLessThanWaypointX)
        {
            velocity.x = 0;// wayPoint.y - transform.position.y;
            transform.position = new Vector3(wayPoint.x, transform.position.y, transform.position.z);
        }

        //Check for overshooting Y destination
        bool newPosYGreaterThanWaypointPosy = (transform.position.y + (velocity.y * Time.deltaTime)) > wayPoint.y;
        bool currPosYLessThanWaypointY = transform.position.y < wayPoint.y;
        //Apply it
        if (newPosYGreaterThanWaypointPosy && currPosYLessThanWaypointY)
        {
            velocity.y = 0;// wayPoint.y - transform.position.y;
            //transform.position = new Vector3(transform.position.x, wayPoint.y, transform.position.z);
        }

        //apply movement to controller - use move to account for gravity effects 
        controller.Move(velocity * Time.deltaTime);

        viewParent.MoveAnimation();
    }
}
