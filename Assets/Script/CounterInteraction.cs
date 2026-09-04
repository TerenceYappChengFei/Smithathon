using UnityEngine;

// Submits the selected weapon to the order counter.
public class CounterInteraction : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public OrderManager orderManager;

    // A match completes an order; a mismatch fails the earliest active order.
    public void SubmitHeldItem()
    {
        ItemData heldItem =
            playerInventory.GetSelectedItem();

        if (heldItem == null)
        {
            Debug.Log("The selected inventory slot is empty");
            return;
        }

        bool orderCompleted =
            orderManager.SubmitOrder(heldItem);

        if (orderCompleted)
        {
            playerInventory.RemoveSelectedItem();

            if (SFXManager.instance != null)
            {
                SFXManager.instance.PlaySubmitCorrect();
            }


            Debug.Log(
                heldItem.itemName +
                " was successfully delivered"
            );
        }
        else
        {
            bool orderFailed =
                orderManager.FailEarliestOrder();

            if (orderFailed)
            {
                playerInventory.RemoveSelectedItem();

                if (SFXManager.instance != null)
                {
                    SFXManager.instance.PlaySubmitWrong();
                }


                Debug.Log(
                    heldItem.itemName +
                    " was incorrect. The earliest order failed."
                );
            }
            else
            {
                Debug.Log(
                    "There are no active orders to fail"
                );
            }
        }

    }
}
