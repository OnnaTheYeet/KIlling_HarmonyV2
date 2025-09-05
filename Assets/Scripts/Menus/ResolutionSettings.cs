using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionSettings : MonoBehaviour
{
    [Header("UI References")]
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] availableResolutions;
    private List<string> resolutionOptions = new List<string>();
    private int currentResolutionIndex = 0;

    void Start()
    {
        availableResolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();
        
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string option = availableResolutions[i].width + " x "
                          + availableResolutions[i].height
                          + " @ " + availableResolutions[i].refreshRate + "Hz";

            resolutionOptions.Add(option);
            
            if (availableResolutions[i].width == Screen.currentResolution.width &&
                availableResolutions[i].height == Screen.currentResolution.height &&
                availableResolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(resolutionOptions);
        
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
        resolutionDropdown.onValueChanged.AddListener(delegate {
            SetResolution(resolutionDropdown.value);
        });
        
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    /// <summary>
    /// Apply the selected resolution from the dropdown.
    /// </summary>
    /// <param name="resolutionIndex"></param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution chosenResolution = availableResolutions[resolutionIndex];
        
        bool isFullscreen = fullscreenToggle.isOn;
        
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, isFullscreen);
    }

    /// <summary>
    /// Toggle fullscreen mode on or off.
    /// </summary>
    /// <param name="isFullscreen"></param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        
        SetResolution(resolutionDropdown.value);
    }
}

