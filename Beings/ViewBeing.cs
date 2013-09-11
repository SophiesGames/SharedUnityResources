using UnityEngine;
using System.Collections;

/// <summary>
/// As this genric level probably can  be used to do anything, i should probably just be an interface. 
/// Its only purpose is to allow thei high level being to give aniamtion calls to something.
/// </summary>
public class ViewBeing : View, IBeingView
{
    protected Being being;

    /// <summary>
    /// Can be overridden to take new child type for greater access
    /// </summary>
    /// <param name="being"></param>
    public void InitialiseView(Being injectedBeing)
    {
        being = injectedBeing;
    }

    public virtual void MoveAnimation()
    {
        Vector3 x = being.directionVector;
        Debug.Log(x);

        //decide direction of aniamtion to play
        //TODO: Decide what kind of movement to player here eg. run, walk, crawl.

    }
}



