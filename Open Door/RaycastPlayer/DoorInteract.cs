// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to door with collider

[RequireComponent(typeof(Collider))]

public class DoorInteract : MonoBehaviour
{
    Animator anim;
    public bool dooropen = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        ToggleDoor();
    }

    void ToggleDoor()
    {
        if (dooropen)
        {
            DoorClose();
        }
        else
        {
            DoorOpen();
        }
    }

    public void DoorOpen()
    {
        dooropen = true;
        anim.Play("dooropen");
    }

    public void DoorClose()
    {
        dooropen = false;
        anim.Play("doorclose");
    }
}
