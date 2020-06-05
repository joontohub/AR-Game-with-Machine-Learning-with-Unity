using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{

    public AudioSource audioSrc;

    private float musicVolume = 1f;
    public Slider slider;
    private bool onoffValue;
    public GameObject toggleButton;
    void Update()
    {
        audioSrc.volume = musicVolume;
    }

    public void ControlVolume(float vol)
    {
        
        musicVolume = vol;
        DataVariables.SoundVolume = vol;
    }
    //toggle
    public static bool isVibOn = true;

    public void OnChangeValue()
    {
        onoffValue = toggleButton.GetComponent<Toggle>().isOn;
        if (onoffValue)
        {
            Debug.Log("Vibration on");
            isVibOn = true;
            DataVariables.isVibrateOn = true;
            Debug.Log("vibe on");
        }
        else
        {
            Debug.Log("Vibration off");
            isVibOn = false;
            DataVariables.isVibrateOn = false;
            Debug.Log("vibe off");
        }
    }

}