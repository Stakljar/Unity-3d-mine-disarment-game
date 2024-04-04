using System;

[Serializable]
public class SettingsData
{
    float volume;
    int qualityLevel;
    int resolutionOptionIndex;
    float mouseSentivity;

    public SettingsData(float volume, int qualityLevel, int resolutionOptionIndex, float mouseSentivity)
    {
        this.volume = volume;
        this.qualityLevel = qualityLevel;
        this.resolutionOptionIndex = resolutionOptionIndex;
        this.mouseSentivity = mouseSentivity;
    }

    public float Volume
    {
        get => volume;
        set => volume = value;
    }

    public int QualityLevel
    {
        get => qualityLevel;
        set => qualityLevel = value;
    }

    public int ResolutionOptionIndex
    {
        get => resolutionOptionIndex;
        set => resolutionOptionIndex = value;
    }

    public float MouseSensitivity
    {
        get => mouseSentivity;
        set => mouseSentivity = value;
    }
}
