using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//refer from https://unity3d.com/learn/tutorials/topics/scripting/localized-text-component
public class LocalizedText : MonoBehaviour
{

    public string key;

    // Use this for initialization
    void Start()
    {
        Text text = GetComponent<Text>();
        string getText = LocalizationManager.instance.GetLocalizedValue(key);
        if (LocalizationManager.missingTextString != getText)
            text.text = getText;
    }

}