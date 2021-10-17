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

    // these get set by object ontrigger enter / exit
    public DoorMechanism interactiveDoor;
    public StorageInteraction interactiveStorage;

    public bool isNearDoor;
    public bool isNearStorage;

    public Text uiTextPrompt;
    public GameObject storageUIScreenPrompt;
    public StorageUI storageUIScript;

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            AttemptInteraction();
        }

        if (isNearDoor || isNearStorage)
        {
            uiTextPrompt.text = "(E) Interact";
        }
        else
        {
            uiTextPrompt.text = "";
        }
    }

    void AttemptInteraction()
    {
        if (isNearDoor)
        {
            interactiveDoor.Interact();
        }


        if (isNearStorage)
        {
            storageUIScreenPrompt.SetActive(true);
            storageUIScript.CreateInterface(interactiveStorage);
        }

    }
}