using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//refer from https://unity3d.com/learn/tutorials/topics/scripting/localized-text-component
public class StartupManager : MonoBehaviour
{

    // Use this for initialization
    private IEnumerator Start()
    {
        while (!LocalizationManager.instance.GetIsReady())
        {
            yield return null;
        }

        SceneManager.LoadScene(buttonController.LOBBY_SCENE);
    }

}