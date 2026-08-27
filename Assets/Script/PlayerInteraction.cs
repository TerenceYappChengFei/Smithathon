using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private StationInteraction currentStation;
    public CanvasGroup interactButton; //for interact button behaviours


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
    }

    private void OnTriggerExit(Collider other)
    {
        StationInteraction station =
            other.GetComponent<StationInteraction>();

        if (station == currentStation)
        {
            currentStation = null;
            SetInteractButton(false);
            //Debug.Log("Station is no longer available");
        }
    }

    public void Interact()
    {
        if (currentStation != null)
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
