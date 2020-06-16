using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMLManager : MonoBehaviour
{
    public static testMLManager instance;
    public GameObject[] Jewels;
    GameObject JewelParent;
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
        JewelParent = GameObject.Find("Jewels");
    
    }
    public void testResetEnv(){
        ClearReset(GameObject.FindGameObjectsWithTag("Jewel"));
        foreach(GameObject number in Jewels)
        {  
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),10f,Random.Range(-38f,38f));
            GameObject instance =  Instantiate(number,RandomPos,Quaternion.Euler(90,0,0)) as GameObject;
            instance.transform.parent = JewelParent.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }  
    }
    public void GameEndChecker() 
    {
        if(GameObject.FindGameObjectsWithTag("Jewel").Length <= 0){
            //GemCollectorAgent.instance.EndGame();
        }
        //another Case is 60 sec over
    }
    private void Update() {
        GameEndChecker();
    }
    public void ClearReset(GameObject[] gameObjects)
    {
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}
