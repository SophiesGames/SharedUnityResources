using UnityEngine;
using System.Collections;
/// <summary>
/// Mouse and keyboard controller take the selected person(s) and can change whats selected. They lsiten for ui changes
/// and tell the selectedselectedCharacter person(s) controller about that. Their controller then performs the right descicion.
/// It mgiht be the mid controller is not needed. See UserInputControllerQI for potential use for it.
/// </summary>
public class MouseController : UserInputController
{
    protected Vector3 mouseWorldPosition;

    protected void Start()
    {
        GameObject ObjectPool = GameObject.Find("ObjectPool");
        destinationLocator = ObjectPool.transform.Find("DestinationWayPoint");
    }

	//need a mouse over that changes the icon based on the type of enemy it is over
	//if the icon is not on default this will not happend. default for going over pc is talking icon.
	
    public void Update()
    {
        //Left Click = selections
        if (Input.GetButton("LeftClick"))
        {
            //create the ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //give somethign to store the hit
            RaycastHit hit;

            //checks for a hit
            if (Physics.Raycast(ray, out hit, 100))
            {
                //the hit is a character that doesnt ahve a controller
                if (hit.transform.GetComponent(typeof(Being)) && !hit.transform.GetComponent(typeof(AI_NPC_Controller)))
                {
                    selectedCharacter = (Being)hit.transform.GetComponent(typeof(Being));
                }

                //the hit is an ai character
                if (hit.transform.GetComponent(typeof(AI_BasicController)))
                {
                    //show picture of enemy?
                }

                //The hit is a ui component
                //if (hit.transform.GetComponent(typeof(UIcomponent)))
                //{
                //      //get the type and make an instance
                //    UIcomponent uIcomponent = hit.transform.GetComponent(typeof(UIcomponent);
                //      //get the name of the button hit.
                //    string command = uIcomponent.GetButtonComamnd;
                //      //tell the selected objects that this button was hit
                //      selectedObject.UICall(command);
                //}
            }
            //nothing hit so deslection
            else
            {
                selectedCharacter = null;
            }
        }

        //Right CLick - commands
        if (Input.GetButton("RightClick"))
        {
            //check there is something selected to receive commands
            if (selectedCharacter != null)
            {
                selectedCharacter.ClearAllCommands();

                //create the ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //give somethign to store the hit
                RaycastHit hit;

                //checks for hit
                if (Physics.Raycast(ray, out hit, 100))
                {
                    //the hit is an ai character - doesnt actualy amtter. suicide possible
                    //if (hit.transform.GetComponent(typeof(AI_BasicController)))
                    //{}
                        selectedCharacter.AttackTargetTransform = hit.transform;
                    
                }
                //Nothing was hit so its the floor
                else
                {
                    mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mouseWorldPosition.z = 0;
                    selectedCharacter.CalculatePath(mouseWorldPosition);

                    destinationLocator.transform.position = mouseWorldPosition;
                }
            }
        }

        if (Input.GetButton("Space"))
        {
            if (selectedCharacter) selectedCharacter.Die();

        }

        if (Input.GetButton("9"))
        {
            selectedCharacter.equipedWeapon = new Dagger();
        }
    }
}