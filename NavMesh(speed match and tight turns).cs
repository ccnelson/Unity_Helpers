using System.Collections;
using System.Collections.Generic;
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
        // match agent speed to animation speed
        Vector3 velocity = navmesh.velocity;
        Vector3 localvelocity = transform.InverseTransformDirection(velocity);
        float speed = localvelocity.z;
        animator.SetFloat("forward_speed", speed);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        // To prevent large turning circle with patrolling behaviour:
        // - - - - - - - - - - - EDITOR: Nav Mesh Agent - - - - - - - - - - - - - - - - - - - - - 
        // speed = 1.2 (walking)
        // angular speed = 0 (steering is handled in code below)
        // acceleration = 5 (slightly more than speed)
        // stopping distance = 0
        // auto braking = false (as deceleration can give a sliding effect also)
        //
        // rotate agent to look at target
        Vector3 dir = navmesh.steeringTarget - transform.position;
        dir.y = 0; // only rotate on y axis
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}
