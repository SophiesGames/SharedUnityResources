using UnityEngine;
using System.Collections;
/// <summary>
/// Provides the Being class with a range of default functions to call on.
/// Should only provide the very broadest of calls. If it is too narrow, look at taking the being function that 
/// calls it and extend this in the child class to call 
/// 
/// ViewCharacter uses this to show being generic functions that it tries to call. For example it always assumes a move should call a move animation.
/// 
/// Implemented by ViewCharacter to reveal 
/// </summary>
public interface IBeingView 
{
    void MoveAnimation();
}
