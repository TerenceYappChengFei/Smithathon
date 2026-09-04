using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Connects the settings UI to the AudioMixer and saves values with PlayerPrefs.
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

        ApplySavedVolumes(audioMixer);
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

    // Static allows audio preferences to load before the Settings panel is opened.
    public static void ApplySavedVolumes(AudioMixer mixer)
    {
        float masterVolume = PlayerPrefs.GetFloat(
            "MasterSliderValue",
            1f
        );

        float musicVolume = PlayerPrefs.GetFloat(
            "MusicSliderValue",
            1f
        );

        float sfxVolume = PlayerPrefs.GetFloat(
            "SFXSliderValue",
            1f
        );

        bool isMuted =
            PlayerPrefs.GetInt("MuteAll", 0) == 1;

        mixer.SetFloat(
            "MusicVolume",
            ConvertToDecibels(
                musicVolume
            )
        );

        mixer.SetFloat(
            "SFXVolume",
            ConvertToDecibels(
                sfxVolume
            )
        );

        if (isMuted)
        {
            mixer.SetFloat(
                "MasterVolume",
                -80f
            );
        }
        else
        {
            mixer.SetFloat(
                "MasterVolume",
                ConvertToDecibels(
                    masterVolume
                )
            );
        }
    }

    // Converts a Slider value from 0-1 into the decibels expected by AudioMixer.
    private static float ConvertToDecibels(float value)
    {
        value = Mathf.Max(value, 0.0001f);
        return Mathf.Log10(value) * 20f;
    }
}
