using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public ItemID tear;
    public ItemID heart;
    public string npcName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveItem(ItemID item)
    {
        Debug.Log("Giving earned item");
        bool wasPickedUp = Inventory.instance.AddItems(item);

        if(wasPickedUp)
        {
            Debug.Log("Picked up check");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

    }

    void OnTriggerExit2D(Collider2D other)
    {

    }
}
