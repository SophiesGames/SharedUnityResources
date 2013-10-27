using UnityEngine;
using System.Collections;

/// <summary>
/// Inherit from here for all the base controller needs.
/// If click is on being, send that being info along with the fact it was clicked on to the controlledObject
/// The contorlled oject will then be abe to acces the clicked on object to resovle what should happend ina fight 
/// with who takes damge, resistance etc.
/// </summary>
public abstract class Controller : MonoBehaviour
{



    // Update is called once per frame
    void Update()
    {

        /*

         */

    }

    protected virtual void CancelCurrentCommand()
    {
    }
    /// <summary>
    /// default implementation
    /// </summary>
    public virtual void AttemptedAttack(Transform attacker)
    {
        this.transform.GetComponent<Being>().attackTargetTransform = attacker;
        //Aggressive mode
        /*
         * Load rules based on how it fights eg: a bear attacks who ever first htis it then whoever is closest
         * A amastermind might try to first split the group up then pick of the weakest first.
         * 
         * How intelleingt you fight and how you even respond to things is based on your behviours.
         * These can be children of this class.
         * 
         */

    }
}
