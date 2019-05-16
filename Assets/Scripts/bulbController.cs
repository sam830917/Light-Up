using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulbController : MonoBehaviour {

    public void bulbOnclick()
    {
        if (gameMaster.gameStatus == gameMaster.GameStatus.GameStart) {
            Bulb bulb = gameMaster.bulbMap[this.gameObject];
            AudioSource audioData = GetComponent<AudioSource>();
            buttonController.activeSound(audioData);

            convertBulbOnOrOff(bulb.getPositionX(), bulb.getPositionY(), true);

            if (gameMaster.checkWin()) {
                gameMaster.gameStatus = gameMaster.GameStatus.GameOver;
                winActing();
            }
        }
    }

    public static void convertBulbOnOrOff(int x, int y, bool onAnim) {
        int length = gameMaster.length;
        int width = gameMaster.width;

        //bulb itself
        gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y]], onAnim);
        //Up Left bulb
        if (x == 0 && y == 0)
        {
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y + 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x + 1, y]], onAnim);
        }
        //Up Right bulb
        else if (x == 0 && y == width - 1)
        {
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y - 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x + 1, y]], onAnim);
        }
        //Up Mid bulb
        else if (x == 0)
        {
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y - 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y + 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x + 1, y]], onAnim);
        }
        //Botton Left bulb
        else if (x == length - 1 && y == 0)
        {
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y + 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x - 1, y]], onAnim);
        }
        //Botton Right bulb
        else if (x == length - 1 && y == width - 1)
        {
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y - 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x - 1, y]], onAnim);
        }
        //Botton Mid bulb
        else if (x == length - 1)
        {
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y - 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y + 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x - 1, y]], onAnim);
        }
        //Mid Left bulb
        else if (y == 0)
        {
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y + 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x - 1, y]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x + 1, y]], onAnim);
        }
        //Mid Right bulb
        else if (y == width - 1)
        {
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y - 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x - 1, y]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x + 1, y]], onAnim);
        }
        //Mid bulb
        else
        {
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y - 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x, y + 1]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x - 1, y]], onAnim);
            gameMaster.convertBulbStatus(gameMaster.bulbMap[gameMaster.bulbArr[x + 1, y]], onAnim);
        }
    }

    //after win acting
    public void winActing()
    {
        if (gameMaster.gameMode != gameMaster.GameMode.Custom)
        {
            string type = null;
            if (gameMaster.gameMode == gameMaster.GameMode.Easy)
                type = SaveOrLoadData.SAVE_MODE_EASY;
            else if (gameMaster.gameMode == gameMaster.GameMode.Normal)
                type = SaveOrLoadData.SAVE_MODE_NORMAL;
            else
                type = SaveOrLoadData.SAVE_MODE_HARD;

            PlayerResultData playerResultData = SaveOrLoadData.LoadData(type);
            int resultSec = int.Parse(gameMaster.printResultStr);
            print("resultSec---------->" + resultSec);
            //when break the record
            if (playerResultData.getUserName() == null || resultSec < playerResultData.getSpendTime())
            {
                gameMaster.winPanel.SetActive(true);
                AudioSource audioData = gameMaster.winPanel.GetComponent<AudioSource>();
                buttonController.activeSound(audioData);
                gameMaster.printResultText.text = gameMaster.printResultStr;
            }
            else
            {
                gameMaster.finishGamePanel.SetActive(true);
                AudioSource audioData = gameMaster.finishGamePanel.GetComponent<AudioSource>();
                buttonController.activeSound(audioData);
                gameMaster.printResultText2.text = gameMaster.printResultStr;
            }
        }
        else
        {
            gameMaster.finishGamePanel.SetActive(true);
            gameMaster.printResultText2.text = gameMaster.printResultStr;
        }
        gameMaster.pauseBtn.SetActive(false);
        gameMaster.tipsBtn.SetActive(false);
    }
}
