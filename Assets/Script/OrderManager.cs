using System.Collections;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public OrderDisplay[] orderSlots;
    public OrderData[] availableOrders;
    public GameProgressManager gameProgressManager;
    private float patienceMultiplier = 1f;



    public float minimumSpawnDelay = 5f;
    public float maximumSpawnDelay = 10f;
    private int nextOrderNumber = 1;


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

        emptySlot.orderNumber = nextOrderNumber;
        nextOrderNumber++;

        emptySlot.gameProgressManager =
        gameProgressManager;

        emptySlot.ShowOrder(
            selectedOrder.requestedWeapon,
            selectedOrder.requiredMaterial1,
            selectedOrder.requiredMaterial2,
            selectedOrder.patienceDuration *
    patienceMultiplier

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

    //Checks for valid order submission according to weapon held, if valid, clears order and returns true, else returns false
    public bool SubmitOrder(ItemData submittedItem)
    {
        OrderDisplay earliestMatchingOrder = null;

        for (int i = 0; i < orderSlots.Length; i++)
        {
            OrderDisplay order = orderSlots[i];

            if (order.hasOrder &&
                !order.isResolving &&
                order.requestedWeapon == submittedItem)
            {
                if (earliestMatchingOrder == null ||
                    order.orderNumber < earliestMatchingOrder.orderNumber)
                {
                    earliestMatchingOrder = order;
                }
            }
        }

        if (earliestMatchingOrder == null)
        {
            return false;
        }

        earliestMatchingOrder.CompleteOrder();
        return true;
    }

    //Finds the earliest active order and fails it
    public bool FailEarliestOrder()
    {
        OrderDisplay earliestOrder = null;

        for (int i = 0; i < orderSlots.Length; i++)
        {
            OrderDisplay order = orderSlots[i];

            if (order.hasOrder &&
                !order.isResolving)
            {
                if (earliestOrder == null ||
                    order.orderNumber < earliestOrder.orderNumber)
                {
                    earliestOrder = order;
                }
            }
        }

        if (earliestOrder == null)
        {
            return false;
        }

        earliestOrder.FailOrder(FailureReason.WrongOrder);
        return true;
    }

    public void SetDifficulty(int difficultyLevel)
    {
        if (difficultyLevel == 1)
        {
            minimumSpawnDelay = 5f;
            maximumSpawnDelay = 10f;
            patienceMultiplier = 1f;
        }
        else if (difficultyLevel == 2)
        {
            minimumSpawnDelay = 4f;
            maximumSpawnDelay = 8f;
            patienceMultiplier = 0.9f;
        }
        else if (difficultyLevel == 3)
        {
            minimumSpawnDelay = 3f;
            maximumSpawnDelay = 6f;
            patienceMultiplier = 0.8f;
        }
        else
        {
            minimumSpawnDelay = 2f;
            maximumSpawnDelay = 5f;
            patienceMultiplier = 0.7f;
        }
    }

}
