using UnityEngine;
using UnityEngine.UI;

public class OrderDisplay : MonoBehaviour
{
    public Image weaponIcon;
    public Image material1Icon;
    public Image material2Icon;
    public Slider patienceBar;

    public bool hasOrder;

    public ItemData requestedWeapon;
    public ItemData requiredMaterial1;
    public ItemData requiredMaterial2;

    private float patienceRemaining;
    private float patienceDuration;

    private void Update()
    {
        if (!hasOrder)
        {
            return;
        }

        patienceRemaining -= Time.deltaTime;

        patienceBar.value =
            patienceRemaining / patienceDuration * 100f;

        if (patienceRemaining <= 0f)
        {
            ClearOrder();
        }
    }

    public void ShowOrder(
        ItemData weapon,
        ItemData material1,
        ItemData material2,
        float duration
    )
    {
        requestedWeapon = weapon;
        requiredMaterial1 = material1;
        requiredMaterial2 = material2;

        weaponIcon.sprite = weapon.itemIcon;
        material1Icon.sprite = material1.itemIcon;
        material2Icon.sprite = material2.itemIcon;

        patienceDuration = duration;
        patienceRemaining = duration;

        patienceBar.value = 100f;
        hasOrder = true;

        gameObject.SetActive(true);
    }

    public void ClearOrder()
    {
        hasOrder = false;

        requestedWeapon = null;
        requiredMaterial1 = null;
        requiredMaterial2 = null;

        gameObject.SetActive(false);
    }
}
