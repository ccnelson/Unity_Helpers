// C NELSON 2021
using UnityEngine;

////////////////////////////////////////////////////////////////////////////////////////////////
// Attach this script to an empty scenemanager object.
// Assign a sun and moon (directional lights), SkyGradient and TimeOfDay (scriptable objects) 
/////// SETUP ////////////////////
// In the centre of the scene have a bright directional yellow light, the sun.
// As a child of the sun, in the same position but pointing in the opposite direction,
// a dull blue light as the moon.
//Aalso a child of the sun, far out along the trajectory the moon light will shine, a moon texture.
// (a specular transparent material assigned to a quad, with a texture sprite using transparent alpha).
// optionally have a domed mesh cover the main area, using a material linked to a shadergraph
// using noise patterns to blend alpha and colour to generate clouds.
// The skygradient scriptable object contains two gradients, controlling the light colour
// of the sun, and the overall ambient light of the scene.
// The timeofday scriptable object stores a float (time), and a string (phase) -
// allow all scene objects to refer to this to find the time of day.
// Stars is a gameobject holding a particle effect.
//////// LIGHTING //////////////////
// Skybox Material: Skybox_Mat (shader = Skybox/Procedural) (a generic empty sky)
// Sun Source: your sun direcional light
// Environmental lighting Source: Skybox.
///////////////////////////////////////////////////////////////////////////////////////////////

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private Light moon;
    [SerializeField] private GameObject stars;
    [SerializeField] private SkyGradient_ScriptableObject presetGradient;
    [SerializeField, Range(0, 24)] public float timeOfDay;
    [SerializeField, Range(1, 100)] private float speed = 1f;
    private enum timePhases { MORNING, AFTERNOON, EVENING, NIGHT }
    [SerializeField] private timePhases dayPhase;
    [SerializeField] private TimeOfDay_ScriptableObject todSO;

    private bool sunIsOut = false;

    private void Awake()
    {
        NightTime();
    }
    private void LateUpdate()
    {
        timeOfDay += Time.deltaTime / speed;
        timeOfDay %= 24;
        UpdateLights(timeOfDay / 24f);

        if (sunIsOut == false && (timeOfDay > 5f && timeOfDay < 18f))
        {
            DayTime();
        }

        else if (sunIsOut == true && (timeOfDay < 5f || timeOfDay > 18f))
        {
            NightTime();
        }

        if (dayPhase != timePhases.MORNING && timeOfDay > 6f && timeOfDay < 12f )
        {
            dayPhase = timePhases.MORNING;
        }
        else if (dayPhase != timePhases.AFTERNOON && timeOfDay > 12f && timeOfDay < 16f)
        {
            dayPhase = timePhases.AFTERNOON;
        }
        else if (dayPhase != timePhases.EVENING && timeOfDay > 16f && timeOfDay < 20f)
        {
            dayPhase = timePhases.EVENING;
        }
        else if (dayPhase != timePhases.NIGHT && timeOfDay > 20f || timeOfDay < 6f)
        {
            dayPhase = timePhases.NIGHT;
        }

        todSO.time = timeOfDay;
        todSO.phase = dayPhase.ToString();
    }

    private void UpdateLights(float timePercent)
    {
        RenderSettings.ambientLight = presetGradient.AmbientColour.Evaluate(timePercent);
        sun.color = presetGradient.DirectionalColour.Evaluate(timePercent);
        //RenderSettings.fogColor = presetGradient.FogColour.Evaluate(timePercent);

        sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
    }

    private void DayTime()
    {
        sun.enabled = true;
        moon.enabled = false;
        stars.SetActive(false);
        sunIsOut = true;
    }

    private void NightTime()
    {
        sun.enabled = false;
        moon.enabled = true;
        stars.SetActive(true);
        sunIsOut = false;
    }
}
