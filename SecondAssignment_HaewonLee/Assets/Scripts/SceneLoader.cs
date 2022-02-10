using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering; // SplashScreen
using UnityEngine.SceneManagement; 

public class SceneLoader : MonoBehaviour // From the class "Menu UI Loading"
{
    private AsyncOperation async; // store scenes 
    public void BtnLoadScene() // check if there is Async
    {
        if (async != null) return;
        Scene currentScene = SceneManager.GetActiveScene();
        async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
    }
    public void BtnLoadScene(int i) // load a scene by number
    {
        if (async != null) return;
        if (i==2)
        {
            LoadingScene.LoadScene("ForChristmas");
        }
        else if (i==1)
        {
            LoadingScene.LoadScene("MainMenu");
        }
    }
    public void BtnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
