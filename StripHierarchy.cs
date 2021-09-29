// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to heirarchy parent objects used for organization.
// (Flatten hierarchy and remove prior to final build)

public class StripHierarchy : MonoBehaviour
{
    private void Awake()
    {
        // get the transforms of all children
        Transform[] childObjects = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i);
        }

        // iterate children removing parent
        foreach (Transform ct in childObjects)
        {
            ct.SetParent(null);
        }
        
        //destroy self
        Destroy(this.gameObject);
    }
}
