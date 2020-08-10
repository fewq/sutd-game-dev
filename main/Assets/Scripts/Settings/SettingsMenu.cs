using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public CustomInputManager customInputManager;
    public Slider volSlider;
    void OnEnable()
    {
        volSlider.value = customInputManager.Volume;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
        customInputManager.Volume = volume;
    }
}
