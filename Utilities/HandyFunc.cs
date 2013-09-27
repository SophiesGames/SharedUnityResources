using UnityEngine;
using System.Collections;

public static class HandyFunc 
{
    public static bool ApproximatelyEqual(Vector3 a, Vector3 b)
    {
        float d = Vector3.SqrMagnitude(a - b);
        return Vector3.SqrMagnitude(a - b) < 0.0001;
    }
}
