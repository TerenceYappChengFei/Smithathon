using UnityEngine;

// Permanently removes the item in the selected hotbar slot.
public class ScrapBinInteraction : MonoBehaviour
{
    public PlayerInventory playerInventory;

    public void DestroyHeldItem()
    {
        //Asks inventory for the currently held item and removes it from the inventory if it exists
        ItemData heldItem = playerInventory.GetSelectedItem();

        //If there was something, destroy it
        if (heldItem != null)
        {
            Debug.Log(heldItem.itemName + " was destroyed");

            playerInventory.RemoveSelectedItem();
        }
        //If there was nothing, log that the selected slot is empty
        else
        {
            Debug.Log("The selected slot is empty");
        }
    }
}
