using UnityEngine;

public class FurnaceInteraction : MonoBehaviour
{
    public PlayerInventory playerInventory;

    public void SmeltHeldItem()
    {
        ItemData heldItem =
            playerInventory.GetSelectedItem();

        //If not holding anything, return empty and nothing happens
        if (heldItem == null)
        {
            Debug.Log("The selected slot is empty");
            return;
        }
        //Detect if held item is a ore or not
        if (heldItem.itemCategory != ItemCategory.Ore)
        {
            Debug.Log(heldItem.itemName + " is not ore");
            return;
        }
        //Unable to smelt non-ores
        if (heldItem.smeltedVersion == null)
        {
            Debug.Log(
                heldItem.itemName +
                " does not have a smelted version assigned"
            );

            return;
        }

        //If held item is ore, smelt it
        playerInventory.ReplaceSelectedItem(
            heldItem.smeltedVersion
        );

        Debug.Log(
            heldItem.itemName +
            " was smelted into " +
            heldItem.smeltedVersion.itemName
        );
    }
}
