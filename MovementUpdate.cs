// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - SETUP: - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
// Attach script to Navmesh agent to prevent large turning circle / drifting / sliding.
// Assumes Animator is a child object, using parameter "forwardSpeed"
// - - - - - - - - - - - EDITOR: Nav Mesh Agent - - - - - - - - - - - - - - - - - - - - - 
// speed = 1.2 (walking)
// angular speed = 0 (steering is handled in code below)
// acceleration = 5 (slightly more than speed)
// stopping distance = 0
// auto braking = false (as deceleration can give a sliding effect also)
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using UnityEngine;
using UnityEngine.AI;


public class MovementUpdate : MonoBehaviour
{
    private NavMeshAgent navmesh;
    private Animator animator;
    private float rotSpeed = 5f;

    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // match animator speed parameter to agent velocity
        Vector3 velocity = navmesh.velocity;
        Vector3 localvelocity = transform.InverseTransformDirection(velocity);
        float speed = localvelocity.z;
        animator.SetFloat("forwardSpeed", speed);

        // get direction of destination
        Vector3 dir = navmesh.steeringTarget - transform.position;
        // only rotate y axis
        dir.y = 0; 
        // ensure we have a valid rotation, and agent is moving
        if (dir != Vector3.zero && speed != 0)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            // rotate the agent
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
        }
    }
}