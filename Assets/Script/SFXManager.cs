using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioSource sfxSource;
    public AudioMixerGroup sfxMixerGroup;
    public AudioSource smeltingSource;
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

        if (smeltingSource == null)
        {
            smeltingSource =
                gameObject.AddComponent<AudioSource>();
        }

        smeltingSource.playOnAwake = false;
        smeltingSource.loop = true;
        smeltingSource.spatialBlend = 0f;
        smeltingSource.outputAudioMixerGroup =
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
        if (smelting == null)
        {
            return;
        }

        smeltingSource.clip = smelting;
        smeltingSource.Play();
    }

    public void StopSmelting()
    {
        if (smeltingSource == null)
        {
            return;
        }

        smeltingSource.Stop();
        smeltingSource.clip = null;
    }

    //stops the sound when the game is paused
    public void PauseSmelting()
    {
        if (smeltingSource != null &&
            smeltingSource.isPlaying)
        {
            smeltingSource.Pause();
        }
    }
    //resumes the sound when the game is resumed
    public void ResumeSmelting()
    {
        if (smeltingSource != null &&
            smeltingSource.clip != null &&
            !smeltingSource.isPlaying)
        {
            smeltingSource.UnPause();
        }
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
