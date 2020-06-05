using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DailyReward : MonoBehaviour
{

    public string sDate = "";

    public bool delete;

    public Text coinText;

    public List<int> rewardCoin;

    public List<Button> rewardButton;

    public GameObject dailyPanel;

    private void Start()
    {
        if (delete)
            PlayerPrefs.DeleteAll();

        coinText.text = PlayerPrefs.GetInt("Coin").ToString();

        sDate = DateTime.Now.ToString("yyyy-MM-dd");
        Debug.Log(sDate);
    }
    

    public void DailyCheck()
    {
        string dateOld = PlayerPrefs.GetString("PlayDateOld");
        
        if (string.IsNullOrEmpty(dateOld))
        {
            Debug.Log("First Game");
            Debug.Log("First Reward");

            rewardButton[0].interactable = true;

            PlayerPrefs.SetString("PlayDateOld", sDate);
            PlayerPrefs.SetInt("PlayGameCount", 1);
        }
        else
        {
            //Load present time from device
            DateTime _dateNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

            //Load Saved time in the past
            DateTime _dateOld = Convert.ToDateTime(dateOld);

            TimeSpan diff = _dateNow.Subtract(_dateOld);

            if((diff.Days>=1) && (diff.Days < 2))
            {
                int gameCount = PlayerPrefs.GetInt("PlayGameCount");
                if (gameCount == 1)
                {
                    rewardButton[1].interactable = true;
                    PlayerPrefs.SetInt("PlayGameCount", 2);
                    
                }
                else if(gameCount == 2)
                {
                    rewardButton[2].interactable = true;
                    PlayerPrefs.SetInt("PlayGameCount", 3);
                }
                else if(gameCount == 3)
                {
                    rewardButton[3].interactable = true;
                    PlayerPrefs.SetInt("PlayGameCount", 4);
                }
                else if (gameCount == 4)
                {
                    rewardButton[4].interactable = true;
                    PlayerPrefs.SetInt("PlayGameCount", 5);
                }
                else if (gameCount == 5)
                {
                    rewardButton[5].interactable = true;
                    PlayerPrefs.SetInt("PlayGameCount", 6);
                }
                else if (gameCount == 6)
                {
                    rewardButton[6].interactable = true;
                    PlayerPrefs.SetInt("PlayGameCount", 7);
                }
                else if (gameCount == 7)
                {
                    rewardButton[0].interactable = true;
                    PlayerPrefs.SetInt("PlayGameCount", 1);
                }
                Debug.Log("Other Days");

                //Save time reward given
                PlayerPrefs.SetString("PlayDateOld", _dateNow.ToString());
            }
            else if(diff.Days >= 2)
            {
                rewardButton[0].interactable = true;
                PlayerPrefs.SetInt("PlayGameCount", 1);
                PlayerPrefs.SetString("PlayDateOld", _dateNow.ToString());
            }
        }
    }

    public void Reward(int count)
    {
        int coin = PlayerPrefs.GetInt("Coin");
        coin += rewardCoin[count];
        DataVariables.characterPoint += rewardCoin[count];
        PlayerPrefs.SetInt("Coin", coin);
        coinText.text = coin.ToString();

        Button clickButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        clickButton.interactable = false;
    }

    public void CloseButton()
    {
        dailyPanel.SetActive(false);
    }
}