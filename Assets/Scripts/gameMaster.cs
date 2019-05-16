using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameMaster : MonoBehaviour {
    public enum GameStatus
    {
        Lobby, Rank, LevelSelect, Menu, GameStart, GamePause, GameOver
    }
    public enum GameMode
    {
        Easy, Normal, Hard, Custom
    }
    public GameObject bulbPrefab;
    public GameObject bulbGroupGM;
    public Text typeText;
    public GameObject winPanelObj;
    public GameObject finishGamePanelObj;
    public Text printResult;//for break result Text
    public Text printResult2;//for just win a game
    public InputField nameInput;
    public GameObject typeErrorText;
    public GameObject pauseMenu;
    public GameObject tipsMenu;
    public GameObject pauseButton;
    public GameObject tipsButton;
    public Sprite starBtnLight;
    public Sprite starBtnDark;

    public static GameStatus gameStatus = GameStatus.Lobby;
    public static GameObject pauseBtn;
    public static GameObject tipsBtn;
    public static GameMode gameMode = GameMode.Normal;
    public static Text printResultText;
    public static Text printResultText2;
    public static string printResultStr;
    public static Sprite starButtonLight;
    public static Sprite starButtonDark;

    public static GameObject[,] bulbArr = new GameObject[7, 7];
    public static Dictionary<GameObject, Bulb> bulbMap = new Dictionary<GameObject, Bulb>();
    public static int length = 4;
    public static int width = 4;
    public static GameObject winPanel;
    public static GameObject finishGamePanel;

    private void initObj(){
        printResultText = printResult;
        printResultText2 = printResult2;
        winPanel = winPanelObj;
        finishGamePanel = finishGamePanelObj;
        starButtonLight = starBtnLight;
        starButtonDark = starBtnDark;
        pauseBtn = pauseButton;
        tipsBtn = tipsButton;
    }

    // Use this for initialization
    void Start () {
        initObj();
        //set status
        gameStatus = GameStatus.GameStart;

        typeText.text = length + "x" + width;
        int x = 0;
        int y = 0;
        bulbGroupGM.transform.localPosition = new Vector3(-(width - 1) * 20, length * 10, 0);
        //start to create the bulbs
        for (int i = 0; i < length; i++)
        {
            for(int j = 0; j < width; j++)
            {
                GameObject bulbItem = Instantiate(bulbPrefab);
                bulbItem.transform.SetParent(bulbGroupGM.transform);
                bulbItem.transform.localPosition = new Vector3(x, y, 0);
                bulbItem.transform.localScale = new Vector3(1, 1, 1);
                bulbItem.name = "bulb" + i + " " + j;

                //create a Bulb Obj
                Bulb bulbObj = bulbItem.AddComponent<Bulb>();
                bulbObj.setBulbStatus(0);
                bulbObj.setBulbGameObj(bulbItem);
                bulbObj.setPositionX(i);
                bulbObj.setPositionY(j);
                bulbMap.Add(bulbItem, bulbObj);
                bulbArr[i,j] = bulbItem;

                x += 40;
            }
            x = 0;
            y -= 40;
        }

        //random to light up the bulbs
        int thirdOfBulbsNumber = (width * length) / 3;
        int index = 0;
        bool isValid = false;
        while (!isValid) {
            while(index < thirdOfBulbsNumber)
            {
                int ranWidth = Random.Range(0, width);
                int ranLength = Random.Range(0, length);
                bulbController.convertBulbOnOrOff(ranWidth, ranLength, false);
                index++;
            }
            if (!checkWin()) {
                isValid = true;
            }
        }
    }

    //change bulb status  (light to dark, dark to light)
    public static void convertBulbStatus(Bulb bulb, bool onAnim)
    {
        //onAnim = true, then active the animator
        if(onAnim)
            bulb.getBulbGameObj().GetComponent<Animator>().SetTrigger("onClick");
        if (bulb.getBulbStatus() == 0)
        {
            bulb.setBulbStatus(1);
            //bulb.getBulbGameObj().GetComponent<Image>().color = Color.yellow;
            bulb.getBulbGameObj().GetComponent<Image>().sprite = starButtonLight;
        }
        else if (bulb.getBulbStatus() == 1)
        {
            bulb.setBulbStatus(0);
            //bulb.getBulbGameObj().GetComponent<Image>().color = Color.white;
            bulb.getBulbGameObj().GetComponent<Image>().sprite = starButtonDark;
        }
    }

    //check whether player win the game
    public static bool checkWin() {
        bool isWin = true;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (bulbMap[bulbArr[i, j]].getBulbStatus() == 0)
                {
                    isWin = false;
                }
            }
        }
        return isWin;
    }

    //press save button
    public void OnClickSaveButton() {
        string inputText = nameInput.text;
        //If none of inputing name, print error.
        if (inputText.Equals(""))
        {
            typeErrorText.SetActive(true);
        }
        else
        {
            typeErrorText.SetActive(false);

            PlayerResultData playerResultData = new PlayerResultData();
            playerResultData.playerResultData(inputText, int.Parse(printResultStr));
            if(gameMode == GameMode.Easy)
                SaveOrLoadData.SaveData(playerResultData, SaveOrLoadData.SAVE_MODE_EASY);
            else if(gameMode == GameMode.Normal)
                SaveOrLoadData.SaveData(playerResultData, SaveOrLoadData.SAVE_MODE_NORMAL);
            else if (gameMode == GameMode.Hard)
                SaveOrLoadData.SaveData(playerResultData, SaveOrLoadData.SAVE_MODE_HARD);

            gameStatus = GameStatus.Rank;
            SceneManager.LoadScene(buttonController.RANK_SCENE);
        }
    }

    //press to pause
    public void OnClickPauseButton()
    {
        pauseButton.SetActive(false);
        tipsButton.SetActive(false);
        pauseMenu.SetActive(true);
        gameStatus = GameStatus.GamePause;
    }

    //press to back the game
    public void OnClicBackButtonFromPauseMenu()
    {
        pauseButton.SetActive(true);
        tipsButton.SetActive(true);
        pauseMenu.SetActive(false);
        gameStatus = GameStatus.GameStart;
    }

    //press to tips
    public void OnClickTipsButton()
    {
        pauseButton.SetActive(false);
        tipsButton.SetActive(false);
        tipsMenu.SetActive(true);
        bulbGroupGM.SetActive(false);
        gameStatus = GameStatus.GamePause;
    }

    //press to back the game
    public void OnClicBackButtonFromTipsMenu()
    {
        pauseButton.SetActive(true);
        tipsButton.SetActive(true);
        tipsMenu.SetActive(false);
        bulbGroupGM.SetActive(true);
        gameStatus = GameStatus.GameStart;
    }
}
