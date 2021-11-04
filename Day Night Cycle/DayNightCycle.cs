// C NELSON 2021
using System.Collections;
using UnityEngine;

////////////////////////////////////////////////////////////////////////////////////////////////
// Attach this script to an empty scenemanager object.
// Assign a sun and moon (directional lights), SkyGradient and TimeOfDay (scriptable objects) 
/////// SETUP ////////////////////
// In the centre of the scene have a bright directional yellow light, the sun.
// As a child of the sun, in the same position but pointing in the opposite direction,
// a dull blue light as the moon.
// Also a child of the sun, far out along the trajectory the moon light will shine, a sphere with a moon texture.
// Optionally have a domed mesh cover the main area, using a material linked to a shadergraph
// using noise patterns to blend alpha and colour to generate clouds.
// The skygradient scriptable object contains two gradients, controlling the light colour
// of the sun, and the overall ambient light of the scene.
// The timeofday scriptable object stores a float (time), and a string (phase) -
// allow all scene objects to refer to this to find the time of day.
// Stars is a gameobject holding a particle effect.
//////// LIGHTING //////////////////
// Skybox Material: Skybox_Mat (shader = Skybox/Procedural) (a generic empty sky)
// Or SkyBox Material: Default-Skybox works just as well.
// Sun Source: your sun direcional light
// Environmental lighting Source: Skybox (makes ambient light redundant).
// Environmental lighting Source: Colour (use ambient gradient alongside directional lights).
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

    private bool sunIsOut;

    private void Awake()
    {
        NightTime();
        dayPhase = timePhases.NIGHT;
        sunIsOut = false;
    }

    private void Start()
    {
        StartCoroutine(TimeCoroutine());
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

    IEnumerator TimeCoroutine()
    {
        while (true)
        {
            timeOfDay += 0.5f / speed;
            timeOfDay %= 24;
            UpdateLights(timeOfDay / 24f);

            if (sunIsOut == false && dayPhase == timePhases.MORNING)
            {
                DayTime();
            }
            else if (sunIsOut == true && dayPhase == timePhases.NIGHT)
            {
                NightTime();
            }

            switch (Mathf.Floor(timeOfDay))
            {
                case 6:
                    dayPhase = timePhases.MORNING;
                    break;
                case 12:
                    dayPhase = timePhases.AFTERNOON;
                    break;
                case 16:
                    dayPhase = timePhases.EVENING;
                    break;
                case 20:
                    dayPhase = timePhases.NIGHT;
                    break;
            }

            todSO.time = timeOfDay;
            todSO.phase = dayPhase.ToString();

            yield return new WaitForSeconds(0.25f);
        }
    }
}
