using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundController : MonoBehaviour {
    public GameObject bgmObj;
    public static GameObject bgm = null;

    // Use this for initialization
    void Start () {
        //When first into Lobby, create a bgm.
        if (gameMaster.gameStatus == gameMaster.GameStatus.Lobby) {
            bgm = GameObject.FindGameObjectWithTag("Sound");
            if (bgm == null)
            {
                bgm = (GameObject)Instantiate(bgmObj);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
