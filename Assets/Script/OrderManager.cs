using System.Collections;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public OrderDisplay[] orderSlots;
    public OrderData[] availableOrders;

    public float minimumSpawnDelay = 5f;
    public float maximumSpawnDelay = 10f;

    private void Start()
    {
        ClearAllOrders();
        StartCoroutine(GenerateOrders());
    }

    private void ClearAllOrders()
    {
        for (int i = 0; i < orderSlots.Length; i++)
        {
            orderSlots[i].ClearOrder();
        }
    }

    private IEnumerator GenerateOrders()
    {
        while (true)
        {
            float delay = Random.Range(
                minimumSpawnDelay,
                maximumSpawnDelay
            );

            yield return new WaitForSeconds(delay);

            TryGenerateOrder();
        }
    }

    private void TryGenerateOrder()
    {
        OrderDisplay emptySlot = FindEmptySlot();

        if (emptySlot == null)
        {
            return;
        }

        if (availableOrders.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(
            0,
            availableOrders.Length
        );

        OrderData selectedOrder =
            availableOrders[randomIndex];

        emptySlot.ShowOrder(
            selectedOrder.requestedWeapon,
            selectedOrder.requiredMaterial1,
            selectedOrder.requiredMaterial2,
            selectedOrder.patienceDuration
        );
    }

    private OrderDisplay FindEmptySlot()
    {
        for (int i = 0; i < orderSlots.Length; i++)
        {
            if (!orderSlots[i].hasOrder)
            {
                return orderSlots[i];
            }
        }

        return null;
    }
}
