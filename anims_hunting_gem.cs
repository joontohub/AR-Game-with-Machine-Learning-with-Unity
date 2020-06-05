using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anims_hunting_gem : MonoBehaviour
{
    Animator anim;
    int jumpHash = Animator.StringToHash("Jump");
    int pushHash = Animator.StringToHash("Push");
    int pickUpHash = Animator.StringToHash("Pickup");
    int runStateHash = Animator.StringToHash("Base Layer.Walking");
    public float Speed = 5f;
    public float JumpHeight = 10f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;


    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    public float distToGround = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        //TODO: isGrounded not working !!!
        //_isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        _isGrounded = isGrounded();
        anim.SetBool("isGrounded", _isGrounded);
        
        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");

        
        if (_inputs != Vector3.zero)
        { 
            transform.forward = _inputs;
            //anim.SetFloat("Speed", Mathf.Abs(_inputs.x) + Mathf.Abs(_inputs.z));
        }

        //anim.SetFloat("Speed", Mathf.Abs(_inputs.x) + Mathf.Abs(_inputs.z));
        anim.SetFloat("xyVel", Speed *(Mathf.Abs(_inputs.x) + Mathf.Abs(_inputs.z)));
        
        // TODO: Jump script bug! 
        // TODO: animation startup too slow. Suspend the JumpHeight for a few seconds??
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            anim.SetTrigger(jumpHash);
            _inputs.y = JumpHeight; // ->  WaitForSeconds or Rigidbody.Sleep needed?
            //anim.ResetTrigger(jumpHash);
            //_body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.M) && _isGrounded)
        {
            anim.SetTrigger(pushHash);
        }

        if (Input.GetKeyDown(KeyCode.P) && _isGrounded)
        {
            anim.SetTrigger(pickUpHash);
        }
    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);    
    }

    /*
    virtual public void OnAnimatorMove()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //anim.SetTrigger(jumpHash);

            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }
    */

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround) ||
               Physics.Raycast(transform.position, Vector3.up, 0.05f);
    }

}
