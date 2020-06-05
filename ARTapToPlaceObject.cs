using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject gameOjbectToInstantiate;
    private GameObject spawnedObject;
    private ARRaycastManager aRRaycastManager;
    private Vector2 touchPosition;
    private bool MadeSwitch;
    ARPlaneManager aRPlaneManager;
    ARPointCloudManager aRPointCloudManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private void Awake() {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }
    private void Start() {
        aRPointCloudManager = GetComponent<ARPointCloudManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }
    bool TryGetTouchPosition (out Vector2 touchPosition)
    {
        if(Input.touchCount >0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;

    }

    // Update is called once per frame
    void Update()
    {
        if(!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
        if(aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(spawnedObject == null && MadeSwitch == false)
            {
                spawnedObject = Instantiate(gameOjbectToInstantiate,hitPose.position,hitPose.rotation);
                MadeSwitch = true;
                aRPlaneManager.enabled = false;
                foreach (var plane in aRPlaneManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                aRPointCloudManager.enabled = false; 
                foreach (var Point in aRPointCloudManager.trackables) 
                {
                Point.gameObject.SetActive(false);
                }
            }
            else{
                //spawnedObject.transform.position = hitPose.position;
            }
        }
    }
}
