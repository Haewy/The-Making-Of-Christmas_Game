using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Slider))]

public class SetVol : MonoBehaviour // From the class "Menu UI Loading"
{
    [SerializeField] private AudioMixer audioMixer = null; // reference to AudioMixer
    [SerializeField] private string parmeterName = null; // the name of the Exposed parameter in AudioMixer
    private Slider slider; // reference to Slider

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(parmeterName, 0);
        slider.value = v; // slider's default position 
        audioMixer.SetFloat(parmeterName, v); // change AudioMixer value by v (default postion)
    }
    public void SetVolume(float vol) // update the AudioMixer when the slider(volume) changes
    {
        audioMixer.SetFloat(parmeterName, vol); //
        slider.value = vol;
        PlayerPrefs.SetFloat(parmeterName, vol); // save changed value
    }

}
