using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //using Images for the icons

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    
    //private variables below
    ItemID _item;

    void Start()
    {
        _item = GameObject.FindObjectOfType<ItemID>();
    }

    public void AddItem (ItemID newItem)
    {
        _item = newItem;
        icon.sprite = _item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
