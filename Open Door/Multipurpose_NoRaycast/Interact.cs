// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// attach to player
// interactive object will use OnTriggerEnter / Exit to set 
// required interaction fields.
// In heirarchy Canvas > Panel > Text
// In insepctor assign uiText = text, uiPanel = Panel
public class Interact : MonoBehaviour
{
    [SerializeField] KeyCode interactionKey = KeyCode.E;

    public DoorMechanism interactiveDoor;
    public bool isNearDoor;

    public Text uiText;
    public GameObject uiPanel;

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            AttemptInteraction();
        }

        if (isNearDoor)
        {
            uiPanel.SetActive(true);
            uiText.text = "(E) Interact";
        }
        else
        {
            uiPanel.SetActive(false);
            uiText.text = "";
        }
    }

    void AttemptInteraction()
    {
        if (isNearDoor)
        {
            interactiveDoor.Interact();
        }
            
    }
}