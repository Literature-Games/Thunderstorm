using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallBack;

    public List<ItemID> items = new List<ItemID>();
    public int spaces = 4; //defualt spaces
    public static Inventory instance;

    //private variables below

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one inventory.");
            return;
        }
        instance = this;
    }

    public void GetCount(int amount)
    {
        amount = items.Count;
    }

    public bool AddItems(ItemID item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= spaces)
            {
                Debug.Log("Not enough space");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }
        for(int i = 0; i < items.Count; i++)
        {
            Debug.Log("In Inventory: " + items[i]);
        }
        return true;
    }

    public void RemoveItems(ItemID item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    public void RemoveItemsByString(string name, int amount)
    {
        Debug.Log("Begin removing"); 
        for (int i = amount; i >= 0; i--)
        {
            for (int j = 0; j < items.Count; j++)
            {
                Debug.Log("Looking for name"); 
                if(items[j].name == name)
                {
                    Debug.Log("found: " + items[j].name); 
                    RemoveItems(items[j]);
                    Debug.Log("removed");
                }
            }
        }
    }
}
