// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to empty object with same position as door (not a child)
// Add sphere collider trigger.
// In inspector link the di field with the DoorMechanism script on the door
// make sure NPC & player have rigidbody, collider, and tag

public class DoorInteraction : MonoBehaviour
{
    public DoorMechanism di;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            if (!di.dooropen)
            {
                di.DoorOpen();
            }
        }

        if (other.CompareTag("Player"))
        {
            Interact playerInteractor = other.GetComponentInChildren<Interact>();
            playerInteractor.isNearDoor = true;
            playerInteractor.interactiveDoor = di;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            if (di.dooropen)
            {
                di.DoorClose();
            }
        }

        if (other.CompareTag("Player"))
        {
            Interact playerInteractor = other.GetComponentInChildren<Interact>();
            playerInteractor.isNearDoor = false;
            other.GetComponentInChildren<Interact>().interactiveDoor = null;
        }
    }
}
