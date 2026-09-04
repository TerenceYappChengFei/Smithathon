using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceInteraction : MonoBehaviour
{
    public PlayerInventory playerInventory;

    [Header("Smelting")]
    public float smeltingDuration = 5f;
    public GameObject timerCanvas;
    public Slider smeltingSlider;
    public ParticleSystem smeltingParticles;

    private ItemData storedOre;
    private ItemData finishedIngot;

    private bool isSmelting;
    private bool ingotIsReady;

    private Coroutine smeltingCoroutine;

    private void Start()
    {
        timerCanvas.SetActive(false);
        smeltingSlider.value = 0f;

        smeltingParticles.Stop(
            true,
            ParticleSystemStopBehavior.StopEmittingAndClear
        );
    }

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

    private IEnumerator SmeltingTimer()
    {
        float elapsedTime = 0f;

        while (elapsedTime < smeltingDuration)
        {
            elapsedTime += Time.deltaTime;

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

    private void OnDestroy()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.StopSmelting();
        }
    }

}
