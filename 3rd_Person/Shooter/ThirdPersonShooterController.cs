using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
//using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private Image crosshair;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    // tracks direction camera is facing
    [SerializeField] private Transform cameraTracker;
    // FX of bullets hitting non-targets
    [SerializeField] private Transform impactFX;
    [SerializeField] private Transform muzzleFX;
    // gun sounds
    [SerializeField] private AudioSource gunShot;

    private Animator anim;

    private StarterAssets.StarterAssetsInputs starterAssetsInputs;
    private StarterAssets.ThirdPersonController thirdPersonController;
    
    private float normalSensitivity = 1f;
    private float aimSensitivity = 0.5f;

    Vector2 screenCentre;

    // exact positon of hit
    private Vector3 hitPosition;
    // transform of object hit
    private Transform hitTransform = null;
    // for interpolating animation layer weight
    [SerializeField] private Transform hitObject;

    private float targetAimWeight;
    // is player aiming
    private bool localAim = false;

    // alternative to null. make the position out of view
    //private Vector3 oneMeterUnderPlayer { get { return transform.position + (1f * Vector3.down); } }

    // position to rotate player towards when aiming
    private Vector3 newLookPos { get { return new Vector3(cameraTracker.position.x, transform.position.y, cameraTracker.position.z); } }


    private void Awake()
    {
        screenCentre = new Vector2(Screen.width / 2f, Screen.height / 2f);

        starterAssetsInputs = GetComponent<StarterAssets.StarterAssetsInputs>();
        thirdPersonController = GetComponent<StarterAssets.ThirdPersonController>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        // only happens when value changes
        if (localAim != starterAssetsInputs.aim)
        {
            if (starterAssetsInputs.aim)
            {
                StartAiming();
            }
            else
            {
                StopAiming();
            }
        }

        if(localAim)
        {
            //Vector2 inputVector = starterAssetsInputs.move;

            anim.SetFloat("x", starterAssetsInputs.move.x);
            anim.SetFloat("y", starterAssetsInputs.move.y);

            Ray ray = Camera.main.ScreenPointToRay(screenCentre);
            if(Physics.Raycast(ray, out RaycastHit raycastHit, 100f, aimColliderLayerMask))
            {
                hitTransform = raycastHit.transform;
                hitPosition = raycastHit.point;
                hitObject.position = raycastHit.point;
            }
            else
            {
                //hitPosition = oneMeterUnderPlayer;
            }

            // rotate player towards screen centre
            transform.LookAt(newLookPos);

            if(starterAssetsInputs.shoot)
            {
                Shoot();
            }
        }
    }

    private void StartAiming()
    {
        localAim = starterAssetsInputs.aim;
        // remove any queued shots
        starterAssetsInputs.shoot = false;
        aimVirtualCamera.gameObject.SetActive(true);
        thirdPersonController.SetSensitivity(aimSensitivity);
        crosshair.gameObject.SetActive(true);
        thirdPersonController.SetRotateOnMove(false);
        anim.SetBool("isAiming", true);
    }

    private void StopAiming()
    {
        localAim = starterAssetsInputs.aim;
        aimVirtualCamera.gameObject.SetActive(false);
        thirdPersonController.SetSensitivity(normalSensitivity);
        crosshair.gameObject.SetActive(false);
        thirdPersonController.SetRotateOnMove(true);
        //hitPosition = oneMeterUnderPlayer;
        anim.SetBool("isAiming", false);
    }

    private void Shoot()
    {
        starterAssetsInputs.shoot = false;
        gunShot.pitch = Random.Range(0.8f, 1.2f);
        gunShot.Play();
        muzzleFX.gameObject.SetActive(true);

        if (hitTransform == null)
        {
            // raycast hasnt hit yet
            return;
        }
        else if (hitTransform.gameObject.TryGetComponent(out IBulletTarget bulletTarget))
        {
            bulletTarget.Damage(hitPosition);
        }
        //else if (hitPosition != oneMeterUnderPlayer)
        else
        {
            Instantiate(impactFX, hitPosition, Quaternion.identity);
        }
        
    }
}
