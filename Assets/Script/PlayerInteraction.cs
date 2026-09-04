using UnityEngine;

// Detects the station in front of the player and routes the Interact button.
public class PlayerInteraction : MonoBehaviour
{
    private StationInteraction currentStation; //for detection of which station player is facing
    public CanvasGroup interactButton; //for interact button behaviours
    private ScrapBinInteraction currentScrapBin; //for scrapping iems
    private GrindstoneInteraction currentGrindstone; //for sharpening mechanic
    private FurnaceInteraction currentFurnace; //for smelting mechanic
    private WorkbenchInteraction currentWorkbench; //for assembling weapons
    private CounterInteraction currentCounter; //for submitting orders





    private void Start()
    {
        SetInteractButton(false);
    }

    // Saves references when compatible stations enter the forward trigger.
    private void OnTriggerEnter(Collider other)
    {
        StationInteraction station =
            other.GetComponent<StationInteraction>();

        if (station != null)
        {
            currentStation = station;
            //Debug.Log("Station is available");
        }

        // if detected scrap bin, updates detection and enables corresponding interact function
        ScrapBinInteraction scrapBin =
            other.GetComponent<ScrapBinInteraction>();

        if (scrapBin != null)
        {
            currentScrapBin = scrapBin;
        }

        //if detected grindstone, updates detection and enables corresponding interact function 
        GrindstoneInteraction grindstone =
            other.GetComponent<GrindstoneInteraction>();

        if (grindstone != null)
        {
            currentGrindstone = grindstone;
        }
        //if detected grindsotne, updates detection and enables corresponding interaction
        FurnaceInteraction furnace =
            other.GetComponent<FurnaceInteraction>();

        if (furnace != null)
        {
            currentFurnace = furnace;
        }

        WorkbenchInteraction workbench =
            other.GetComponent<WorkbenchInteraction>();

        if (workbench != null)
        {
            currentWorkbench = workbench;
        }

        CounterInteraction counter =
    other.GetComponent<CounterInteraction>();

        if (counter != null)
        {
            currentCounter = counter;
        }


        UpdateInteractButton();

    }

    private void OnTriggerExit(Collider other)
    {
        StationInteraction station =
            other.GetComponent<StationInteraction>();

        if (station == currentStation)
        {
            currentStation = null;

            //Debug.Log("Station is no longer available");
        }

        // Disables scrap bin interact fucntion when walk away
        ScrapBinInteraction scrapBin =
            other.GetComponent<ScrapBinInteraction>();

        if (scrapBin == currentScrapBin)
        {
            currentScrapBin = null;
        }
        //Disables grindstone interact function when walk away
        GrindstoneInteraction grindstone =
            other.GetComponent<GrindstoneInteraction>();

        if (grindstone == currentGrindstone)
        {
            currentGrindstone = null;
        }

        //Disables furnace interact function when walk away
        FurnaceInteraction furnace =
    other.GetComponent<FurnaceInteraction>();

        if (furnace == currentFurnace)
        {
            currentFurnace = null;
        }

        WorkbenchInteraction workbench =
            other.GetComponent<WorkbenchInteraction>();

        if (workbench == currentWorkbench)
        {
            currentWorkbench = null;
        }

        CounterInteraction counter =
    other.GetComponent<CounterInteraction>();

        if (counter == currentCounter)
        {
            currentCounter = null;
        }


        UpdateInteractButton();

    }

    // The else-if order ensures one button press uses only one station.
    public void Interact()
    {
        if (currentScrapBin != null)
        {
            currentScrapBin.DestroyHeldItem();
        }
        else if (currentGrindstone != null)
        {
            currentGrindstone.SharpenHeldWeapon();
        }
        else if (currentFurnace != null)
        {
            currentFurnace.SmeltHeldItem();
        }
        else if (currentWorkbench != null)
        {
            currentWorkbench.AssembleHeldItems();
        }
        else if (currentCounter != null)
        {
            currentCounter.SubmitHeldItem();
        }
        else if (currentStation != null)
        {
            currentStation.OpenStation();
        }
    }



    private void SetInteractButton(bool available)
    {
        if (available)
        {
            interactButton.alpha = 1f;
            interactButton.interactable = true;
            interactButton.blocksRaycasts = true;
        }
        else
        {
            interactButton.alpha = 0.3f;
            interactButton.interactable = false;
            interactButton.blocksRaycasts = false;
        }
    }

    //Enables interact button when approaching stations
    private void UpdateInteractButton()
    {
        bool interactionAvailable =
            currentStation != null ||
            currentScrapBin != null ||
            currentGrindstone != null ||
            currentFurnace != null ||
            currentWorkbench != null ||
            currentCounter != null;

        SetInteractButton(interactionAvailable);
    }

}
