using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour // From the class "Menu UI Loading"
{
    [SerializeField] private GameObject[] panels = null; // list of all the panels

    // Start is called before the first frame update
    IEnumerator Start() // in order to delay to start
    {
        yield return new WaitForFixedUpdate();
        PanelToggle(0); // default pannel
    }
    public void SavePrefs() // Save change of the slider(volume) 
    {
        PlayerPrefs.Save();
    }
    public void PanelToggle(int position) // enable a specific panel and select its default button
    {
        for (int i = 0; i < panels.Length; i++) //loop all panels in the list 
        {
            // the pannel will be enable or not
            panels[i].SetActive(position == i);
        }
    }
    public void Exit()
    {
        // exit the play mode from Editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
            Application.Quit();
        #endif
    }

}
