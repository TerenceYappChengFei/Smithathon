using UnityEngine;

//Defines what category the item belongs to
public enum ItemCategory
{
    Ore,
    Wood,
    Ingot,
    WeaponPart,
    Weapon
}

//Defines the condition of the weapon
public enum WeaponCondition
{
    NotApplicable,
    Dull,
    Sharp
}
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

    //Defines item category and weapon condition and make them accessible in the inspector
    public ItemCategory itemCategory;
    public WeaponCondition weaponCondition;
    public ItemData sharpenedVersion;
    public ItemData smeltedVersion;

}
