using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    const string masterSoundVolumeName = "mixerVolume";

    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    TMP_Dropdown resolutionDropDown;

    [SerializeField]
    TMP_Dropdown qualityDropDown;

    [SerializeField]
    Slider soundSlider;

    [SerializeField]
    Slider mouseSensitivitySlider;

    Resolution[] resolutions;

    float currentVolume;
    int currentQualityLevel;
    int currentResolutionOptionIndex;
    float currentMouseSentivity;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> resolutionsAsString = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (!resolutionsAsString.Contains(resolutions[i].width + " x " + resolutions[i].height))
            {
                resolutionsAsString.Add(resolutions[i].width + " x " + resolutions[i].height);
            }
        }

        resolutionDropDown.AddOptions(resolutionsAsString);

        LoadSavedValues();
        LoadValuesIntoGame();
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
    }

    public void SetGraphicsQuality(int qualityLevel)
    {
        currentQualityLevel = qualityLevel;
    }

    public void SetResolution(int optionIndex)
    {
        currentResolutionOptionIndex = optionIndex;
    }

    public void SetMouseSensitivity(float mouseSensitivity)
    {
        currentMouseSentivity = mouseSensitivity;
    }

    public void SaveSettings()
    {
        SaveValues();
        LoadValuesIntoGame();
        gameObject.SetActive(false);
    }

    public void ExitSettings()
    {
        LoadSavedValues();
        LoadValuesIntoGame();
        gameObject.SetActive(false);
    }

    void LoadSavedValues()
    {
        SettingsData settingsData = IOManager.ReadSettings();
        currentVolume = settingsData.Volume;
        currentQualityLevel = settingsData.QualityLevel;
        currentResolutionOptionIndex = settingsData.ResolutionOptionIndex;
        currentMouseSentivity = settingsData.MouseSensitivity;
    }

    void LoadValuesIntoGame()
    {
        audioMixer.SetFloat(masterSoundVolumeName, currentVolume);
        QualitySettings.SetQualityLevel(currentQualityLevel, true);
        Resolution currentResolution = resolutions[currentResolutionOptionIndex];
        Screen.SetResolution(currentResolution.width, currentResolution.height, FullScreenMode.FullScreenWindow);

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            FindObjectOfType<Rotation>().MouseSensitivity = currentMouseSentivity;
        }

        resolutionDropDown.value = currentResolutionOptionIndex;
        resolutionDropDown.RefreshShownValue();

        qualityDropDown.value = currentQualityLevel;
        qualityDropDown.RefreshShownValue();

        soundSlider.value = currentVolume;
        mouseSensitivitySlider.value = currentMouseSentivity;
    }

    void SaveValues()
    {
        IOManager.SaveSettings(new SettingsData(currentVolume, currentQualityLevel, currentResolutionOptionIndex, currentMouseSentivity));
    }
}
