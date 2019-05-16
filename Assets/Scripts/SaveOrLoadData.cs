using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

//refer from https://home.gamer.com.tw/creationDetail.php?sn=3562034
public class SaveOrLoadData{

    public const string SAVE_NAME = "PlayerResult";
    public static string SAVE_MODE_EASY = "Easy";
    public static string SAVE_MODE_NORMAL = "Normal";
    public static string SAVE_MODE_HARD = "Hard";
    public static string SAVE_SETTINGS = "Settings";

    public static string LAN_EN = "English";
    public static string LAN_CN = "繁體中文";

    public static PlayerResultData LoadData(string type) {
        PlayerResultData result = new PlayerResultData();

        if (PlayerPrefs.HasKey(type)) {
            string saveString = PlayerPrefs.GetString(type);

            try
            {
                saveString = System.Text.Encoding.Unicode.GetString(System.Convert.FromBase64String(saveString));
                XmlSerializer ser = new XmlSerializer(typeof(PlayerResultData));
                StringReader sr = new StringReader(saveString);
                result = ser.Deserialize(sr) as PlayerResultData;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                return new PlayerResultData();
            }
        }

        return result;
    }

    public static void SaveData(PlayerResultData saveData, string type) {
        XmlSerializer ser = new XmlSerializer(typeof(PlayerResultData));
        StringWriter sw = new StringWriter();
        ser.Serialize(sw, saveData);
        string saveString = sw.ToString();
        try
        {
            saveString = System.Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(saveString));
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        try
        {
            PlayerPrefs.SetString(type, saveString);
        }
        catch (System.Exception e)
        {
            Debug.Log("Got: " + e);
        }
        PlayerPrefs.Save();
        Debug.Log("---  Saving Succed... ---  \nStringLength = " + saveString.Length);
    }

    public static void deleteSaveData(string type) {
        PlayerPrefs.DeleteKey(type);
        Debug.Log("---  Already DeleteSaveData... ---");

    }

    public static Settings LoadSetting()
    {
        Settings result = new Settings();

        if (PlayerPrefs.HasKey(SAVE_SETTINGS))
        {
            string saveString = PlayerPrefs.GetString(SAVE_SETTINGS);

            try
            {
                saveString = System.Text.Encoding.Unicode.GetString(System.Convert.FromBase64String(saveString));
                XmlSerializer ser = new XmlSerializer(typeof(Settings));
                StringReader sr = new StringReader(saveString);
                result = ser.Deserialize(sr) as Settings;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                Settings newSettings = new Settings();
                newSettings.settings(100.0f, LocalizationManager.LocalizedText_en);
                SaveSetting(newSettings);
                return newSettings;
            }
        }

        return result;
    }

    public static void SaveSetting(Settings saveData)
    {
        XmlSerializer ser = new XmlSerializer(typeof(Settings));
        StringWriter sw = new StringWriter();
        ser.Serialize(sw, saveData);
        string saveString = sw.ToString();
        try
        {
            saveString = System.Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(saveString));
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        try
        {
            PlayerPrefs.SetString(SAVE_SETTINGS, saveString);
        }
        catch (System.Exception e)
        {
            Debug.Log("Got: " + e);
        }
        PlayerPrefs.Save();
        Debug.Log("---  Saving Succed... ---  \nStringLength = " + saveString.Length);
    }
}
