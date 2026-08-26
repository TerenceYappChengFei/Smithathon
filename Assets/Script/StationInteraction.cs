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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
