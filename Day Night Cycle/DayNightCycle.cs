// C NELSON 2021
using UnityEngine;

// Attach to a scenemanager object (empty will do)
// Assign a sun (directional light), and SkyGradient (scriptable object)

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private SkyGradient_ScriptableObject presetGradient;
    [SerializeField, Range(0, 24)] public float timeOfDay;
    [SerializeField, Range(1, 100)] private float speed = 1f;


    private void LateUpdate()
    {
        timeOfDay += Time.deltaTime / speed;
        timeOfDay %= 24;
        UpdateLights(timeOfDay / 24f);
    }

    private void UpdateLights(float timePercent)
    {
        RenderSettings.ambientLight = presetGradient.AmbientColour.Evaluate(timePercent);
        sun.color = presetGradient.DirectionalColour.Evaluate(timePercent);
        RenderSettings.fogColor = presetGradient.FogColour.Evaluate(timePercent);

        sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
    }
}
