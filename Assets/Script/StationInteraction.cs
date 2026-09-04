using UnityEngine;

// Generic station component for opening and closing a linked UI panel.
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
