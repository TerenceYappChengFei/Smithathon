using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Sprite slot1Item;
    public Sprite slot2Item;

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

    private void UpdateSlot(Image itemIcon, Sprite itemSprite)
    {
        if (itemSprite != null)
        {
            itemIcon.sprite = itemSprite;
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
}
