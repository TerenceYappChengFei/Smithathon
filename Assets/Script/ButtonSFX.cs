using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSFX : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

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
