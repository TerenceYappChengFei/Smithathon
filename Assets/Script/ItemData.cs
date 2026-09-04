using UnityEngine;

//Defines what category the item belongs to
// Categories let stations decide which types of items they accept.
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


// A reusable asset containing one item's icon, category, and crafting links.
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

    //The weapon heads that an ingot can become at the anvil
    public ItemData swordHeadVersion;
    public ItemData axeHeadVersion;
    public ItemData spearHeadVersion;

    //These are only used by weapon heads at the workbench
    public ItemData requiredAssemblyItem;
    public ItemData assembledVersion;

}
