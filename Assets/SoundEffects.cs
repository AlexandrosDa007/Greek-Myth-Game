using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{

    private AudioSource audioSource;

    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    public void ChangeSoundEffectVolume(float volume)
    {
        AudioSource.volume = volume;
    }
}
