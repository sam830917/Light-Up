using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {
    private Text thisText;
    private float temp;

    // Use this for initialization
    void Start () {
        thisText = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameMaster.gameStatus == gameMaster.GameStatus.GameStart) {
            temp += Time.deltaTime;
            thisText.text = Math.Floor(temp).ToString();
            gameMaster.printResultStr = Math.Floor(temp).ToString();
        }
    }
}
