using UnityEngine;

public class StationAssemblyButton : MonoBehaviour
{
    public PlayerInventory playerInventory;

    public ItemData requiredItem1;
    public ItemData requiredItem2;
    public ItemData resultItem;

    public void AssembleItem()
    {
        bool hasRequiredItems =
            playerInventory.HasItems(
                requiredItem1,
                requiredItem2
            );

        if (hasRequiredItems)
        {
            playerInventory.CombineItems(resultItem);

            Debug.Log(
                requiredItem1.itemName +
                " and " +
                requiredItem2.itemName +
                " were assembled into " +
                resultItem.itemName
            );
        }
        else
        {
            Debug.Log("The required items are not in the hotbar");
        }
    }
}
