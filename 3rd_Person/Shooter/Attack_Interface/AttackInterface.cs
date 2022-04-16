// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Component linking implementation of IAttack interface to helper methods.
// Script will find the implementation of IAttack attached to parent gameobject
// and provide standarised methods for access.
// In a behaviour tree this can be used to as an access point to 
// different implementations of attack and aim.
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInterface : MonoBehaviour
{
    private IAttack attackType;

    private void Awake()
    {
        attackType = GetComponent<IAttack>();
    }

    public void DoAttack()
    {
        attackType.Attack();
    }

    public void DoStartAim()
    {
        attackType.StartAim();
    }

    public void DoStopAim()
    {
        attackType.StopAim();
    }
}
