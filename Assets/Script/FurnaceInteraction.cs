using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Controls the furnace interaction.
// The furnace can be empty, smelting an ore, or holding a finished ingot.
public class FurnaceInteraction : MonoBehaviour
{
    // Lets the furnace remove items from and return items to the player.
    public PlayerInventory playerInventory;

    [Header("Smelting")]
    public float smeltingDuration = 5f;
    public GameObject timerCanvas;
    public Slider smeltingSlider;
    public ParticleSystem smeltingParticles;

    private ItemData storedOre; // The raw ore currently inside the furnace.
    private ItemData finishedIngot; // The result created after smelting.

    private bool isSmelting;
    private bool ingotIsReady;

    // Keeping this reference lets us cancel the timer when ore is removed early.
    private Coroutine smeltingCoroutine;

    // Resets the furnace visuals when the scene begins.
    private void Start()
    {
        timerCanvas.SetActive(false);
        smeltingSlider.value = 0f;

        smeltingParticles.Stop(
            true,
            ParticleSystemStopBehavior.StopEmittingAndClear
        );
    }

    // Keeps the looping furnace sound in sync with paused gameplay.
    private void Update()
    {
        if (!isSmelting ||
            SFXManager.instance == null)
        {
            return;
        }

        if (Time.timeScale == 0f)
        {
            SFXManager.instance.PauseSmelting();
        }
        else
        {
            SFXManager.instance.ResumeSmelting();
        }
    }


    // Called by PlayerInteraction. The result depends on the furnace's state.
    public void SmeltHeldItem()
    {
        if (isSmelting)
        {
            ReturnStoredOre();
        }
        else if (ingotIsReady)
        {
            CollectFinishedIngot();
        }
        else
        {
            StartSmelting();
        }
    }

    // Validates the selected item, stores it, and starts the timer.
    private void StartSmelting()
    {
        ItemData heldItem =
            playerInventory.GetSelectedItem();

        if (heldItem == null)
        {
            Debug.Log("The selected slot is empty");
            return;
        }

        if (heldItem.itemCategory != ItemCategory.Ore)
        {
            Debug.Log(heldItem.itemName + " is not ore");
            return;
        }

        if (heldItem.smeltedVersion == null)
        {
            Debug.Log(
                heldItem.itemName +
                " does not have a smelted version"
            );

            return;
        }

        // The item is moved out of the hotbar and stored inside the furnace.
        storedOre = heldItem;
        finishedIngot = heldItem.smeltedVersion;

        playerInventory.RemoveSelectedItem();

        isSmelting = true;
        ingotIsReady = false;

        timerCanvas.SetActive(true);
        smeltingSlider.value = 0f;
        smeltingParticles.Play();

        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlaySmelting();
        }

        smeltingCoroutine =
            StartCoroutine(SmeltingTimer());
    }

    // A Coroutine lets the timer progress across many frames.
    private IEnumerator SmeltingTimer()
    {
        float elapsedTime = 0f;

        while (elapsedTime < smeltingDuration)
        {
            elapsedTime += Time.deltaTime;

            // This division converts progress into a Slider value from 0 to 1.
            smeltingSlider.value =
                elapsedTime / smeltingDuration;

            yield return null;
        }

        smeltingSlider.value = 1f;

        isSmelting = false;
        ingotIsReady = true;
        smeltingCoroutine = null;

        smeltingParticles.Stop(
            true,
            ParticleSystemStopBehavior.StopEmittingAndClear
        );

        if (SFXManager.instance != null)
        {
            SFXManager.instance.StopSmelting();
            SFXManager.instance.PlaySmeltingDone();
        }


        Debug.Log(
            finishedIngot.itemName +
            " is ready to collect"
        );
    }

    // Cancels smelting and returns the raw ore if an inventory slot is free.
    private void ReturnStoredOre()
    {
        bool itemReturned =
            playerInventory.AddItem(storedOre);

        if (!itemReturned)
        {
            Debug.Log(
                "Inventory is full. Ore remains in the furnace."
            );

            return;
        }

        if (smeltingCoroutine != null)
        {
            StopCoroutine(smeltingCoroutine);
            smeltingCoroutine = null;
        }

        storedOre = null;
        finishedIngot = null;

        isSmelting = false;
        ingotIsReady = false;

        timerCanvas.SetActive(false);
        smeltingSlider.value = 0f;

        smeltingParticles.Stop(
            true,
            ParticleSystemStopBehavior.StopEmittingAndClear
        );

        if (SFXManager.instance != null)
        {
            SFXManager.instance.StopSmelting();
        }

        Debug.Log("The ore was removed from the furnace");
    }

    // Gives the completed ingot to the player if an inventory slot is free.
    private void CollectFinishedIngot()
    {
        bool itemCollected =
            playerInventory.AddItem(finishedIngot);

        if (!itemCollected)
        {
            Debug.Log(
                "Inventory is full. Ingot remains in the furnace."
            );

            return;
        }

        Debug.Log(
            finishedIngot.itemName +
            " was collected"
        );

        storedOre = null;
        finishedIngot = null;

        ingotIsReady = false;

        timerCanvas.SetActive(false);
        smeltingSlider.value = 0f;
    }

    // Stops persistent furnace audio when restarting or changing scenes.
    private void OnDestroy()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.StopSmelting();
        }
    }

}
