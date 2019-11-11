using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseWinInteract : MonoBehaviour
{
    public void back()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scene1") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scene2") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scene3"))
        {
            SceneManager.LoadScene("ChooseScene");
        }
    }

    public void Retry()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scene1"))
        {
            SceneManager.LoadScene("Scene1");
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scene2"))
        {
            SceneManager.LoadScene("Scene2");
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scene3"))
        {
            SceneManager.LoadScene("Scene3");
        }
    }
}
