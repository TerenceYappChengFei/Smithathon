using UnityEngine;

public class StationInteraction : MonoBehaviour
{

    public GameObject stationPanel;

    public void OpenStation()
    {
        stationPanel.SetActive(true);
    }

    public void CloseStation()
    {
        stationPanel.SetActive(false);
    }
}
