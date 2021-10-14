// C NELSON 2021
using UnityEngine;

// Create a new scriptable object from Assets > Create
// Assign time of day

[System.Serializable]
[CreateAssetMenu(fileName = "TimeOfDay", menuName = "Scriptable Objects/TimeOfDay", order = 1)]
public class TimeOfDay_ScriptableObject : ScriptableObject
{
    public float time;
    public string phase;
}
