using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    void Start() {
        slider.value = PlayerPrefs.GetFloat("SystemVol");
    }

    public void SetLevel(float volumeValue) {
        float adjustedVolume = Mathf.Log10(volumeValue)*20;
        mixer.SetFloat("SystemVol", adjustedVolume);
        PlayerPrefs.SetFloat("SystemVol", volumeValue);
        PlayerPrefs.Save();
    }
}
