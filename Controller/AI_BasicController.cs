using UnityEngine;
using System.Collections;

/// <summary>
/// Use this class to proved the instructions for the objects that will be controlled.
/// Place it on the route obejct to be contrlled and attach the scrpt (the script with all the bejaviour for the object) for the route
/// object on this.
/// The aiCOntroller will provide the ai logic that decidesd when things get called. This seperation means the script that will be
/// attached to these controllers can easily be controlled by a usercontroller or an ai controller.
/// </summary>
public class AI_BasicController : Controller
{
	public bool IsHostile;// {get; set;}
    public override void AlertVictimController(Transform attacker)
    {
        base.AlertVictimController(attacker);
        //turn aggresive
		IsHostile = true;
        Debug.Log("Turn aggressive");

    }
}
