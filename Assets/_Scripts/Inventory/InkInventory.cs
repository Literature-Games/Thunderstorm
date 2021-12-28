using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

/*
*   This file holds the integration between ink and the inventory system.
*/
public class InkInventory : MonoBehaviour
{
    public static InkInventory ii;

    [Header("Scriptable Items")]
    public ItemID tear;
    public ItemID heart;

    [HideInInspector] public TextAsset inkJSON;

    //private variables below
    Inventory _inventory;

    ///Ink variables
    Story currentStory;

    void Awake()
    {
        if(ii != null)
        {
            Debug.LogWarning("More than one Ink Inventory in scene");
        }
        if(ii == null)
            ii = this.GetComponent<InkInventory>();
    }

    void Start()
    {
        _inventory = Inventory.instance;
    }

    public void checkVariables()
    {
        currentStory = new Story(inkJSON.text);

    }

    public void ItemCheck()
    {
        if(_inventory.items.Count > 0)
        {
            currentStory.variablesState["invCount"] = 1;
        }
        else{
            currentStory.variablesState["invCount"] = 0;
        }

    }

    public void GiveItem(ItemID item)
    {
        Debug.Log("Giving earned item");
        bool wasPickedUp = _inventory.AddItems(item);

        if(wasPickedUp)
        {
            Debug.Log("Picked up check");
        }
    }

    public void GetHeart()
    {
        GiveItem(heart);
    }

    public void GetTear()
    {
       GiveItem(tear);
    }
}
