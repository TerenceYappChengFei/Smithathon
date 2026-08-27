using UnityEngine;

[CreateAssetMenu(

    fileName = "NewItem",
    menuName = "Smithathon/Item"
)]
public class ItemData : ScriptableObject
{
    //Defines item name
    public string itemName;
    //Defines item icon
    public Sprite itemIcon;
}
