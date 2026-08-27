using UnityEngine;

public class StationItemButton : MonoBehaviour
{
    public ItemData itemToGive;
    public PlayerInventory playerInventory;

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
