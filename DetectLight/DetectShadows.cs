// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// detect if object is in shadows.
// NOTES ////////////////////
// spot & point = check light range against distance
// spot = check light direction 
// all = raycast to check line of sight between target & light
// light types are:
// 0 (spot)
// 1 (directional)
// 2 (point)
//////////////////////////////
// assign target
// assign layers to ignore (ground and player)
// assign lights

public class DetectShadows : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private LayerMask layers;
    [SerializeField] private List<Light> lights;
    private RaycastHit hit;
    private float timer = 0f;
    private bool isInSpot;
    private bool isInDirectional;
    private bool isInPoint;

    public bool isInShadow;
    

    private void LateUpdate()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            for (int i = 0; i < lights.Count; ++i)
            {
                switch ((int)lights[i].type)
                {
                    case 0:
                        isInSpot = InSpot(lights[i]);
                        break;
                    case 1:
                        isInDirectional = InDirectional(lights[i]);
                        break;
                    case 2:
                        isInPoint = InPoint(lights[i]);
                        break;
                    default:
                        break;
                }
            }
            if (!isInSpot && !isInDirectional && !isInPoint)
            {
                isInShadow = true;
            }
            else
            {
                isInShadow = false;
            }
            timer = 0f;
        }
    }

    private bool InSpot(Light l)
    {
        Vector3 lightToTarget = target.transform.position - l.transform.position;
        if (lightToTarget.magnitude > l.range * (l.intensity / 10.0f) || Vector3.Angle(l.transform.forward, lightToTarget) > l.spotAngle / 2.0f)
        {
            return false;
        }

        Ray ray = new Ray(l.transform.position, lightToTarget);
        if (Physics.Raycast(ray, out hit, lightToTarget.magnitude, ~layers))
        {
            return false;
        }

        return true;
    }

    private bool InDirectional(Light l)
    {
        Ray ray = new Ray(target.transform.position, -l.transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layers))
        {
            return false;
        }

        return true;
    }

    private bool InPoint(Light l)
    {
        Vector3 targetToLight = l.transform.position - target.transform.position;
        if (targetToLight.magnitude >= l.range * (l.intensity / 10.0f))
        {
            return false;
        }
            
        Ray ray = new Ray(target.transform.position, targetToLight);
        if (Physics.Raycast(ray, out hit, targetToLight.magnitude, ~layers))
        {
            return false;
        }

        return true;
    }
}
