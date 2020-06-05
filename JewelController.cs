using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelController : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private bool PlaneStat;
    private void Start() 
    {
        rigidbody = this.GetComponent<Rigidbody>();
        if(rigidbody)
        {
            Debug.Log("rigid on");
        }
        else{
            Debug.Log("No rigid");
        }
        rigidbody.useGravity = false;
        
    }
    void Update()
    {
        transform.Rotate(new Vector3(0f,0f,30f) * Time.deltaTime);
        if(transform.position.y <= 0.3f * 0.1f * 0.8f)
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }
   
    }
    
}
