// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to door

[RequireComponent(typeof(Collider))]

public class DoorInteract : MonoBehaviour
{
    Animator anim;
    bool dooropen = false;

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
            anim.Play("doorclose");
            dooropen = false;
        }
        else
        {
            anim.Play("dooropen");
            dooropen = true;
        }
    }
}
