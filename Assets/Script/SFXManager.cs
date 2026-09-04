using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioSource sfxSource;
    public AudioMixerGroup sfxMixerGroup;

    public AudioClip buttonPress;
    public AudioClip crafting;
    public AudioClip gameOver;
    public AudioClip grinding;
    public AudioClip order;
    public AudioClip smelting;
    public AudioClip smeltingDone;
    public AudioClip smithing;
    public AudioClip submitCorrect;
    public AudioClip submitWrong;
    public AudioClip titleDrop;

    private void Awake()
    {
        if (instance != null &&
            instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (sfxSource == null)
        {
            sfxSource =
                gameObject.AddComponent<AudioSource>();
        }

        sfxSource.playOnAwake = false;
        sfxSource.loop = false;
        sfxSource.spatialBlend = 0f;
        sfxSource.outputAudioMixerGroup =
            sfxMixerGroup;
    }

    private void PlaySound(AudioClip sound)
    {
        if (sound == null)
        {
            return;
        }

        sfxSource.PlayOneShot(sound);
    }

    public void PlayButtonPress()
    {
        PlaySound(buttonPress);
    }

    public void PlayCrafting()
    {
        PlaySound(crafting);
    }

    public void PlayGameOver()
    {
        PlaySound(gameOver);
    }

    public void PlayGrinding()
    {
        PlaySound(grinding);
    }

    public void PlayOrder()
    {
        PlaySound(order);
    }

    public void PlaySmelting()
    {
        PlaySound(smelting);
    }

    public void PlaySmeltingDone()
    {
        PlaySound(smeltingDone);
    }

    public void PlaySmithing()
    {
        PlaySound(smithing);
    }

    public void PlaySubmitCorrect()
    {
        PlaySound(submitCorrect);
    }

    public void PlaySubmitWrong()
    {
        PlaySound(submitWrong);
    }

    public void PlayTitleDrop()
    {
        PlaySound(titleDrop);
    }
}
