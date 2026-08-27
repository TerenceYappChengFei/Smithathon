using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private StationInteraction currentStation;
    public CanvasGroup interactButton; //for interact button behaviours
    private ScrapBinInteraction currentScrapBin;



    private void Start()
    {
        SetInteractButton(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        StationInteraction station =
            other.GetComponent<StationInteraction>();

        if (station != null)
        {
            currentStation = station;
            SetInteractButton(true);
            //Debug.Log("Station is available");
        }

        // if detected scrap bin, set currentScrapBin to that scrap bin and enable the interact button
        ScrapBinInteraction scrapBin =
            other.GetComponent<ScrapBinInteraction>();

        if (scrapBin != null)
        {
            currentScrapBin = scrapBin;
            SetInteractButton(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        StationInteraction station =
            other.GetComponent<StationInteraction>();

        if (station == currentStation)
        {
            currentStation = null;

            //SetInteractButton(false);
            SetInteractButton(currentScrapBin != null);

            //Debug.Log("Station is no longer available");
        }

        // if detected scrap bin, set currentScrapBin to null and disable the interact button
        ScrapBinInteraction scrapBin =
        other.GetComponent<ScrapBinInteraction>();

        if (scrapBin == currentScrapBin)
        {
            currentScrapBin = null;

            SetInteractButton(currentStation != null);
        }

    }

    public void Interact()
    {
        if (currentScrapBin != null)
        {
            currentScrapBin.DestroyHeldItem();
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

}
