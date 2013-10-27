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
    public int directionChangeInterval = 300;

    private int counter = 0;

    protected Being controlledObject;
    // Use this for initialization
    protected virtual void Start()
    {
        controlledObject = this.gameObject.GetComponent<Being>();
    }

    protected virtual void Update()
    {
        if (counter > directionChangeInterval)
        {
            counter = 0;
        }
        if (counter == 0)
        {
            float x = Random.Range(0f, 1000f);
            float y = Random.Range(0f, 1000f);
            Vector3 randomWalk = new Vector3(x, y, 0);
            controlledObject.CalculatePath(randomWalk);
        }

        counter++;
    }

    public override void AttemptedAttack(Transform attacker)
    {
        base.AttemptedAttack(attacker);
        //turn aggresive
        Debug.Log("Turn aggressive");



    }
}
