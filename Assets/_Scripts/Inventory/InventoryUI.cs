using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI :  MonoBehaviour
{

    public Transform itemsParent;
    public GameObject inventoryUI;

    //private variables below
    Inventory _inventory;
    InventorySlot[] slots;
    Controller2D _charController;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory.instance;
        _inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        _charController = GameObject.FindObjectOfType<Controller2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //scroll through the items in the inventory slots
        HighlightItem();
    }
    
    void UpdateUI()
    {
        Debug.Log("Updating UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < _inventory.items.Count)
            {
                slots[i].AddItem(_inventory.items[i]);
            } else {
                slots[i].ClearSlot();
            }   
        }
    }

    void HighlightItem()
    {
        if(InputManager.im.GetSlotPressed())
            StartCoroutine(SelectFirstChoice());
    }

    IEnumerator SelectFirstChoice()
    {
        // cleared first, then wait
        //for at least one frame befroe setting current object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(slots[0].gameObject);
    }
}
