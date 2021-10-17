// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageUI : MonoBehaviour
{
    public GameObject buttonprefab;
    int interactions = 0;
    public ManageModularOutfit outfits;
    public UnityEngine.UI.Text header;
    public void CreateInterface(StorageInteraction si)
    {
        // guard from multiple interactions ////
        interactions += 1;
        if (interactions > 1)
            return;
        ////////////////////////////////////////

        if (si.contents.Count > 0)
        {
            header.text = "Contents:";
        }
        else
        {
            header.text = "Empty";
        }

        for (int i = 0; i < si.contents.Count; i++)
        {
            GameObject g = Instantiate(buttonprefab, transform);
            g.GetComponent<RectTransform>().localPosition = new Vector3(0f, (0f - (20f * i)), 0f);
            var text = g.GetComponent<UnityEngine.UI.Text>();
            text.text = outfits.nameMap[si.contents[i].item.name];
            var button = g.GetComponentInChildren<UnityEngine.UI.Button>();
            int index = i;
            button.onClick.AddListener(() => ButtonOnClick(si, index));
        }
    }


    void ButtonOnClick(StorageInteraction s, int ind)
    {
        // if there is an active object
        if (outfits.mmomData.currentHat != null)
        {
            // get it, add to storage, and deactivate
            GameObject g = outfits.mmomData.currentHat.gameObject;
            s.AddContents(g, ItemType.hats);
            g.SetActive(false);
        }   

        // make new hat current hat, activate, remove from storage
        outfits.mmomData.currentHat = s.contents[ind].item.transform;
        s.contents[ind].item.SetActive(true);
        s.RemoveContents(s.contents[ind]);
        HideInterface();
    }

    public void HideInterface()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        this.gameObject.SetActive(false);

        interactions = 0;
        header.text = "";
    }
}
