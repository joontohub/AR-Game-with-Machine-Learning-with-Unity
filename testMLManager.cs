using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMLManager : MonoBehaviour
{
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
