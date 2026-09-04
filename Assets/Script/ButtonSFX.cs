using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
// Plays the shared button sound whenever the attached UI Button is pressed.
public class ButtonSFX : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    // Subscribe while enabled and unsubscribe later to prevent duplicate listeners.
    private void OnEnable()
    {
        button.onClick.AddListener(
            PlayButtonSound
        );
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(
            PlayButtonSound
        );
    }

    private void PlayButtonSound()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayButtonPress();
        }
    }
}
