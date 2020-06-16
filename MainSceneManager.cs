using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class MainSceneManager : BaseController
{
    public GameObject OptionPanel;
    public GameObject StorePanel;
    public static bool isVibOn = true;
    public Text pointText;
    public GameObject BellyDancer;
    public GameObject Character;
    public void OutGame()
    {
        Application.Quit();
    }
    public void StoreOpen()
    {
        BellyDancer.SetActive(false);
        Character.SetActive(true);
    }
    private void Start() {
        BaseController.instance.Load();
        if(DataVariables.characterPoint <= 10)
        {
            DataVariables.characterPoint = 1000;
        }
        CharacterController.AttackGameSwitch = false;
        CharacterController.AvoidGameSwitch = false;
    }
    public void AttackSceneChanger()
    {
        CharacterController.AttackGameSwitch = true;
        SceneManager.LoadScene("AttackGameScene");
    }
    
    public void GameSceneChanger()
    {
        CharacterController.AvoidGameSwitch = true;
        SceneManager.LoadScene("BaseGameScene");
    }
    public void StoreOpenButtonControl()
    {
        if(StorePanel.activeSelf)
        {
            StorePanel.SetActive(false);
        }
        else{
            StorePanel.SetActive(true);
        }
    }
    public void OptionButtonControl()
    {
        if(OptionPanel.activeSelf)
        {
            OptionPanel.SetActive(false);
        }
        else
        {
            OptionPanel.SetActive(true);
        }
    }
    public void OnChangeValue()
    {
        bool onoffValue = gameObject.GetComponent<Toggle>().isOn;
        if (onoffValue)
        {
            Debug.Log("Vibration on");
            isVibOn = true;
        }
        else
        {
            Debug.Log("Vibration off");
            isVibOn = false;
        }
    }
    public void PointTextUpdate()
    {
        pointText.text = DataVariables.characterPoint.ToString();
    }
    private void Update() {
        PointTextUpdate();
    }
}
