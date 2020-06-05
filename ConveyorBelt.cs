using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{

    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;

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
        onBelt.Add(collision.gameObject);
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}