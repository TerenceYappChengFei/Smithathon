using UnityEngine;
using UnityEngine.UI;

public class AnvilInteraction : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public Image submittedItemIcon;
    public Image swordMoldImage;
    public Image axeMoldImage;
    public Image spearMoldImage;

    public Color normalMoldColor = Color.white;
    public Color selectedMoldColor = Color.yellow;

    private ItemData submittedIngot;
    private int submittedSlot;
    private string selectedMold;

    private void OnEnable()
    {
        ClearSubmittedIngot();
        selectedMold = null;
        UpdateMoldHighlights();
    }

    public void SelectSwordMold()
    {
        selectedMold = "Sword";
        UpdateMoldHighlights();
        Debug.Log("Sword mold selected");
    }

    public void SelectAxeMold()
    {
        selectedMold = "Axe";
        UpdateMoldHighlights();
        Debug.Log("Axe mold selected");
    }

    public void SelectSpearMold()
    {
        selectedMold = "Spear";
        UpdateMoldHighlights();
        Debug.Log("Spear mold selected");
    }

    private void UpdateMoldHighlights()
    {
        swordMoldImage.color = normalMoldColor;
        axeMoldImage.color = normalMoldColor;
        spearMoldImage.color = normalMoldColor;

        if (selectedMold == "Sword")
        {
            swordMoldImage.color = selectedMoldColor;
        }
        else if (selectedMold == "Axe")
        {
            axeMoldImage.color = selectedMoldColor;
        }
        else if (selectedMold == "Spear")
        {
            spearMoldImage.color = selectedMoldColor;
        }
    }

    public void ForgeWeaponHead()
    {
        if (submittedIngot == null)
        {
            Debug.Log("Submit an ingot first");
            return;
        }

        if (selectedMold == null)
        {
            Debug.Log("Select a mold first");
            return;
        }

        if (submittedSlot == 1)
        {
            playerInventory.SelectSlot1();
        }
        else
        {
            playerInventory.SelectSlot2();
        }

        if (playerInventory.GetSelectedItem() != submittedIngot)
        {
            Debug.Log("The submitted ingot is no longer in the hotbar");
            ClearSubmittedIngot();
            return;
        }

        ItemData resultItem = null;

        if (selectedMold == "Sword")
        {
            resultItem = submittedIngot.swordHeadVersion;
        }
        else if (selectedMold == "Axe")
        {
            resultItem = submittedIngot.axeHeadVersion;
        }
        else if (selectedMold == "Spear")
        {
            resultItem = submittedIngot.spearHeadVersion;
        }

        if (resultItem == null)
        {
            Debug.Log("This ingot does not have a weapon head assigned");
            return;
        }

        playerInventory.ReplaceSelectedItem(resultItem);

        Debug.Log(
            submittedIngot.itemName +
            " was forged into " +
            resultItem.itemName
        );

        ClearSubmittedIngot();
        selectedMold = null;
        UpdateMoldHighlights();
    }

    public void SubmitHeldIngot()
    {
        ItemData heldItem =
            playerInventory.GetSelectedItem();

        if (heldItem == null)
        {
            Debug.Log("The selected slot is empty");
            return;
        }

        if (heldItem.itemCategory != ItemCategory.Ingot)
        {
            Debug.Log(heldItem.itemName + " is not an ingot");
            return;
        }

        submittedIngot = heldItem;
        submittedSlot = playerInventory.selectedSlot;

        submittedItemIcon.sprite =
            submittedIngot.itemIcon;

        submittedItemIcon.gameObject.SetActive(true);

        Debug.Log(
            submittedIngot.itemName +
            " was submitted to the anvil"
        );
    }

    private void ClearSubmittedIngot()
    {
        submittedIngot = null;
        submittedSlot = 0;

        if (submittedItemIcon != null)
        {
            submittedItemIcon.sprite = null;
            submittedItemIcon.gameObject.SetActive(false);
        }
    }
}
