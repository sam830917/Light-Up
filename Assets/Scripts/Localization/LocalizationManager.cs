using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

//refer from https://unity3d.com/learn/tutorials/topics/scripting/localized-text-component
public class LocalizationManager : MonoBehaviour
{
    //public Text test;
    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    public static string missingTextString = "Localized text not found";

    public static string LocalizedText_en = "LocalizedText_en";
    public static string LocalizedText_cn = "LocalizedText_cn";

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            string language = SaveOrLoadData.LoadSetting().getLanguage() + ".json";
            LoadLocalizedText(language);
            //print(language);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        WWW wWA = new WWW(filePath);
        while (!wWA.isDone) { }
        filePath = wWA.text;
        print(filePath);

        if (filePath!=null)
        {
            string dataAsJson = filePath;
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;

    }

    public bool GetIsReady()
    {
        return isReady;
    }

}