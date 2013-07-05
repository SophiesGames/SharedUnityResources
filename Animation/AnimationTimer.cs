using UnityEngine;
using System.Collections;

public class AnimationTimer 
{
    private float timeAtStart = 0.0f;
    private float _animationTimeElapsed = 0.0f;
    public float animationTimeElapsed
    {
        get
        {
            return _animationTimeElapsed;
        }
        set
        {
            _animationTimeElapsed = value;
        }
    }

    public void ResetTimer(AnimationFrameSet animationFrameSet)
    {
        timeAtStart = Time.time;
        animationFrameSet.hasStarted = false;
        animationTimeElapsed = 0.0f;
    }

    public void UpdateElapsedTime(AnimationFrameSet animationFrameSet)
    {
        float lastTimeElapsed = animationTimeElapsed;
        if (animationFrameSet.hasStarted == true)                                                               //if the framset has started playing 
        {
            animationTimeElapsed = Time.time - timeAtStart;                                                     //update timer 
            if (lastTimeElapsed + 1.0f < animationTimeElapsed)                                                  //check that more than a second has not passed since last play
            {
                ResetTimer(animationFrameSet);                                                                                   //if it has then reset the timer
            }
        }
        else                                                                                                    //else set timer to current time and make sure time elapsed = 0  
        {
            ResetTimer(animationFrameSet);
            animationFrameSet.hasStarted = true;
        }
    }
}
