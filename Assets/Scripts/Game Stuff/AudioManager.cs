using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource effectsSource;
    public AudioSource musicSource;

    private void Start()
    {
        musicSource.ignoreListenerPause = true;
    }

    public void PlaySoundEffect(AudioClip effectClip)
    {
        if (effectsSource != null)
        {
            // use PlayOneShot so i can play multiple clips at once without cutting off the previous played one
            effectsSource.PlayOneShot(effectClip);
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource != null)
        {
            musicSource.clip = musicClip;
            musicSource.Play();
        }
    }
}