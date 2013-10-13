using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnimationFrameSet
{
    //take in an xml file and read the data to automatically fill a lot of these variables.
    public string frameSetName = "";
    public int startingFrame = 1;
    public int endingFrame = 7;

    public int framesPerSecond = 15;
    public bool endOnFrameOne = false;
    public bool isLooping = true;
    public bool isPlaying = false;
    public bool bounceAnimation = false;

    public bool reverseAnimation = false;

    public string textureName;

    public bool applyFrameSet = false;

    [HideInInspector]
    public bool hasStarted = false;

    private int _framesInSet;
    public int framesInSet
    {
        get
        {
            _framesInSet = Mathf.Abs(endingFrame - startingFrame) + 1;
            return _framesInSet;
        }
        set
        {
        }
    }
}
