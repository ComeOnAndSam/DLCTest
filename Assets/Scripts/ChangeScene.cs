using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        //if (SceneManager.GetSceneByBuildIndex(1).IsValid())
        //{
        SceneManager.LoadScene("DLCScene");
       // }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
