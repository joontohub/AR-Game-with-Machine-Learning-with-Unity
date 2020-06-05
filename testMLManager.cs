using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMLManager : MonoBehaviour
{
    public static testMLManager instance;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else{
            return;
        }
    }
    private void Start() {
      
            Debug.Log(GameObject.FindGameObjectsWithTag("Jewel").Length);
    
    }
    public void GameEndChecker() 
    {
        if(GameObject.FindGameObjectsWithTag("Jewel").Length <= 0){
            GemCollectorAgent.instance.EndGame();
        }
        //another Case is 60 sec over
    }
    private void Update() {
        GameEndChecker();
    }
}
