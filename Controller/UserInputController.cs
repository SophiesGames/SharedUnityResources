using UnityEngine;
using System.Collections;

public class UserInputController : Controller
{
    public Being selectedCharacter;

    [HideInInspector]
    public Transform destinationLocator;
}
