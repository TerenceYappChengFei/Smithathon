using UnityEngine;

// Replaces a valid dull weapon with its sharpened version.
public class GrindstoneInteraction : MonoBehaviour
{
    public PlayerInventory playerInventory;

    public void SharpenHeldWeapon()
    {
        ItemData heldItem =
            playerInventory.GetSelectedItem();

        //If not holding anything, return empty and nothing happens
        if (heldItem == null)
        {
            Debug.Log("The selected slot is empty");
            return;
        }
        //Detect if held item is a weapon or not
        if (heldItem.itemCategory != ItemCategory.Weapon)
        {
            Debug.Log(heldItem.itemName + " is not a weapon");
            return;
        }
        //Nothing happens when you sharpen a sharp weapon
        if (heldItem.weaponCondition != WeaponCondition.Dull)
        {
            Debug.Log(heldItem.itemName + " is not dull");
            return;
        }
        //Detects if the item held can be sharpened or not
        if (heldItem.sharpenedVersion == null)
        {
            Debug.Log(
                heldItem.itemName +
                " does not have a sharp version assigned"
            );

            return;
        }
        //If item held is a dull weapon, replace with the corresponding sharpened version
        playerInventory.ReplaceSelectedItem(
            heldItem.sharpenedVersion
        );

        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayGrinding();
        }


        Debug.Log(
            heldItem.itemName +
            " was sharpened into " +
            heldItem.sharpenedVersion.itemName
        );
    }
}
