// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to empty object with same position as door (not a child)
// Add sphere collider trigger
// make sure NPC has a rigidbody, collider, and tag
// In inspector link the di field with the DoorInteract script on the door

public class NPCDoorInteract : MonoBehaviour
{
    public DoorInteract di;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            di.Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            di.Interact();
        }
    }
}
