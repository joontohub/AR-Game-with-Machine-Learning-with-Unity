using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRespawn : MonoBehaviour
{
    public GameObject[] Jewels;
    public GameObject[] ObsCrystal;
    public GameObject[] ObsTree;
    public GameObject[] Slopes;
    public GameObject[] Clouds;
    public GameObject[] Walls;
    public GameObject[] MineCarts;
    public GameObject[] Box;
    public GameObject empty;
    GameObject Obstacles;
  
    public GameObject Character;
    private void Start() {
        Obstacles = GameObject.FindWithTag("RespawnObstacle");
        RespawnToWorldIn3DGame();
        Invoke("CharacterRespawn", 1f);
    }
    public void RespawnToWorldIn3DGame()
    {

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
            instance.transform.parent = Obstacles.transform;
            instance.transform.localScale = instance.transform.localScale * 0.1f *0.8f;
            instance.transform.localPosition = instance.transform.localPosition * 0.1f * 0.8f;
        }
    }
    public void CharacterRespawn() 
    {
        Character.SetActive(true);
        Character.transform.position  = new Vector3 (0,0.5f,0);
    }
}
