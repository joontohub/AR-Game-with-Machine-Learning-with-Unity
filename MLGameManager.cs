using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MLGameManager : MonoBehaviour
{
    public static MLGameManager instance;
    public GameObject EnemyAgent;
    public GameObject[] Jewels;
    public GameObject[] ObsCrystal;
    public GameObject[] ObsTree;
    public GameObject[] Slopes;
    public GameObject[] Clouds;
    public GameObject[] Walls;
    public GameObject[] MineCarts;
    public GameObject[] Box;

    public GameObject TestAvoidObj;


    public GameObject[] DefaultRandomizedObj;

    GameObject Obstacles;
    GameObject JewelParent;
    public List<Transform> ChildTransform;
    public GameObject Character;

    public GameObject[] TestJewel;

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
        Obstacles = GameObject.FindWithTag("RespawnObstacle");
        
        // when game staerts, Obstacles setactive true after random positioning.
        ChildTransform = new List<Transform>();
    }
    public void RandomPositioning()
    {
        //ClearReset(GameObject.FindGameObjectsWithTag("Jewel"));
        ClearReset(DefaultRandomizedObj);
        Randomize(DefaultRandomizedObj);
        SetOnReset(DefaultRandomizedObj);
        SetOnReset(GameObject.FindGameObjectsWithTag("Jewel"));
        TestJewelSetOn();
        //CharacterRandomize();
        //JewelRandomize();

    }
    private void Randomize(GameObject[] GameObjects)
    {
        foreach(GameObject obj in GameObjects)
        {
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),1,Random.Range(-38f,38f));
            
            //check this code in AR Env.
            obj.transform.position = RandomPos * 0.1f * 0.8f;


            Debug.Log("this is RandomPos" +    RandomPos);
            //obj.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            //obj.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }
    }
    public void JewelRandomize(){
        foreach(GameObject number in Jewels)
        {  
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),10f,Random.Range(-38f,38f));
            GameObject instance =  Instantiate(number,RandomPos,Quaternion.Euler(90,0,0)) as GameObject;
            instance.transform.parent = JewelParent.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        } 
    }
    public void CharacterRandomize(){
        Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),0f,Random.Range(-38f,38f));
        Character.transform.position = RandomPos;
        Character.transform.localPosition = Character.transform.localPosition * 0.1f * 0.8f;
    }


    public void EnvironmentReset() 
    {
        //GameManager.instance.TimeChecker();

        ClearReset(GameObject.FindGameObjectsWithTag("Jewel"));
        ClearReset(GameObject.FindGameObjectsWithTag("Crystal"));
        ClearReset(GameObject.FindGameObjectsWithTag("Slope"));
        ClearReset(GameObject.FindGameObjectsWithTag("Cloud"));
        ClearReset(GameObject.FindGameObjectsWithTag("Wall"));
        ClearReset(GameObject.FindGameObjectsWithTag("MineCart"));
        ClearReset(GameObject.FindGameObjectsWithTag("Chest"));
        

        EnemyAgent.SetActive(false);


        foreach(GameObject number in Clouds)
        {   
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),Random.Range(8f,20f),Random.Range(-38f,38f));
            GameObject instance =  Instantiate(number,RandomPos,Quaternion.Euler(0,0,Random.Range(0,360))) as GameObject;
            instance.transform.parent = Obstacles.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }
        foreach(GameObject number in ObsCrystal)
        {
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),1,Random.Range(-38f,38f));
            GameObject instance =  Instantiate(number,RandomPos,Quaternion.Euler(0,0,Random.Range(0,360))) as GameObject;
            instance.transform.parent = Obstacles.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }
        foreach(GameObject number in ObsTree)
        {
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),0,Random.Range(-38f,38f));
            GameObject instance =  Instantiate(number,RandomPos,Quaternion.Euler(0,Random.Range(0,360),0)) as GameObject;
            instance.transform.parent = Obstacles.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }
        foreach(GameObject number in Slopes)
        {
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),0,Random.Range(-38f,38f));
            GameObject instance =  Instantiate(number,RandomPos,Quaternion.Euler(90,0,Random.Range(0,360))) as GameObject;
            instance.transform.parent = Obstacles.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }
        foreach(GameObject number in Walls)
        {
            
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),0,Random.Range(-38f,38f));
            GameObject instance =  Instantiate(number,RandomPos,Quaternion.Euler(0,Random.Range(0,360),0)) as GameObject;
            instance.transform.parent = Obstacles.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }
        foreach(GameObject number in MineCarts)
        {
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),1,Random.Range(-38f,38f));
            GameObject instance =  Instantiate(number,RandomPos,Quaternion.Euler(-90,0,Random.Range(0,360))) as GameObject;
            instance.transform.parent = Obstacles.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }
        foreach(GameObject number in Box)
        {
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),0,Random.Range(-38f,38f));
            GameObject instance =  Instantiate(number,RandomPos,Quaternion.Euler(0,Random.Range(0,360),0)) as GameObject;
            instance.transform.parent = Obstacles.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }
        foreach(GameObject number in Jewels)
        {  
            Vector3 RandomPos = new Vector3(Random.Range(-38f,38f),10f,Random.Range(-38f,38f));
            GameObject instance =  Instantiate(number,RandomPos,Quaternion.Euler(90,0,0)) as GameObject;
            instance.transform.parent = JewelParent.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }  

        EnemyAgentReset();
    }
    public void EnemyAgentReset()
    {
        EnemyAgent.SetActive(true);
        EnemyAgent.transform.position  = new Vector3 (0,0.5f,0);
    }
    //public void GameEndChecker() 
    //{
    //    if(GameObject.FindGameObjectsWithTag("Jewel").Length <= 0){
    //        GemCollectorAgent.instance.EndGame();
    //    }
    //    //another Case is 60 sec over
    //}
    //private void Update() {
    //    GameEndChecker();
    //}
    public void ClearReset(GameObject[] gameObjects)
    {
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
    }
    public void SetOnReset(GameObject[] gameObjects)
    {
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        } 
    }
    public void TestJewelSetOn()
    {
        foreach(GameObject jewel in TestJewel)
        {
            jewel.SetActive(true);
        }
    }
}
