using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource _audioSource;

    
    public AudioSource AudioSource { get => _audioSource; set => _audioSource = value; }
    

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        AudioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (AudioSource.isPlaying) return;
        AudioSource.Play();
    }

    public void StopMusic()
    {
        AudioSource.Stop();
    }

   
    public void ChangeVolume(float volume)
    {
        AudioSource.volume = volume;
    }

   
}