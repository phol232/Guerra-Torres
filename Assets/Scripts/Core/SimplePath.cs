using UnityEngine;
using System.Collections.Generic;


public class SimplePath : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();

    public Vector3 GetWaypoint(int index)
    {
        return _waypoints[index].position;
    }

    public int WaypointCount => _waypoints.Count;
}