using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IBulletTarget
{
    [SerializeField] private Transform impactFX;

    public void Damage(Vector3 p)
    {
        Instantiate(impactFX, p, Quaternion.identity);
        Destroy(gameObject);
    }
}
