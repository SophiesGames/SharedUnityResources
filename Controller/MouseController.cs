using UnityEngine;
using System.Collections;
/// <summary>
/// Mouse and keyboard controller take the selected person(s) and can change whats selected. They lsiten for ui changes
/// and tell the selectedselectedCharacter person(s) controller about that. Their controller then performs the right descicion.
/// It mgiht be the mid controller is not needed. See UserInputControllerQI for potential use for it.
/// </summary>
public class MouseController : UserInputController
{

	public GameObject wayPointManager;
    protected Vector3 mouseWorldPosition;

    protected void Start()
    {
        //GameObject ObjectPool = GameObject.Find("ObjectPool");

		destinationLocator = wayPointManager.transform.Find("WayPoint1");
    }

	//need a mouse over that changes the icon based on the type of enemy it is over
	//if the icon is not on default this will not happend. default for going over pc is talking icon.
	
    public void Update()
    {
        //Left Click = selections
        if (Input.GetButton("LeftClick"))
        {
			//create the ray
			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//if (Physics.Raycast(ray, out hit, 100))
           
			Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
			                               Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			//LayerMask layerMask = LayerMask.NameToLayer("Player");

			//give somethign to store the hit
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);//layerMask

            //checks for a hit on collider as hit as value type so cant be checked for null
			if (hit.collider != null)
            {
                //the hit is a character that doesnt ahve a controller
                if (hit.transform.GetComponent(typeof(Being)) && !hit.transform.GetComponent(typeof(AI_BasicController)))
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
                //      //get the type and make an insta nce
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

				Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
				                               Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
				LayerMask layerMask = LayerMask.NameToLayer("Player");
				//give somethign to store the hit
				
				RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, layerMask);

                //checks for hit
                if (hit.collider != null)
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
					//sets destination marker
                    destinationLocator.transform.position = mouseWorldPosition;
					//tells slected characters where to move
					selectedCharacter.CalculatePath(mouseWorldPosition);
                }
            }
        }
    }
}