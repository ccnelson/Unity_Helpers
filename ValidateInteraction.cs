// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Validate if an object exists directly in front of this.gameObject,
// and if it is tagged as "NPC"

public class ValidateInteraction : MonoBehaviour
{
    RaycastHit hit;
    private string targetTag = "NPC";

    [SerializeField]
    public GameObject target;


    public bool Test()
    {
        if (Physics.SphereCast(transform.position, 0.2f, transform.forward, out hit, 0.3f))
        {
            if (hit.collider.gameObject.CompareTag(targetTag))
            {
                target = hit.collider.gameObject;
                return true;
            }
            else
            {
                target = null;
                return false;
            }
        }
        else
        {
            target = null;
            return false;
        }
    }
}
