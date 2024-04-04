using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class IOManager
{
    static string path = Application.persistentDataPath + "/settings.mdg";

    public static void SaveSettings(SettingsData settingsData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Create);
        binaryFormatter.Serialize(fileStream, settingsData);
        fileStream.Close();
    }

    public static SettingsData ReadSettings()
    {
        if(!File.Exists(path))
        {
            return new SettingsData(0, 2, Screen.resolutions.Length - 1, 100f);
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Open);
        SettingsData settingsData = (SettingsData) binaryFormatter.Deserialize(fileStream);
        fileStream.Close();
        return settingsData;
    }
}
