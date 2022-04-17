// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Projectile using pooled particle effects.
// Pool and Sounds are set by script instantiating the parent object.
// Collision detection is a little expensive, but accurate.
// Impact particle set to 'Stop Action = Disable' so doesn't need
// returning to pool.
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject projectileParticle;

    public ParticleObjectPooler projectilePool;
    public ParticleObjectPooler impactPool;
    public AudioSource bulletShootSound;
    public AudioSource bulletImpactSound;

    float rad;
    float collideOffset = 0.15f;
    Rigidbody rb;


    void Start()
    {
        bulletShootSound.pitch = Random.Range(0.8f, 1.1f);
        bulletShootSound.Play();
        projectileParticle = projectilePool.GetFromPool(transform.position, transform.rotation);
        projectileParticle.transform.parent = transform;

        rad = transform.GetComponent<SphereCollider>().radius;
        rb = transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 dir = transform.GetComponent<Rigidbody>().velocity;
        dir = dir.normalized;
        float dist = rb.velocity.magnitude * Time.deltaTime;

        if (Physics.SphereCast(transform.position, rad, dir, out hit, dist))
        {
            transform.position = hit.point + (hit.normal * collideOffset);

            impactPool.Spawn(transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal));
            bulletImpactSound.pitch = Random.Range(0.8f, 1.1f);
            bulletImpactSound.Play();

            if (hit.transform.gameObject.TryGetComponent(out IBulletTarget bulletTarget))
            {
                bulletTarget.Damage(hit.point);
            }

            // return particle to pool
            projectileParticle.transform.SetParent(null);
            projectileParticle.SetActive(false);

            Destroy(gameObject);

        }
    }
}

