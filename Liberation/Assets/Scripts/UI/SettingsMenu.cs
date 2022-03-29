using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{

    //Fields
    private Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    // Find supported resolutions for the player's device
    void Start() {
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray();
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;

        // Convert arraylist into a list of strings to output in the UI dropdown
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);


            // Find the default resolution of a user's device
            if (resolutions[i].width.Equals(Screen.width) && resolutions[i].height.Equals(Screen.height)) {
                currentResolutionIndex = i;
            }
            
        }
        
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Allow user to set their resolution through a UI dropdown in settings
    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Allow user to set the graphics quality of the game. Graphical specifications can be found in unity project settings.
    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Allow user to use fullscreen or windowed through the UI toggle option in settings.
    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
    
}
