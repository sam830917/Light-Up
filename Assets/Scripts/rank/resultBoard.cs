using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultBoard : MonoBehaviour {
    //public Text easyTypeText;
    public Text easyUserNameText;
    public Text easyScoreText;
    public GameObject easyNoSaveDisplayText;

    //public Text normalTypeText;
    public Text normalUserNameText;
    public Text normalScoreText;
    public GameObject normalNoSaveDisplayText;

    //public Text hardTypeText;
    public Text hardUserNameText;
    public Text hardScoreText;
    public GameObject hardNoSaveDisplayText;

    // Use this for initialization
    void Start () {
        PlayerResultData playerResultData_easy = SaveOrLoadData.LoadData(SaveOrLoadData.SAVE_MODE_EASY);
        printResult(SaveOrLoadData.SAVE_MODE_EASY, playerResultData_easy, easyUserNameText, easyScoreText, easyNoSaveDisplayText);

        PlayerResultData playerResultData_normal = SaveOrLoadData.LoadData(SaveOrLoadData.SAVE_MODE_NORMAL);
        printResult(SaveOrLoadData.SAVE_MODE_NORMAL, playerResultData_normal, normalUserNameText, normalScoreText, normalNoSaveDisplayText);

        PlayerResultData playerResultData_hard = SaveOrLoadData.LoadData(SaveOrLoadData.SAVE_MODE_HARD);
        printResult(SaveOrLoadData.SAVE_MODE_HARD, playerResultData_hard, hardUserNameText, hardScoreText, hardNoSaveDisplayText);

    }

    private void printResult(string type, PlayerResultData playerResultData, Text userNameText, Text scoreText, GameObject noSaveDisplayText) {
        if (playerResultData.getUserName() != null)
        {
            //typeText.text = type;
            userNameText.text = playerResultData.getUserName();
            scoreText.text = playerResultData.getSpendTime() + " Sec.";
        }
        else
        {
            //typeText.gameObject.SetActive(false);
            userNameText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            noSaveDisplayText.SetActive(true);
            print("noSaveDisplayText");
        }
    }
}
