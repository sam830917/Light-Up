//refer from https://unity3d.com/learn/tutorials/topics/scripting/localized-text-component

[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}