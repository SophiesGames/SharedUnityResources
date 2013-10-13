using UnityEngine;
using System.Collections;

/// <summary>
/// As this genric level probably can  be used to do anything, i should probably just be an interface. 
/// Its only purpose is to allow their high level being to give aniamtion calls to something becasue i need to add code to ininitalise
///view. 
/// </summary>
public class ViewBeing : View, IBeingView
{
    protected Being being;
    protected AnimateSprite animateSprite;

    public string CurrentAnimation
    {
        get
        {
            return animateSprite.CurrentlyPlayingFrameSetName;
        }
    }
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
    }

    public virtual void IdleAnimation()
    {
    }

    public virtual void AttackAnimation()
    {
    }

    public virtual void DamagedAnimation()
    {
    }

    public virtual void DieAnimation()
    {
    }
}



