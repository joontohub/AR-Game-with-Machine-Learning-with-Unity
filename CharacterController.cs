using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : JoystickValue
{
    int pushHash = Animator.StringToHash("Push");
    int pickUpHash = Animator.StringToHash("Pickup");
    int runStateHash = Animator.StringToHash("Base Layer.Walking");

    public static bool MoveSwitch = true;
    public static bool isDead;
    public static bool runSwitch;
    private static bool walkSwitch = true;

    RaycastHit hit;
    Vector3 ClickPos;


    private float movementSpeed = 0.25f;
    private float walkSpeed = 0.25f;
    private float runSpeed = 0.5f;
    
    
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    Animator anim;
    Rigidbody rb;
    
    public GameObject Enemy;
    private Rigidbody enemyRb ;
    private Rigidbody test_enemyRb;


    private float stunTime;
    public ParticleSystem stunPartc;
    public ParticleSystem GetJewelPartc;

    public static int deathTriggerInt = 0;

    public static bool AvoidGameSwitch;
    //for attack model
    public static bool AttackGameSwitch = true;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        enemyRb = Enemy.GetComponent<Rigidbody>();
        stunPartc.Pause();
        GetJewelPartc.Pause();
    }
    private void FixedUpdate() {
        MoveCharacter();
        var localVelocity = transform.InverseTransformDirection(rb.velocity); 
    }
    
    public void MoveCharacter()
    {
        if(MoveSwitch)
        {
            GetJoyValue(); 
            if(walkSwitch)
            {
                movementSpeed = walkSpeed;
            }
            if(runSwitch)
            {
                movementSpeed = runSpeed;
            }
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
            anim.SetFloat("Speed", movementSpeed * (Mathf.Abs(horizontal) + Mathf.Abs(vertical)));
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            }
            transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        }
    }
    IEnumerator CheckJump ()
    {
        anim.SetBool("jumpChecker", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("jumpChecker",false);
    }

    private void OnCollisionEnter(Collision other) {
   

        if(other.collider.tag == "Enemy" && AvoidGameSwitch == true)
        {
            Debug.Log("enemy!!");
            anim.SetTrigger(pushHash);
            if(DataVariables.isVibrateOn)
            {
                //testing in PC it is offed
                //Handheld.Vibrate();
            }
            HitEnemy();
            
            AudioController.Instance.HitSoundPlay();
        }  
        else if(other.collider.tag == "Enemy" && AttackGameSwitch == true)
        {
            AudioController.Instance.HittedSoundPlay();
            HittedByEnemy();
        }
        //학습을 위해 에이전트가 피하도록 
        //if(other.collider.tag == "Enemy2")
        //{
        //    AudioController.Instance.HittedSoundPlay();
        //}
        if(other.collider.tag == "map")
        {
            anim.SetBool("isGrounded",true);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag =="DeathPlane")
        {
            isDead = true;
            MoveSwitch = false;
            if(DataVariables.characterScore >= 10f)
            {
                DataVariables.characterScore = DataVariables.characterScore - 10f;
            }
            else
            {
                DataVariables.characterScore = 0f;
            }
            deathTriggerInt += 1;
            GameManager.instance.ReSpawnCharacter(isDead);
            AudioController.Instance.DeadSoundPlay();
        }
        if(other.tag == "Jewel")
        {
            GetJewelPartc.Play();
            anim.SetTrigger(pickUpHash);
            AudioController.Instance.GetJewelSoundPlay();
            if(other.gameObject.name == "1_Jewel(Clone)")
            {
                DataVariables.characterScore += DataVariables.jewel_1_score;
                other.gameObject.SetActive(false);
                Debug.Log("Character Score : " +DataVariables.characterScore);
            }
            else if(other.gameObject.name == "2_Jewel(Clone)")
            {
                DataVariables.characterScore += DataVariables.jewel_2_score;
                other.gameObject.SetActive(false);
                Debug.Log("Character Score : " +DataVariables.characterScore);
            }
            else if(other.gameObject.name == "3_Jewel(Clone)")
            {
                DataVariables.characterScore += DataVariables.jewel_3_score;
                other.gameObject.SetActive(false);
                Debug.Log("Character Score : " +DataVariables.characterScore);
            }
        }
        else if(other.gameObject.name == "manholeCover")
        {
            isDead = true;
            MoveSwitch = false;
            if(DataVariables.characterScore >= 10f)
            {
                DataVariables.characterScore = DataVariables.characterScore - 10f;
            }
            else
            {
                DataVariables.characterScore = 0f;
            }
            deathTriggerInt += 1;
            GameManager.instance.ReSpawnCharacter(isDead);
            AudioController.Instance.DeadSoundPlay();
        }
    }
    public void Jump()
    {
        rb.AddForce(0, jumpForce, 0);
        canJump = Time.time + timeBeforeNextJump;
        anim.SetBool("isGrounded",false);
        StartCoroutine("CheckJump");
    }
    private void HitEnemy()
    {
        enemyRb.AddForce(transform.forward * 50,ForceMode.Force);
    }
    IEnumerator StunTimeChecker()
    {
        Debug.Log("stuned");
        stunPartc.Play();
        MoveSwitch = false;
        yield return new WaitForSeconds(3);
        MoveSwitch = true;
        stunPartc.Stop();
    }
    private void HittedByEnemy()
    {
        StartCoroutine("StunTimeChecker");
    }

}
