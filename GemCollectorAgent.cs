using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using MLAgents;
using MLAgents.Sensors;
   
   
public class GemCollectorAgent: Agent
{
    public GameObject TargetCharacter;
    
    public static GemCollectorAgent instance;
    int pushHash = Animator.StringToHash("Push");
    int pickUpHash = Animator.StringToHash("Pickup");
    int runStateHash = Animator.StringToHash("Base Layer.Walking");

    public static bool MoveSwitch = true;
    public static bool isDead;
    public static bool runSwitch;
    private static bool walkSwitch = true;
    public static bool m_jump; // added


    private float movementSpeed = 0.25f;
    private float walkSpeed = 0.25f;
    private float runSpeed = 0.5f;
    
    
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    Animator anim;
    Rigidbody rb;
    
    private Rigidbody TargetCharacterRigidbody ;
    
    


    private float stunTime;
    public ParticleSystem stunPartc;
    public ParticleSystem GetJewelPartc;


    public static int deathTriggerInt = 0;
    public float turnSpeed = 100;

    public JoystickController landJoystick;
    public static  Vector3 dir = Vector3.zero;


    public float nowTime;
    public Vector3 nowPosition;
    public float compareTime = 0f;
    public Vector3 comparePosition = Vector3.zero;

    public static float horizontal;
    public static float vertical;
    public Vector3 HeurisDir = Vector3.zero;


    public Vector3 MLdir;
    public RaycastHit firstTargetJewel;

    RaycastHit[] hits;
    
