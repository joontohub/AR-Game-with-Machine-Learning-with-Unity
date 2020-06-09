using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{

    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;
    public GameObject Character;
    public GameObject Enemy;

    bool CharacterConveyorSwitch;
    bool EnemyConveyorSwitch;

    // Update is called once per frame
    void Update()
    {
       for(int i = 0; i <= onBelt.Count - 1; i++)
       {
           onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
       }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        if(onBelt.Find((x) => x.name == "Agent") == null && EnemyConveyorSwitch == false)
        {
            
            onBelt.Add(collision.gameObject);
            EnemyConveyorSwitch = true;
            
        }
        if(onBelt.Find((x) => x.name == "Character") == null && CharacterConveyorSwitch == false)
        {
            
            onBelt.Add(collision.gameObject);
            CharacterConveyorSwitch = true;
            Debug.Log(onBelt.Find((x) => x.name == "Character"));
        }
    }

    //When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        if(onBelt.Find((x) => x.name == "Agent") == Enemy || EnemyConveyorSwitch == true)
        {
            onBelt.Remove(collision.gameObject);
            EnemyConveyorSwitch = false;
        }
        if(onBelt.Find((x) => x.name == "Character") == Character || CharacterConveyorSwitch == true)
        {
            onBelt.Remove(collision.gameObject);
            CharacterConveyorSwitch = false;
        }
        
    }
}