using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PathfindingLegacy
{

    public static List<Vector3> GetNextWaypoint(Vector3 currentPosition, Vector3 finalDestinationPos)
    {
        List<Vector3> wayPointPathList = new List<Vector3>();
        ///Calculcate list of waypoints here
        wayPointPathList.Add(finalDestinationPos);

        return wayPointPathList;
    }
}