    public void GetJoyValue()
    {
        horizontal = landJoystick.GetHorizontalValue();
        vertical = landJoystick.GetVerticalValue();  

        HeurisDir.x = horizontal;
        HeurisDir.z = vertical;  
    }
    private void Awake() {
         if(instance == null)
         {
            instance = this;
         }
    }
    public override void Initialize()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        TargetCharacterRigidbody = TargetCharacter.GetComponent<Rigidbody>();
        stunPartc.Pause();
        GetJewelPartc.Pause();

    }
    public override void OnEpisodeBegin()
    {
        Debug.Log("episode begin!");
        GameManager.instance.TimeChecker();
        stunPartc.Pause();
        GetJewelPartc.Pause();

        //this is for random environment building.
        //but it cost very high thus it would be necessary to fix random method code.
        //only can produce model in desktop with GPU

        //MLGameManager.instance.EnvironmentReset();
        MLGameManager.instance.RandomPositioning();
        
        //testMLManager.instance.testResetEnv();
        this.gameObject.transform.position = new Vector3(0,0,0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
        

        /*
        I think it is not necessary for action. 
        */
        //var localVelocity = transform.InverseTransformDirection(rb.velocity); 
        //sensor.AddObservation(localVelocity.x);
        //sensor.AddObservation(localVelocity.z);

        sensor.AddObservation(this.transform.position); 
        /*
            scf rst.
        */
        firstTargetJewel = rayHitJewel();
        sensor.AddObservation((firstTargetJewel.transform.position - transform.position).normalized);
        sensor.AddObservation(Vector3.Distance(firstTargetJewel.transform.position , transform.position));
        sensor.AddObservation(GameObject.FindGameObjectsWithTag("Jewel").Length);
        
        sensor.AddObservation(!MoveSwitch); // 스턴 상태 인식
        

        /*
        Need jump observe adding. but I guess it cost high so that action Jump.
        So I think it is better to decrease reward when Agent activate jump.

        */
        //sensor.AddObservation(m_jump); 점프 상태 인식
        //sensor.AddObservation(TargetCharacter.transform.position);
    }
    public RaycastHit rayHitJewel()
    {
        var myTransform = transform;
        hits = Physics.SphereCastAll(myTransform.position,30,Vector3.up,5);
        foreach(RaycastHit hit in hits)
        {
            if(hit.transform.tag == "Jewel")
            {
                Debug.Log(hit.transform.position);
                return hit;
            }
        }
        return hits[0];
    }
    

    public override float[] Heuristic()
    {
        var action = new float[2];
        if(MoveSwitch)
        {
            GetJoyValue(); 
            action[0] = HeurisDir.x;
            action[1] = HeurisDir.z;
            //if(walkSwitch)
            //{
            //    movementSpeed = walkSpeed;
            //}
            //if(runSwitch)
            //{
            //    movementSpeed = runSpeed;
            //}
            //Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
            //anim.SetFloat("Speed", movementSpeed * (Mathf.Abs(horizontal) + Mathf.Abs(vertical)));
            //if (movement != Vector3.zero)
            //{
            //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            //}
            //transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        }
        return action;
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        MoveCharacter(vectorAction);

        if(maxStep > 0 )
        {
            AddReward(-1f/ maxStep);
        }
    }
    public void MoveCharacter(float[] act)
    {
        if(MoveSwitch)
        {
            GetJoyValue(); 
            MLdir.x = Mathf.Clamp(act[0],-1,1);
            MLdir.z = Mathf.Clamp(act[1],-1,1);
            if(walkSwitch)
            {
                movementSpeed = walkSpeed;
            }
            if(runSwitch)
            {
                movementSpeed = runSpeed;
            }
            Vector3 movement = new Vector3(MLdir.x, 0.0f, MLdir.z);
            anim.SetFloat("Speed", movementSpeed * (Mathf.Abs(MLdir.x) + Mathf.Abs(MLdir.z)));
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            }
            transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        }
        //if(MoveSwitch)
        //{
        //    Vector3 dirToGo = Vector3.zero;
        //    Vector3 rotateDir = Vector3.zero;
        //
        //    if(walkSwitch || runSwitch)
        //    {
        //        if(walkSwitch)
        //        {
        //            movementSpeed = walkSpeed;
        //        }
        //        else if(runSwitch)
        //        {
        //            movementSpeed = runSpeed;
        //        }
        //
        //        int forwardAct = (int)act[0];
        //        int sideAct = (int)act[1];
        //        int rotateAct = (int)act[2];
        //        int jumpAct = (int)act[3];
        //
        //        switch (forwardAct)
        //        {
        //            case 1:
        //                dirToGo = transform.forward;
        //                Debug.Log("forward+");
        //                break;
        //            case 2:
        //                dirToGo = -transform.forward;
        //                Debug.Log("forward-");
        //                break;
        //        }
        //        switch(sideAct)
        //        {
        //            case 1:
        //                dirToGo = transform.right;
        //                Debug.Log("right+");
        //                break;
        //            case 2:
        //                dirToGo = -transform.right;
        //                Debug.Log("right-");
        //                break;
        //        }
        //        switch(rotateAct)
        //        {
        //            case 1:
        //                rotateDir = -transform.up;
        //                break;
        //            case 2:
        //                rotateDir = transform.up;
        //                break;
        //        }
        //        switch(jumpAct)
        //        {
        //            case 1:
        //                //Jump();
        //                break;
        //            case 2:
        //                break;
        //        }
        //        rb.AddForce(dirToGo * movementSpeed, ForceMode.VelocityChange);
        //        transform.Rotate(rotateDir, Time.fixedDeltaTime * turnSpeed);
        //    }
        //   
        //    //Vector3 movement = new Vector3(dir.x, 0.0f, dir.z);
        //    anim.SetFloat("Speed", movementSpeed * (Mathf.Abs(dirToGo.x) + Mathf.Abs(dirToGo.z)));
        //    //if (movement != Vector3.zero)
        //    //{
        //    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        //    //}
        //    //transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        //}
        ////if (rb.velocity.sqrMagnitude > 10f) // slow it down
        ////{
        ////    rb.velocity *= 0.95f;
        ////}
        //if(transform.position.x>30||transform.position.x<-30||
        //    transform.position.z>30||transform.position.z<-30||
        //    transform.position.y<-1||transform.position.y>7)
        //{
        //    AddReward(-1f);
        //    EndEpisode();
        //}
    }
    private void Update() {
        StatePositionDecreasePoint();
    }
    private void StatePositionDecreasePoint()
    {
        nowTime = Time.deltaTime;
        nowPosition = gameObject.transform.position;
        

        if(compareTime == 0 && comparePosition == Vector3.zero)
        {
            compareTime = nowTime + 0.1f;
            comparePosition = nowPosition;
        }
        else if(compareTime < nowTime)
        {
            if(comparePosition == nowPosition)
            {
                Debug.Log("state decrease Point!");
                AddReward(-0.1f);
            }
            comparePosition = Vector3.zero;
            compareTime = 0f;
        }
    }
    IEnumerator CheckJump ()
    {
        m_jump=true; //added
        anim.SetBool("jumpChecker", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("jumpChecker",false);
        m_jump=false;
    }
    public void Push()
    {
        TargetCharacterRigidbody.AddForce(transform.forward * 50,ForceMode.Force);
    }
    private void OnCollisionEnter(Collision other) {
    
        //부딪혔을 경우는 추가

        //if(other.collider.tag == "Character")
        //{
        //    anim.SetTrigger(pushHash);
        //    if(DataVariables.isVibrateOn)
        //    {
        //        Handheld.Vibrate();
        //    }
        //    HitCharacter();
        //    
        //    AudioController.Instance.HitSoundPlay();
        //}    


        /* for ml model testing colliding player off */

        //if(other.collider.tag == "Player")
        //{
        //    Debug.Log("hitted!!");
        //    AudioController.Instance.HittedSoundPlay();
        //    HittedByCharacter();
        //}
        //if(other.collider.tag == "map")
        //{
        //    anim.SetBool("isGrounded",true);
        //}
    }

    private void HitCharacter()
    {
        TargetCharacterRigidbody.AddForce(transform.forward * 50,ForceMode.Force);
    } 

    private void OnTriggerEnter(Collider other) {
        if(other.tag =="DeathPlane")
        {
            isDead = true;
            MoveSwitch = false;

            deathTriggerInt += 1;
            GameManager.instance.ReSpawnEnemy(isDead);
            AudioController.Instance.DeadSoundPlay();
            
            AddReward(-1f);
            EndEpisode();
        }
        if(other.tag == "Jewel")
        {
            GetJewelPartc.Play();
            anim.SetTrigger(pickUpHash);
            AudioController.Instance.GetJewelSoundPlay();
            //test jewel get collider
            if(other.gameObject.name == "1_Jewel")
            {
                DataVariables.characterScore += DataVariables.jewel_1_score;
                other.gameObject.SetActive(false);
                Debug.Log("Character Score : " +DataVariables.characterScore);
                AddReward(1f); //added
            }
            if(other.gameObject.name == "1_Jewel(Clone)")
            {
                DataVariables.characterScore += DataVariables.jewel_1_score;
                other.gameObject.SetActive(false);
                Debug.Log("Character Score : " +DataVariables.characterScore);
                AddReward(0.6f); //added
            }
            else if(other.gameObject.name == "2_Jewel(Clone)")
            {
                DataVariables.characterScore += DataVariables.jewel_2_score;
                other.gameObject.SetActive(false);
                Debug.Log("Character Score : " +DataVariables.characterScore);
                AddReward(0.8f); //added
            }
            else if(other.gameObject.name == "3_Jewel(Clone)")
            {
                DataVariables.characterScore += DataVariables.jewel_3_score;
                other.gameObject.SetActive(false);
                Debug.Log("Character Score : " +DataVariables.characterScore);
                AddReward(1f); //added
            }
        }
        else if(other.gameObject.name == "manholeCover")
        {
            isDead = true;
            MoveSwitch = false;
            if(DataVariables.enemyScore >= 10f)
            {
                DataVariables.enemyScore = DataVariables.enemyScore - 10f;
            }
            else
            {
                DataVariables.enemyScore = 0f;
            }
            deathTriggerInt += 1;
            GameManager.instance.ReSpawnEnemy(isDead);
            AudioController.Instance.DeadSoundPlay();
            AddReward(-1f); //added
        }
    }
    public void Jump()
    {
        rb.AddForce(0, jumpForce, 0);
        canJump = Time.time + timeBeforeNextJump;
        anim.SetBool("isGrounded",false);
        StartCoroutine("CheckJump");
    }

    IEnumerator StunTimeChecker()
    {
        Debug.Log("stunned");
        stunPartc.Play();
        MoveSwitch = false;
        yield return new WaitForSeconds(3);
        

        //when making base model just active off opponent object.
        AddReward(-0.05f); //added

        MoveSwitch = true;
        stunPartc.Stop();
    }
    private void HittedByCharacter()
    {
        StartCoroutine("StunTimeChecker");
    }
    public void EndGame(){
        EndEpisode();
    }
    
}