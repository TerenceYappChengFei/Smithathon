using UnityEngine;

[CreateAssetMenu(
    fileName = "NewOrder",
    menuName = "Smithathon/Order"
)]
// A reusable order asset containing its weapon, materials, and patience time.
public class OrderData : ScriptableObject
{
    public ItemData requestedWeapon;
    public ItemData requiredMaterial1;
    public ItemData requiredMaterial2;

    public float patienceDuration = 30f;
}
