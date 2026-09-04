using UnityEngine;

public class WorkbenchInteraction : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public ItemData defectiveWeapon;

    public void AssembleHeldItems()
    {
        if (playerInventory.slot1Item == null ||
            playerInventory.slot2Item == null)
        {
            Debug.Log("Two items are required for assembly");
            return;
        }

        ItemData weaponHead = null;
        ItemData handleMaterial = null;

        //Find the weapon head, regardless of which slot it is in.
        if (playerInventory.slot1Item != null &&
            playerInventory.slot1Item.itemCategory == ItemCategory.WeaponPart)
        {
            weaponHead = playerInventory.slot1Item;
            handleMaterial = playerInventory.slot2Item;
        }
        else if (playerInventory.slot2Item != null &&
                 playerInventory.slot2Item.itemCategory == ItemCategory.WeaponPart)
        {
            weaponHead = playerInventory.slot2Item;
            handleMaterial = playerInventory.slot1Item;
        }

        bool validRecipe =
            weaponHead != null &&
            handleMaterial == weaponHead.requiredAssemblyItem &&
            weaponHead.assembledVersion != null;

        if (validRecipe)
        {
            playerInventory.CombineItems(weaponHead.assembledVersion);

            if (SFXManager.instance != null)
            {
                SFXManager.instance.PlayCrafting();
            }


            Debug.Log(
                weaponHead.itemName +
                " was assembled into " +
                weaponHead.assembledVersion.itemName
            );

            return;
        }

        if (defectiveWeapon == null)
        {
            Debug.Log("The defective weapon has not been assigned to the workbench");
            return;
        }

        playerInventory.CombineItems(defectiveWeapon);

        Debug.Log("The invalid recipe produced a defective weapon");
    }
}
