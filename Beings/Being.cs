using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Anythign that exists with some kind of health
/// </summary>
public abstract class Being : MonoBehaviour
{
    private CharacterController characterController;

    public int movementSpeed = 100;
    public int rotationSpeed = 10;
    public int health = 10;
    public float attackSpeed = 2;
    public int attackDamage;
    public int meleeRange = 7000;

    private float roundStartTime = 0;

    private bool isAlive = true;
    private Being attacker;
    private Being defender;

    public Weapon equipedWeapon;
    private Armour equipedArmour;

    private int strength = 1;

    [HideInInspector]
    public Vector3 directionVector;

    public Transform attackTargetTransform;


    private List<Vector3> WayPointsList = new List<Vector3>();
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
        characterController = GetComponent<CharacterController>();
        FindAndInitialiseView();
        equipedWeapon = new Fist();
    }

    /// <summary>
    /// checks collision with an other moving body and recalls pathfinding. May only work with overlap making this redundant.
    /// </summary>
    /// <param name="hit"></param>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //check the player is actualy trying to go somewhere and hast jsut been bumped into.
        if (WayPointsList.Count > 0)
        {
            //tell the hit you ahve collided and wokr something out so both your recalcualtions dont put you in the
            //same path next time. May need to make this more complicated for intetional blocking.

            //Recalcualte paths, presumably with the same destination as before.
            CalculatePath(WayPointsList[WayPointsList.Count - 1]);
        }
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
        if (isAlive)
        {
            bool isIdleThisFrame = true;
            //Movement
            if (WayPointsList.Count > 0)
            {
                if (transform.position != WayPointsList[0])
                {
                    Move();
                }
                else//Removes this waypoint. Does not move this frame resulting in short pause but should work nicely with direction change.
                {
                    WayPointsList.RemoveAt(0);
                }
                isIdleThisFrame = false;
            }
            //Attack
            if (attackTargetTransform != null)
            {
                Attack();
                isIdleThisFrame = false;
            }

            //nothing being done so idle //check for aniamtion playing = null
            if (viewParent.CurrentAnimation == null && isIdleThisFrame)
            {
                viewParent.IdleAnimation();
            }
        }
    }

    /// <summary>
    /// Call this every time a path should be recalculated or a new one given.
    /// Allows for controllers to inject a destination point.
    /// </summary>
    /// <param name="finalDestinationPos"></param>
    public void CalculatePath(Vector3 targetPosition)
    {
        //clears any other movement orders
        WayPointsList.Clear();
        //In future it can take in a list of waypoints
        WayPointsList = Pathfinding.GetNextWaypoint(transform.position, targetPosition);
    }

    /// <summary>
    /// Called in Update. Rotates to face walking direction, checks for overshooting and then moves controller
    /// </summary>
    /// <param name="destinationPosition"></param>
    protected virtual void Move()
    {
        Vector3 wayPoint = WayPointsList[0];
        //Get direction
        directionVector = (wayPoint - transform.position);
        //Make direction vector 1, 0 or -1 
        if (directionVector.x < 0)
        {
            directionVector.x = -1;
        }
        else if (directionVector.x > 0) directionVector.x = 1;
        else directionVector.x = 0;
        if (directionVector.y < 0) directionVector.y = -1;
        else if (directionVector.y > 0) directionVector.y = 1;
        else directionVector.y = 0;
        //2d games so z is ignored
        directionVector.z = 0;

        //move in the right direction by speed.
        Vector3 velocity = movementSpeed * directionVector;


        //Check for overshooting X destination to the right(it has to be checked with delta time as that makes it much smaller)
        if (directionVector.x == 1)
        {
            bool newPosXGreaterThanWaypointPosx = (transform.position.x + (velocity.x * Time.deltaTime)) > wayPoint.x;
            bool currPosXLessThanWaypointX = transform.position.x < wayPoint.x;
            if (newPosXGreaterThanWaypointPosx && currPosXLessThanWaypointX)
            {
                velocity.x = 0;
                transform.position = new Vector3(wayPoint.x, transform.position.y, transform.position.z);
            }
        }
        //check to the elft
        else if (directionVector.x == -1)
        {
            bool newPosXLessThanWaypointPosx = (transform.position.x + (velocity.x * Time.deltaTime)) < wayPoint.x;
            bool currPosXGreaterThanWaypointX = transform.position.x > wayPoint.x;
            if (newPosXLessThanWaypointPosx && currPosXGreaterThanWaypointX)
            {
                velocity.x = 0;
                transform.position = new Vector3(wayPoint.x, transform.position.y, transform.position.z);
            }
        }
        //otherwise it doesnt matter

        //Check for overshooting Y destination to the top
        if (directionVector.y == 1)
        {
            bool newPosYGreaterThanWaypointPosy = (transform.position.y + (velocity.y * Time.deltaTime)) > wayPoint.y;
            bool currPosYLessThanWaypointY = transform.position.y < wayPoint.y;
            //Apply it
            if (newPosYGreaterThanWaypointPosy && currPosYLessThanWaypointY)
            {
                velocity.y = 0;
                transform.position = new Vector3(transform.position.x, wayPoint.y, transform.position.z);
            }
        }
        //Check for overshooting Y destination to the bottom
        else if (directionVector.y == -1)
        {
            bool newPosYLessThanWaypointPosy = (transform.position.y + (velocity.y * Time.deltaTime)) < wayPoint.y;
            bool currPosYGreaterThanWaypointY = transform.position.y > wayPoint.y;
            //Apply it
            if (currPosYGreaterThanWaypointY && newPosYLessThanWaypointPosy)
            {
                velocity.y = 0;
                transform.position = new Vector3(transform.position.x, wayPoint.y, transform.position.z);
            }
        }
        GA.API.Design.NewEvent("Moved", 1.2f, transform.position); 
        characterController.Move(velocity * Time.deltaTime);

        viewParent.MoveAnimation();
    }

    /// <summary>
    /// Only resets timer after an attack which only happens when in range
    /// </summary>
    protected virtual void Attack()
    {
        //Looks at current time so later on if its in range it can check the new current time. Big difference = time to attack!
        if (roundStartTime == 0)
        {
            roundStartTime = Time.time;
        }

        bool inRange = Vector3.SqrMagnitude(transform.position - attackTargetTransform.transform.position) < meleeRange;

        //if within range attack
        if (inRange)
        {
            //no longer need to get closer.
            WayPointsList.Clear();

            //If time bteween current time and time when round started is big then attack
            if ((Time.time - roundStartTime) >= attackSpeed)
            { 
                //reset timer
                roundStartTime = 0;
                AttackPreparation(this.transform, attackTargetTransform);
            }
            else
            {
                //play idle attack animation
            }
        }
        //else move closer
        else
        {
            CalculatePath(attackTargetTransform.transform.position);
        }
    }

    public virtual void ClearAllCommands()
    {
        //wayPoint = null; // need to stop the movement some other way
        roundStartTime = 0;
        attackTargetTransform = null;
    }

    private void AttackPreparation(Transform attackerTransform, Transform defenderTransform)
    {
        defender = defenderTransform.GetComponent<Being>();
        attacker = attackerTransform.GetComponent<Being>();

        //player controlled characters dont actualy have a controller on them as its on the main camera.
        //Has to do something like give the PC's a default ai script, which is overriden by mouse orders.
        //These orders expire at the end and ai takes over for meantime.
        Controller controller = (Controller)defenderTransform.GetComponent<Controller>();
        if (controller == null)
        {
            
        }
        controller.AttemptedAttack(defenderTransform);
        viewParent.AttackAnimation();
    }

    public void AttackImpact()
    {
        attackDamage = equipedWeapon.GetDamage() + strength;

        defender.health = defender.health - attacker.attackDamage;

        if (defender.health < 0)
        {
            defender.isAlive = false;
            defender.viewParent.DieAnimation();
            defender.enabled = false; //turn of script
            attackTargetTransform.transform.GetComponent<AIController>().enabled = false;
            attackTargetTransform.transform.GetComponent<CharacterController>().enabled = false;
            attackTargetTransform.transform.Find("View/ColliderFeedback").gameObject.SetActive(false);
            attacker.attackTargetTransform = null;
        }
        else
        {
            defender.viewParent.DamagedAnimation();
        }
    }

    public void AttackOver(int damageReceived)
    {
    }


    public void Die()
    {
        viewParent.DieAnimation();
    }
}
