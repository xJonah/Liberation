using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;

    // If the user does not have a saved music volume, set the volume to 1
    void Start() {
        if (!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", 0);
            Load();
        }
        else {
            Load();
        }
    }

    // Change volume through a UI slider in settings
    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    // Set the volume to the user's saved volume
    private void Load() {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    // Save a user's set volume
    private void Save() {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
