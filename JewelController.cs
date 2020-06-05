using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelController : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private bool PlaneStat;
    private JewelController thisObj;
    private float y;
    private MeshCollider meshCollider;
    private void Start() 
    {
        meshCollider = this.GetComponent<MeshCollider>();
        rigidbody = this.GetComponent<Rigidbody>();
        if(rigidbody)
        {
            Debug.Log("rigid on");
        }
        else{
            Debug.Log("No rigid");
        }
        Invoke("OffGravity",0.5f);
    }
    private void OffGravity()
    {
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        meshCollider.isTrigger = true;
    }
    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag== "map" || other.collider.tag =="slope" || other.collider.tag =="mineCart")
        {
            Debug.Log(other);
            Debug.Log("this is Collision");
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;

            if(y < 0)
            {
                y = 0.3f;
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "map")
        {
            Debug.Log(other);
            Debug.Log("this is Trigger");
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;

            if(y < 0)
            {
                
                y = 0.3f;
            }
        }
    }
    void FixedUpdate()
    {
        Debug.Log(y);
        transform.Rotate(new Vector3(0f,0f,30f) * Time.deltaTime);
    }
    
}
