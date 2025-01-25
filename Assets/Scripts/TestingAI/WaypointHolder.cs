using System.Collections.Generic;
using UnityEngine;

public class WaypointHolder : MonoBehaviour
{
    public Transform[] Waypoints;
    private void OnValidate()
    {
        RefreshWaypoints();
    }
    public void RefreshWaypoints()
    {
        List<Transform> wpList = new List<Transform>();
        foreach (Transform child in transform)
        {
            wpList.Add(child);
        }
        Waypoints = wpList.ToArray();
    }


}
