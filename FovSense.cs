// C NELSON 2020
// Check an objects field of view and line of sight, along it's Z axis, towards a given target

using UnityEngine;

public class FovSense : MonoBehaviour
{
    [SerializeField]
    private GameObject target; // spine bone makes a reliable target

    [SerializeField]
    private bool targetInFOV = false;

    [SerializeField]
    public bool canSeeTarget = false;

    [SerializeField]
    private float viewDistance = 30f;

    [SerializeField]
    private string targetTag = "Player";

    [SerializeField]
    private LayerMask layerMask;

    private RaycastHit hit;
    private Vector3 fovDirection;
    private float distanceToTarget;


    void FixedUpdate()
    {
        // get targets distance
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        // if within distance
        if (distanceToTarget < viewDistance)
        {
            // get vector between object and target
            fovDirection = target.transform.position - transform.position;
            
            // is target within vision cone of this object
            targetInFOV = Vector3.Dot(transform.forward, fovDirection.normalized) > 0.5f;
        }
        // not within distance
        else
        {
            targetInFOV = false;
        }

        // if in FOV, raycast for obstructions
        if (targetInFOV && Physics.Raycast(transform.position, fovDirection, out hit, viewDistance, layerMask)) 
        {
            ////debug helpers
            Debug.DrawRay(transform.position, fovDirection * hit.distance, Color.yellow);
            Debug.Log("Hit: " + hit.collider.gameObject.name);

            // if raycast hit the target
            if (hit.collider.gameObject.CompareTag(targetTag))
            {
                canSeeTarget = true;
            }
            // if raycast hit something else
            else
            {
                canSeeTarget = false;
            }
        }
        // if not in FOV or didnt hit anything
        else
        {
            canSeeTarget = false;
        }
    }
}
