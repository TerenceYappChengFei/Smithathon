using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public Toggle muteAllToggle;

    private void Start()
    {
        masterVolumeSlider.value =
            PlayerPrefs.GetFloat(
                "MasterSliderValue",
                1f
            );

        musicVolumeSlider.value =
            PlayerPrefs.GetFloat(
                "MusicSliderValue",
                1f
            );

        sfxVolumeSlider.value =
            PlayerPrefs.GetFloat(
                "SFXSliderValue",
                1f
            );

        bool isMuted =
            PlayerPrefs.GetInt("MuteAll", 0) == 1;

        muteAllToggle.isOn = isMuted;

        ApplySavedVolumes();
    }

    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat(
            "MasterSliderValue",
            value
        );

        if (!muteAllToggle.isOn)
        {
            audioMixer.SetFloat(
                "MasterVolume",
                ConvertToDecibels(value)
            );
        }
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(
            "MusicVolume",
            ConvertToDecibels(value)
        );

        PlayerPrefs.SetFloat(
            "MusicSliderValue",
            value
        );
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(
            "SFXVolume",
            ConvertToDecibels(value)
        );

        PlayerPrefs.SetFloat(
            "SFXSliderValue",
            value
        );
    }

    public void SetMuteAll(bool isMuted)
    {
        if (isMuted)
        {
            audioMixer.SetFloat(
                "MasterVolume",
                -80f
            );

            PlayerPrefs.SetInt("MuteAll", 1);
        }
        else
        {
            audioMixer.SetFloat(
                "MasterVolume",
                ConvertToDecibels(
                    masterVolumeSlider.value
                )
            );

            PlayerPrefs.SetInt("MuteAll", 0);
        }
    }

    private void ApplySavedVolumes()
    {
        audioMixer.SetFloat(
            "MusicVolume",
            ConvertToDecibels(
                musicVolumeSlider.value
            )
        );

        audioMixer.SetFloat(
            "SFXVolume",
            ConvertToDecibels(
                sfxVolumeSlider.value
            )
        );

        if (muteAllToggle.isOn)
        {
            audioMixer.SetFloat(
                "MasterVolume",
                -80f
            );
        }
        else
        {
            audioMixer.SetFloat(
                "MasterVolume",
                ConvertToDecibels(
                    masterVolumeSlider.value
                )
            );
        }
    }

    private float ConvertToDecibels(float value)
    {
        value = Mathf.Max(value, 0.0001f);
        return Mathf.Log10(value) * 20f;
    }
}
