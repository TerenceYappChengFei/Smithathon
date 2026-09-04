using UnityEngine;

// Gives one configured ItemData to the first empty inventory slot.
public class StationItemButton : MonoBehaviour
{
    public ItemData itemToGive;
    public PlayerInventory playerInventory;

    //Checks player inventory for space, only gives item if it's not full
    public void GiveItem()
    {
        bool itemAdded = playerInventory.AddItem(itemToGive);

        if (itemAdded)
        {
            Debug.Log(itemToGive.itemName + " added to inventory");
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }
}
