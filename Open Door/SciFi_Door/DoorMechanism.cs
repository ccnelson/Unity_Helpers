using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    [SerializeField] private Vector3 closedPosition;
    [SerializeField] public Vector3 openPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Transform doorObject;
    [SerializeField] private AudioSource soundFX;

    private int colliderCount = 0;
    private string PlayerTag = "Player";
    private string AITag = "AI";

    private void Start()
    {
        targetPosition = closedPosition;
    }

    private void LateUpdate()
    {
        if (doorObject.localPosition != targetPosition)
        {
            doorObject.localPosition = Vector3.Lerp(doorObject.localPosition, targetPosition, Time.deltaTime * 10f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag) || other.CompareTag(AITag))
        {
            colliderCount += 1;

            if (colliderCount == 1)
            {
                soundFX.Play();
                targetPosition = openPosition;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayerTag) || other.CompareTag(AITag))
        {
            colliderCount -= 1;

            if (colliderCount < 1)
            {
                soundFX.Play();
                targetPosition = closedPosition;
            }
        }   
    }
}
