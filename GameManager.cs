using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : BaseController
{
    public GameObject CharacterObj;
    public delegate void DeadChecker(bool stat);
    public static event DeadChecker DeadActivation;

    public static GameManager instance;
    int ClickCount;
    private void Awake() {
        if(!instance)
        {
            instance = this;
            Debug.Log("Game Start Now");
        }
    }

    public float reSpawnTime = 3f;
    public float reSpawnRestTime ;

    private bool activeRespawnEnumSwitch;

    private float runTime;
    private float runFillGauge = 1f;
    public Image runFillImage;
    public GameObject runBotton;
    
    public Text ScorePan;
    public GameObject RespawnRestTimePan;
    public Text reSpawnText;

    public Text TimeText;
    private int GameTime = 60;
    public ParticleSystem respawnParc;

    private void Start() {
        DeadActivation += ReSpawnCharacter;
        reSpawnRestTime = reSpawnTime;
        GameTime = 60;
        //TimeChecker(); // 학습환경에서 제외. 실제 게임환경에서 실행
    }

    public void TimeChecker()
    {
        GameTime = 60;
        StartCoroutine(DownTime());
    }
    IEnumerator DownTime()
    {
        while(GameTime > 0)
        {
            GameTime -= 1;
            yield return new WaitForSeconds(1);
        }
        GemCollectorAgent.instance.EndEpisode();
        // 학습환경에서 키기
    }
    private void TimePanelUpdate() 
    {
        TimeText.text = "Time : " + GameTime.ToString();
    }
    private void CharacterScoreTextUpdate()
    {
        ScorePan.text = "Score : " + DataVariables.characterScore.ToString();
    }
    private void Update() {
        TimePanelUpdate();
        CharacterScoreTextUpdate();
        RunChecker();
        RunGaugeUpdate();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);
        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");
            SaveQuitGame();
        }
    }
    public void RunSwitchOn()
    {
        CharacterController.runSwitch = true;
    }
    private void RunChecker()
    {
        if(CharacterController.runSwitch)
        {
            runBotton.SetActive(false);
            runFillGauge -= Time.deltaTime / 10;
            if(runFillGauge <= 0 )
            {
                CharacterController.runSwitch = false;
            }
        }
        else
        {
            if(runFillGauge <= 1)
            {
                runFillGauge += Time.deltaTime / 5;
            }
            if(runFillGauge >= 1)
            {
                runBotton.SetActive(true);
            }
        }
    }
    private void RunGaugeUpdate()
    {
        runFillImage.fillAmount = runFillGauge;
    }
    public void ReSpawnCharacter(bool stat)
    {
        if(stat)
        {
            activeRespawnEnumSwitch = true;
            if(CharacterController.deathTriggerInt ==1 )
            {
                StartCoroutine("RespawnTimeDown");
            }
            CharacterObj.transform.position = new Vector3(0,0,0);
            respawnParc.Play();
            Debug.Log("character is dead");
        }
        else
        {
            return;
        }
    }
    public void CharacterRespawnTextUpdate()
    {
        reSpawnText.text = "Time : \n"  + Mathf.Round(reSpawnRestTime).ToString();
    }
    IEnumerator RespawnTimeDown()
    {
        reSpawnRestTime = reSpawnTime;
        while(activeRespawnEnumSwitch)
        {
            RespawnRestTimePan.SetActive(true);
            if(RespawnRestTimePan)
            {
                Debug.Log("text update");
                CharacterRespawnTextUpdate();
            }
            if(reSpawnRestTime <= 0f )
            {
                CharacterController.isDead = false;
                reSpawnRestTime = reSpawnTime;
                RespawnRestTimePan.SetActive(false);
                activeRespawnEnumSwitch = false;
                CharacterController.MoveSwitch = true;
                CharacterController.deathTriggerInt = 0;
                break;
            }
            else
            {
                Debug.Log("minus 1");
                reSpawnRestTime -= 1f;
                Debug.Log(reSpawnRestTime);
                Debug.Log("this is rest time");
            }
            yield return new WaitForSeconds(1f);
        }
    }

    
}
