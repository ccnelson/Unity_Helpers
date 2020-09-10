
using UnityEngine;

public class fovsens : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    [SerializeField]
    public bool playerInSight = false;


    void Update()
    {
        // player object is within vision cone of this object
        playerInSight = Vector3.Dot(transform.forward, (player.transform.position - transform.position).normalized) > 0.5f;

    }
}
