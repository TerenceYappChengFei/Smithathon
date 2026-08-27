using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public ItemData slot1Item;
    public ItemData slot2Item;

    public Image slot1Icon;
    public Image slot2Icon;

    public Image slot1Background;
    public Image slot2Background;

    public Color selectedColor = Color.yellow;
    public Color normalColor = Color.white;

    public int selectedSlot = 1;

    private void Start()
    {
        UpdateHotbar();
        UpdateSelection();
    }

    public void UpdateHotbar()
    {
        UpdateSlot(slot1Icon, slot1Item);
        UpdateSlot(slot2Icon, slot2Item);
    }

    private void UpdateSlot(Image itemIcon, ItemData item)
    {
        if (item != null)
        {
            itemIcon.sprite = item.itemIcon;
            itemIcon.gameObject.SetActive(true);
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.gameObject.SetActive(false);
        }
    }


    public void SelectSlot1()
    {
        selectedSlot = 1;
        UpdateSelection();
    }

    public void SelectSlot2()
    {
        selectedSlot = 2;
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        //changes color if slot 1 is selected
        if (selectedSlot == 1)
        {
            slot1Background.color = selectedColor;
            slot2Background.color = normalColor;
        }

        //changes color if slot 2 is selected
        else
        {
            slot1Background.color = normalColor;
            slot2Background.color = selectedColor;
        }
    }

    // Adds an item to the first available slot in the inventory. Returns true if the item was added successfully
    public bool AddItem(ItemData newItem)
    {
        //Tries to add to slot 1 first
        if (slot1Item == null)
        {
            slot1Item = newItem;
            UpdateHotbar();
            return true;
        }

        //If slot 1 is full, tries to add to slot 2
        else
            if (slot2Item == null)
            {
                slot2Item = newItem;
                UpdateHotbar();
                return true;
            }
        //If both slots are full, return false, no item was added
        return false;
    }

    //Reads the the item in the selected slot and returns it. If no item is selected, returns null
    //Stations will be able to check what item player is holding
    public ItemData GetSelectedItem()
    {
        if (selectedSlot == 1)
        {
            return slot1Item;
        }

        return slot2Item;
    }

    //Removes the item in the selected slot when interact with scrap bin
    //Also updates the hotbar to reflect the change
    public void RemoveSelectedItem()
    {
        if (selectedSlot == 1)
        {
            slot1Item = null;
        }
        else
        {
            slot2Item = null;
        }

        UpdateHotbar();
    }

    //Keeps the result in the same selected slot instead of adding or removing.
    public void ReplaceSelectedItem(ItemData newItem)
    {
        if (selectedSlot == 1)
        {
            slot1Item = newItem;
        }
        else
        {
            slot2Item = newItem;
        }

        UpdateHotbar();
    }

    //Time to check crafting UGGGGGGGGGGGGGGGGGGGGGGGGGGHHHHHHHHHHHHHHHH

    //Checks for viable crafting recipe, and the order can either in inventory
    public bool HasItems(ItemData firstItem, ItemData secondItem)
    {
        bool normalOrder =
            slot1Item == firstItem &&
            slot2Item == secondItem;

        bool reverseOrder =
            slot1Item == secondItem &&
            slot2Item == firstItem;

        return normalOrder || reverseOrder;
    }

    public void CombineItems(ItemData resultItem)
    {
        slot1Item = resultItem;
        slot2Item = null;

        selectedSlot = 1;

        UpdateHotbar();
        UpdateSelection();
    }

}
