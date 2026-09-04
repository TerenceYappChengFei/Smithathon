using UnityEngine;
using UnityEngine.UI;

public class SettingsTabs : MonoBehaviour
{
    public GameObject audioContent;
    public GameObject miscContent;
    public GameObject creditsPanel;

    public Button audioButton;
    public Button miscButton;

    private void OnEnable()
    {
        creditsPanel.SetActive(false);
        ShowAudio();
    }

    public void ShowAudio()
    {
        audioContent.SetActive(true);
        miscContent.SetActive(false);

        audioButton.interactable = false;
        miscButton.interactable = true;
    }

    public void ShowMisc()
    {
        audioContent.SetActive(false);
        miscContent.SetActive(true);

        audioButton.interactable = true;
        miscButton.interactable = false;
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        creditsPanel.transform.SetAsLastSibling();
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }
}
