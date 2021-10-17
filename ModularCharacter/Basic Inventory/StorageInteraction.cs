// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    hats,
    trousers
}


[System.Serializable]
public class Item
{
    public GameObject item;
    public ItemType itemType;
}

public class StorageInteraction : MonoBehaviour
{
    public List<Item> contents = new List<Item>();
    public StorageUI storageUI;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Interact playerInteractor = other.GetComponentInChildren<Interact>();
            playerInteractor.interactiveStorage = this;
            playerInteractor.isNearStorage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact playerInteractor = other.GetComponentInChildren<Interact>();
            playerInteractor.interactiveStorage = null;
            playerInteractor.isNearStorage = false;
            storageUI.HideInterface();
        }
    }

    public void RemoveContents(Item i)
    {
        contents.Remove(i);
    }

    public void AddContents(GameObject g, ItemType i)
    {
        Item x = new Item();
        x.item = g;
        x.itemType = i;
        contents.Add(x);
    }
}
