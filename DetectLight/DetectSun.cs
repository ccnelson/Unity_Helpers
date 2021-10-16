// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// performs raycast between target and light source.
// assign target (player head bone) and sunlight source (directional light).
// select layers to be ignored (player and ground).

public class DetectSun : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Light sun;
    [SerializeField] private LayerMask layers;
    private RaycastHit hit;
    public bool isInSunlight;
    private float timer = 0f;

    private void LateUpdate()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            isInSunlight = Detect();
            timer = 0f;
        }
    }

    private bool Detect()
    {
        Ray ray = new Ray(target.transform.position, -sun.transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layers))
        {
            return false;
        }

        return true;
    }
}
