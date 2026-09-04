using UnityEngine;
using UnityEngine.Audio;

public class PersistentMusic : MonoBehaviour
{
    public static PersistentMusic instance;

    public AudioSource musicSource;
    public AudioClip mainMusic;
    public AudioClip gameOverMusic;
    public AudioMixerGroup musicMixerGroup;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            instance.PlayMainMusic();
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
            musicSource.spatialBlend = 0f;
        }

        musicSource.outputAudioMixerGroup = musicMixerGroup;
    }

    private void Start()
    {
        if (musicMixerGroup != null)
        {
            AudioSettings.ApplySavedVolumes(
                musicMixerGroup.audioMixer
            );
        }

        PlayMainMusic();
    }


    public void PlayMainMusic()
    {
        if (musicSource.clip == mainMusic && musicSource.isPlaying)
        {
            return;
        }

        musicSource.Stop();
        musicSource.clip = mainMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayGameOverMusic()
    {
        musicSource.Stop();
        musicSource.clip = gameOverMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}
