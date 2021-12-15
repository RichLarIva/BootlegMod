using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resDropdown;

    public TMP_Dropdown graphicDropdown;

    public Slider volSlider;

    public Toggle fullScreenToggle;

    Resolution[] resolutions;

    const string resName = "resolutionOption";
    const string qualName = "qualityValue";

    private void Awake()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; ++i)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        int fullScreenInt = PlayerPrefs.GetInt("IsFullScreen");

        if (fullScreenInt == 1)
        {
            fullScreenToggle.isOn = true;
        }
        else if (fullScreenInt == 0)
            fullScreenToggle.isOn = false;

        resDropdown.AddOptions(options);
        resDropdown.value = PlayerPrefs.GetInt(resName, currentResIndex);
        resDropdown.RefreshShownValue();

        graphicDropdown.value = PlayerPrefs.GetInt(qualName, 3);

        resDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(resName, resDropdown.value);
            PlayerPrefs.Save();
        }));

        graphicDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(qualName, graphicDropdown.value);
            PlayerPrefs.Save();
        }));

        SetVolume(PlayerPrefs.GetFloat("Volume"));
        volSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Volume"));
    }

    private void Update()
    {

    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Volume"));
        
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if(isFullscreen == true)
        {
            PlayerPrefs.SetInt("IsFullScreen", 1);
        }
        else if(isFullscreen == false)
        {
            PlayerPrefs.SetInt("IsFullScreen", 0);
        }
    }



    
}
