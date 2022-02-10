using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour // from https://www.youtube.com/watch?v=xRiqSmUggpg
{
    static string nextScene;

    [SerializeField] private Image loadingbar =null;
    [SerializeField] private Text txtPercent = null;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        async.allowSceneActivation = false; // not allow the loading scene yet in order to prevent the scene just flick because exectuting time is too fast

        float timer = 0;
        while (!async.isDone) // if the scene does not finish yet
        {          
            yield return null;
            txtPercent.text = ((loadingbar.fillAmount)* 100).ToString("F2") + " %";
            if (async.progress < 0.9f) // when the loading bar reaches right before 90 %
            {
                loadingbar.fillAmount = async.progress;
            }
            else // when the loading bar reaches over 90 %
            {
                timer += Time.unscaledDeltaTime;
                loadingbar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (loadingbar.fillAmount >= 1f)
                {
                    async.allowSceneActivation = true;
                    yield break;
                }

            }
        }
    }
}
