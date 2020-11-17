using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControl : MonoBehaviour
{

    public AudioClip clip;

    void Start() {
        gameObject.GetComponent<UnityEngine.UI.Slider>().value = GameObject.FindGameObjectWithTag("musicSource").GetComponent<Music>().AudioSource.volume;
    }

    public void ChangeMusicVolume()
    {
        float volume = gameObject.GetComponent<UnityEngine.UI.Slider>().value;
        GameObject.FindGameObjectWithTag("musicSource").GetComponent<Music>().ChangeVolume(volume);
    }

    public void ChangeSoundEffectsVolume()
    {
        float volume = gameObject.GetComponent<UnityEngine.UI.Slider>().value;
        GameObject.FindGameObjectWithTag("soundEffects").GetComponent<SoundEffects>().ChangeSoundEffectVolume(volume);
        GameObject.FindGameObjectWithTag("soundEffects").GetComponent<SoundEffects>().PlaySoundEffect(clip);
    }
}
