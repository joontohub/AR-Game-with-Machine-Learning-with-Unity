using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager aRRaycastManager;
    public GameObject spawnPrefab;
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    
    bool isActive = false;
    private void Awake() {
        //이벤트 함수
        //적용 후 구독시켜놓음. 
        //변할 때마다 참조해서 들어감. 

        //ar 라이프사이클 페이지를 알 수 있음. 지원여부, 지연, 변경여부 등등 알 수 있음. 
        ARSession.stateChanged += OnStateChanged;
    }

    //임의의 이벤트함수의 아규먼트를 가지는 함수 생성. 
    void OnStateChanged(ARSessionStateChangedEventArgs args)
    {
        Debug.Log(args.state);

        //지원되지 않는 기기 라면 종료
        if ( args.state == ARSessionState.Unsupported)
        {
            Application.Quit();
            return;
        }

        if(args.state == ARSessionState.Ready)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }
    void Update()
    {
        //간단하게 표현하기 위해 아닐때 리턴시킴. 
        //arsession 은 싱글톤 처럼 표현되어 있음. 
        if(ARSession.state != ARSessionState.Ready)
        {
            return;
        }
        if(Input.touchCount == 0)
        {
            return;
        }
        if(Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = touch.position;
                //확인을 위한 버튼 추가.

                if(aRRaycastManager.Raycast(touch.position, hits, TrackableType.Planes))
                {
                    Debug.Log("hithit clicked");
                    Pose hitPose = hits[0].pose;

                    Instantiate(spawnPrefab,hitPose.position,hitPose.rotation);
                }
            }
        }

        //if(touch.phase != TouchPhase.Began)
        //{
        //    return;
        //}
        //두개 이상이 히트되는 부분이 생길 수 있음. 
        //그래서 단순 컨테이너 넣기 
        //arRaycast 는 트랙커블 타입에 대해서만 감지 가능. 그냥 콜라이더 붙어있는 3d 감지 못함. 
        //그냥 3d 감지 할때는 Physics.Raycast 쓰면 됨. 


        //ar로 생성한 오브젝트에게 어떤 행동을 취할 때에는, Physics.Raycast 를 써야 함. !!
        //유니티 전통적인 방식의 레이캐스트를 써야 함.  즉 생성된 몬스터에게 볼을 던지는 등의 행위를 할 때.
        //if(aRRaycastManager.Raycast(touch.position, hits,TrackableType.Planes))
        //{
        //    //레이캐스트 정보가 pose 타입으로 들어옴. 첫 번째 맞은 것만 인식. 
        //    //핸드폰 위치나, 사이 거리, 다양한 부가정보가 포함된게 pose 임. 
//
        //    //pose 는 위치랑 회전을 3차원 공간에서 나타내는 타입임. 
        //    //forward position rotation right up 등 이 쓸 수 있는거 끝임. 
        //    //vr ar 개발할 때 쓰는 타입임
        //    Pose hitPose = hits[0].pose;
//
//
        //    if(hits[0].hitType == TrackableType.Image)
        //    {
//
        //        //레이를 맞은 오브젝트가 이미지라면, ~
//
        //    }
//
        //    //그냥 디스턴스는 유니티 씬 상에서의 거리를 나타냄 .
        //    //hits[0].distance
        //    //arsessiondistance 는 축적때문에 다를 수 있음. 
        //    //hits[0].sessionRelativeDistance
        //    //sessionrelativepose 도 오리진 기점으로 한 것.
        //    //hits[0].sessionRelativePose
//
        //    //trackableId 는 특정 오브젝트에 대해서만 할 때 씀
//
        //    TrackableId trackedBefore;
        //    trackedBefore = hits[0].trackableId;
//
        //    if(hits[0].trackableId == trackedBefore)
        //    {
        //        //이거 이전에 트랙킹 하던거. 어떤 처리하려고 할 떄. 
        //        //이전에 트랙킹 한건지 하려고 할 때 쓰면 됨. 
        //    }
        //    Instantiate(spawnPrefab,hitPose.position,hitPose.rotation);
    }
}
