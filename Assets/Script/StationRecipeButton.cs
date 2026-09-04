using UnityEngine;

// Converts one required inventory item into a configured result item.
public class StationRecipeButton : MonoBehaviour
{
    public PlayerInventory playerInventory;

    public ItemData requiredItem;
    public ItemData resultItem;

    public void CraftItem()
    {
        ItemData heldItem = playerInventory.GetSelectedItem();

        if (heldItem == null)
        {
            Debug.Log("The selected slot is empty");
            return;
        }

        if (heldItem != requiredItem)
        {
            Debug.Log("This station cannot use " + heldItem.itemName);
            return;
        }

        playerInventory.ReplaceSelectedItem(resultItem);

        Debug.Log(
            heldItem.itemName +
            " was changed into " +
            resultItem.itemName
        );
    }
}
