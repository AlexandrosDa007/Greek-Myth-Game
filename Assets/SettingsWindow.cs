using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsWindow : MonoBehaviour
{
    public AudioClip soundEffect;
    public GameObject musicSlider;
    public GameObject soundEffectsSlider;
    void Awake() {
        musicSlider.GetComponent<UnityEngine.UI.Slider>().value = GameObject.FindGameObjectWithTag("musicSource").GetComponent<Music>().AudioSource.volume;
        soundEffectsSlider.GetComponent<UnityEngine.UI.Slider>().value = GameObject.FindGameObjectWithTag("soundEffects").GetComponent<SoundEffects>().AudioSource.volume;
    }


    public void ChangeMusicVolume()
    {
        GameObject.FindGameObjectWithTag("musicSource").GetComponent<Music>().ChangeVolume(musicSlider.GetComponent<UnityEngine.UI.Slider>().value);
    }

    public void ChangeSoundEffectsVolume()
    {
        SoundEffects soundEffects = GameObject.FindGameObjectWithTag("soundEffects").GetComponent<SoundEffects>();

        soundEffects.ChangeSoundEffectVolume(soundEffectsSlider.GetComponent<UnityEngine.UI.Slider>().value);
        soundEffects.PlaySoundEffect(soundEffect);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
