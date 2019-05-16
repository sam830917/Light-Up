using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class settingsController : MonoBehaviour {
    public static AudioSource audioSource = null;
    public static Settings settings = null;

    public Slider soundSlider;
    public Dropdown languageDropDown;
    public GameObject settingsBorad;

    // Use this for initialization
    void Start () {
        settings = SaveOrLoadData.LoadSetting();
        if (soundController.bgm != null)
        {
            audioSource = soundController.bgm.GetComponent<AudioSource>();
            audioSource.volume = settings.getSound();
            soundSlider.value = settings.getSound();
        }
        if (LocalizationManager.LocalizedText_en.Equals(settings.getLanguage()))
        {
            languageDropDown.value = 0;
        }
        else if (LocalizationManager.LocalizedText_cn.Equals(settings.getLanguage()))
        {
            languageDropDown.value = 1;
        }
    }

    public void onChangeSoundSlider() {
        float soundValue = soundSlider.value;
        audioSource.volume = soundValue;
    }

    public void onClickBackFromSettings()
    {
        print(languageDropDown.captionText.text);
        string languageStr;
        if (SaveOrLoadData.LAN_EN.Equals(languageDropDown.captionText.text))
        {
            languageStr = LocalizationManager.LocalizedText_en;
            LocalizationManager.instance.LoadLocalizedText(LocalizationManager.LocalizedText_en + ".json");
        }
        else if (SaveOrLoadData.LAN_CN.Equals(languageDropDown.captionText.text))
        {
            languageStr = LocalizationManager.LocalizedText_cn;
            LocalizationManager.instance.LoadLocalizedText(LocalizationManager.LocalizedText_cn + ".json");
        }
        else
        {
            languageStr = LocalizationManager.LocalizedText_en;
            LocalizationManager.instance.LoadLocalizedText(LocalizationManager.LocalizedText_en + ".json");
        }
        float soundValue = soundSlider.value;
        Settings settings = SaveOrLoadData.LoadSetting();
        settings.settings(soundValue, languageStr);
        SaveOrLoadData.SaveSetting(settings);
        settingsBorad.SetActive(false);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void onClickSettings()
    {
        AudioSource audioData = GetComponent<AudioSource>();
        buttonController.activeSound(audioData);
        settingsBorad.SetActive(true);
    }
}
