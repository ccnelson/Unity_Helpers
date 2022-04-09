using Cinemachine;
using Lightbug.CharacterControllerPro.Implementation;
using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RootMotion.FinalIK;

public class ThirdPersonShoot : MonoBehaviour
{
    // to be set in inspector
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private NormalMovement normalMovement;
    [SerializeField] private Image crosshair;
    [SerializeField] private Transform gunHand;
    [SerializeField] private Transform gunHip;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask bulletCollisionMask;
    [SerializeField] private Transform impactFX;
    [SerializeField] private Transform muzzleFX;
    [SerializeField] private AudioSource gunShot;
    [SerializeField] private AudioSource ricochet;
    [SerializeField] private AudioSource equipGun;
    // aim controller takes care of interpolation, and animtion bending restriction
    [SerializeField] private AimController aimController;
    // empty object tracking player aim, used to calibrate ik aimcontroller
    [SerializeField] private Transform aimTransform;

    private bool localAim = false;
    private RaycastHit rayCastHit;
    private float raycastDistance = 333f;
    // object the raycast hits
    private Transform hitTransform;
    // exact position raycast hits
    private Vector3 hitPosition;
    

    // properties
    private Vector3 cameraForwardOutOfRange { get { return cameraTransform.position + ((raycastDistance + 1f) * cameraTransform.forward); } }


    private void Awake()
    {
        hitPosition = cameraForwardOutOfRange;
        aimTransform.position = cameraForwardOutOfRange;
        aimController.target = aimTransform;
    }


    private void Update()
    {
        // runs once when value changes
        if (localAim != normalMovement.wantToAim)
        {
            localAim = normalMovement.wantToAim;

            if (localAim)
            {
                StartAim();
            }
            else
            {
                StopAim();
            }
        }

        // runs if player aiming
        if (localAim)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out rayCastHit, raycastDistance, bulletCollisionMask))
            {
                hitTransform = rayCastHit.transform;
                hitPosition = rayCastHit.point;
            }
            else
            {
                hitPosition = cameraForwardOutOfRange;
            }

            // update IK aim
            aimTransform.position = hitPosition;

            // shoot
            if (normalMovement.wantToShoot)
            {
                normalMovement.wantToShoot = false;
                Shoot();
            }
        }
    }


    private void ToggleGunModel()
    {
        gunHand.gameObject.SetActive(!gunHand.gameObject.activeInHierarchy);
        gunHip.gameObject.SetActive(!gunHip.gameObject.activeInHierarchy);
    }

    
    void StartAim()
    {
        equipGun.pitch = 1f;
        equipGun.Play();
        aimController.weight = 1;
        aimVirtualCamera.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(true);
        ToggleGunModel();
    }


    void StopAim()
    {
        equipGun.pitch = 0.9f;
        equipGun.Play();
        aimController.weight = 0;
        aimVirtualCamera.gameObject.SetActive(false);
        crosshair.gameObject.SetActive(false);
        ToggleGunModel();
    }


    void Shoot()
    {
        gunShot.pitch = Random.Range(0.8f, 1.2f);
        gunShot.Play();
        muzzleFX.gameObject.SetActive(true);

        // raycast hasnt hit yet
        if (hitTransform == null)
        {
            return;
        }
        // raycast didnt hit anything
        else if (hitPosition == cameraForwardOutOfRange)
        {
            return;
        }
        // hit a valid bullet target
        else if (hitTransform.gameObject.TryGetComponent(out IBulletTarget bulletTarget))
        {
            bulletTarget.Damage(hitPosition);
        }
        // hit anything else
        else
        {
            Instantiate(impactFX, hitPosition, Quaternion.identity);
            ricochet.pitch = Random.Range(0.8f, 1.2f);
            ricochet.Play();
        }
    }

}
