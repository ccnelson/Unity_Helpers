// C NELSON 2021
using UnityEngine;

// Create a new scriptable object from Assets > Create
// Assign gradient colours

[System.Serializable]
[CreateAssetMenu(fileName = "SkyGradient", menuName = "Scriptable Objects/SkyGradient", order = 1)]
public class SkyGradient_ScriptableObject : ScriptableObject
{
    public Gradient AmbientColour;
    public Gradient DirectionalColour;
    public Gradient FogColour;
}
