using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingTest : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown ResDropdown;
    
    void Start() {
        resolutions = Screen.resolutions;
        ResDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }

        ResDropdown.AddOptions(options);
        ResDropdown.value = currentResolutionIndex;
        ResDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality (int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log(qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
        Debug.Log(isFullScreen);
    }
}
