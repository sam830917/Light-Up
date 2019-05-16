using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonController : MonoBehaviour {
    public enum GoTo
    {
        Lobby, Rank, LevelSelect, Menu, Main, Quit
    }
    public GoTo changeSceneTo;

    public static string LOBBY_SCENE = "lobbyScene";
    public static string LEVEL_SELECT_SCENE = "levelSelectScene";
    public static string MENU_SCENE = "menuScene";
    public static string RANK_SCENE = "rankScene";
    public static string MAIN_SCENE = "mainScene";

    public static void activeSound(AudioSource audioData)
    {
        audioData.volume = settingsController.settings.getSound();
        audioData.Play(0);
    }

    public void onClickButtonToChangeScene() {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("isChangeScene");
        AudioSource audioData = GetComponent<AudioSource>();
        activeSound(audioData);
    }

    public void afterEndAnim() {
        switch (changeSceneTo) {
            case GoTo.LevelSelect:
                GoToLevelSelect();
                break;
            case GoTo.Rank:
                GoToRank();
                break;
            case GoTo.Lobby:
                GoToLobby();
                break;
            case GoTo.Menu:
                GoToMenu();
                break;
            case GoTo.Main:
                GoToMain();
                break;
            case GoTo.Quit:
                QuitGame();
                break;
        }
    }

    public void GoToMain()
    {
        gameMaster.gameStatus = gameMaster.GameStatus.GameStart;
        SceneManager.LoadScene(MAIN_SCENE);
    }

    public void GoToMenu()
    {
        gameMaster.gameStatus = gameMaster.GameStatus.Menu;
        SceneManager.LoadScene(MENU_SCENE);
    }

    public void GoToLevelSelect()
    {
        gameMaster.gameStatus = gameMaster.GameStatus.LevelSelect;
        SceneManager.LoadScene(LEVEL_SELECT_SCENE);
    }

    public void GoToLobby()
    {
        gameMaster.gameStatus = gameMaster.GameStatus.Lobby;
        SceneManager.LoadScene(LOBBY_SCENE);
    }

    public void GoToRank()
    {
        gameMaster.gameStatus = gameMaster.GameStatus.Rank;
        SceneManager.LoadScene(RANK_SCENE);
    }

    public void LevelSelectToPlay_Easy()
    {
        onClickButtonToChangeScene();
        gameMaster.length = 3;
        gameMaster.width = 3;
        gameMaster.gameMode = gameMaster.GameMode.Easy;
        gameMaster.gameStatus = gameMaster.GameStatus.GameStart;
    }

    public void LevelSelectToPlay_Normal()
    {
        onClickButtonToChangeScene();
        gameMaster.length = 5;
        gameMaster.width = 5;
        gameMaster.gameMode = gameMaster.GameMode.Normal;
        gameMaster.gameStatus = gameMaster.GameStatus.GameStart;
    }

    public void LevelSelectToPlay_Hard()
    {
        onClickButtonToChangeScene();
        gameMaster.length = 7;
        gameMaster.width = 7;
        gameMaster.gameMode = gameMaster.GameMode.Hard;
        gameMaster.gameStatus = gameMaster.GameStatus.GameStart;
    }

    public void LevelSelectToMenu()
    {
        onClickButtonToChangeScene();
        gameMaster.length = 5;
        gameMaster.width = 5;
        gameMaster.gameMode = gameMaster.GameMode.Custom;
        gameMaster.gameStatus = gameMaster.GameStatus.Menu;
    }

    public void QuitGame()
    {
        print("QUIT THE GAME");
        Application.Quit();
    }

    public void Cheating()
    {
        for (int i = 0; i < gameMaster.width; i++)
        {
            for (int j = 0; j < gameMaster.length; j++)
            {
                if (gameMaster.bulbMap[gameMaster.bulbArr[i, j]].getBulbStatus()==0) {
                    gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[i, j]], false);
                }
            }
        }
        gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[0, 0]], false);
        gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[0, 1]], false);
        gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[1, 0]], false);
    }

    public void DeleteSave()
    {
        SaveOrLoadData.deleteSaveData(SaveOrLoadData.SAVE_MODE_EASY);
        SaveOrLoadData.deleteSaveData(SaveOrLoadData.SAVE_MODE_NORMAL);
        SaveOrLoadData.deleteSaveData(SaveOrLoadData.SAVE_MODE_HARD);
    }

    public void onClickNextButtonFromTips()
    {
        Animator anim = GetComponent<Animator>();
        anim.ResetTrigger("Back");
        anim.SetTrigger("Next");
    }

    public void onClickBackButtonFromTips()
    {
        Animator anim = GetComponent<Animator>();
        anim.ResetTrigger("Next");
        anim.SetTrigger("Back");
    }
}
