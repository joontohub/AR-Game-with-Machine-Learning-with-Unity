using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{

    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;
    public GameObject Character;
    public GameObject Base_Agent;
    public GameObject Avoid_Agent;
    public GameObject Attack_Agent;
    bool CharacterConveyorSwitch;
    bool BaseAgentConveyorSwitch;
    bool AvoidAgentConveyorSwitch;
    bool AttackAgentConveyorSwitch;

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
        if(onBelt.Find((x) => x.name == "Avoid_Agent") == null && AvoidAgentConveyorSwitch == false)
        {
            
            onBelt.Add(collision.gameObject);
            BaseAgentConveyorSwitch = true;
            
        }
        if(onBelt.Find((x) => x.name == "Base_Agent") == null && BaseAgentConveyorSwitch == false)
        {
            
            onBelt.Add(collision.gameObject);
            BaseAgentConveyorSwitch = true;
            
        }
        if(onBelt.Find((x) => x.name == "Attack_Agent") == null && AttackAgentConveyorSwitch == false)
        {
            
            onBelt.Add(collision.gameObject);
            BaseAgentConveyorSwitch = true;
            
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
        if(onBelt.Find((x) => x.name == "Avoid_Agent") == Avoid_Agent || AvoidAgentConveyorSwitch == true)
        {
            onBelt.Remove(collision.gameObject);
            BaseAgentConveyorSwitch = false;
        }
        if(onBelt.Find((x) => x.name == "Base_Agent") == Base_Agent || BaseAgentConveyorSwitch == true)
        {
            onBelt.Remove(collision.gameObject);
            BaseAgentConveyorSwitch = false;
        }
        if(onBelt.Find((x) => x.name == "Attack_Agent") == Attack_Agent || AttackAgentConveyorSwitch == true)
        {
            onBelt.Remove(collision.gameObject);
            BaseAgentConveyorSwitch = false;
        }
        if(onBelt.Find((x) => x.name == "Character") == Character || CharacterConveyorSwitch == true)
        {
            onBelt.Remove(collision.gameObject);
            CharacterConveyorSwitch = false;
        }
        
    }
 
}