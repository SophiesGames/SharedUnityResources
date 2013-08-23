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
    protected Being controlledObject;
    // Use this for initialization
    protected virtual void Start()
    {
        controlledObject = this.gameObject.GetComponent<Being>();
    }

    // Update is called once per frame
    void Update()
    {

        /*

         */

    }
}
