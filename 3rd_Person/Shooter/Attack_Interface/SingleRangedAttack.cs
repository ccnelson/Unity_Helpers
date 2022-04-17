// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Implementation of IAttack interface.
// The parent object is assumed to use FinalIK's aimIK.
// Aim transform should be an empty object attached to parent.
// Barrel is location projectile will instatiate.
// Projectile is a prefab bullet with self contained behaviour.
// Pooling and sounds need to be set, to be passed to projectile.
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRangedAttack : MonoBehaviour, IAttack
{
    [SerializeField] private GameObject projectile;

    public Transform barrel;
    public Transform target;
    public float speed = 1000;

    [SerializeField] private AimIK aimik;
    [SerializeField] private Transform aimTransform;

    [SerializeField] ParticleObjectPooler projectilePool;
    [SerializeField] ParticleObjectPooler impactPool;
    [SerializeField] AudioSource bulletShootSound;
    [SerializeField] AudioSource bulletImpactSound;

    private Vector3 targetOffset { get { return new Vector3(target.position.x, target.position.y + 1f, target.position.z); } }


    public void Attack()
    {
        //StartAim();
        GameObject bullet = Instantiate(projectile, barrel.position, barrel.rotation);
        Projectile p = bullet.GetComponent<Projectile>();
        p.projectilePool = projectilePool;
        p.impactPool = impactPool;
        p.bulletShootSound = bulletShootSound;
        p.bulletImpactSound = bulletImpactSound;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * speed);
    }


    public void StartAim()
    {
        aimTransform.position = targetOffset;
        aimik.solver.target = aimTransform;
        aimik.solver.IKPositionWeight = 1;
    }

    public void StopAim()
    {
        aimik.solver.IKPositionWeight = 0;
    }

}
