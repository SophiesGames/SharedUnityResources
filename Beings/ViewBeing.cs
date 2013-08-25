using UnityEngine;
using System.Collections;

/// <summary>
/// If generic character view code is needed it can be put here. 
/// Try to get everything in here
/// </summary>
public class ViewBeing : View, IBeingView
{
    private Being being;

    /// <summary>
    /// Can be overridden to take new child type for greater access
    /// </summary>
    /// <param name="being"></param>
    public virtual void InitialiseView(Being injectedBeing)
    {
        being = injectedBeing;
    }

    public virtual void MoveAnimation()
    {
        Vector3 x = being.direction;
        Debug.Log(x);

        //decide direction of aniamtion to play
        //TODO: Decide what kind of movement to player here eg. run, walk, crawl.

    }


    //void IBeingView.InitialiseView(Being injectedBeing)
    //{
    //    being = injectedBeing;
    //}
}



