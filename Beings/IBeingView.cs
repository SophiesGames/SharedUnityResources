using UnityEngine;
using System.Collections;
/// <summary>
/// If this is only used once it wont be needed and as everything is going ot inherit from the ViewBeing, everythig will already
/// have a implementation of each function.
/// </summary>
public interface IBeingView 
{
    void MoveAnimation();

    void InitialiseView(Being injectedBeing);
}
