using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    private void FixedUpdate() {
        Vector3 CameraPos = new Vector3(target.transform.position.x + offsetX
                                        ,target.transform.position.y + offsetY
                                        ,target.transform.position.z + offsetZ);
        transform.position  = CameraPos;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
