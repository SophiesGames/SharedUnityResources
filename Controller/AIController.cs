using UnityEngine;
using System.Collections;

/// <summary>
/// Use this class to proved the instructions for the objects that will be controlled.
/// Place it on the route obejct to be contrlled and attach the scrpt (the script with all the bejaviour for the object) for the route
/// object on this.
/// The aiCOntroller will provide the ai logic that decidesd when things get called. This seperation means the script that will be
/// attached to these controllers can easily be controlled by a usercontroller or an ai controller.
/// </summary>
public class AIController : Controller
{
	// Update is called once per frame
	void Update () {
	
	}
}
