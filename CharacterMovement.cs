using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] AudioClip[] walk;
    [SerializeField] AudioClip[] run;
    AudioSource playing;
    public static CharacterMovement instance;
    private void Awake()
    {
        instance = this;
    }
    [Header("Movement Transform")]
    [SerializeField] CharacterController cc;
    [SerializeField] Transform CharacterBody;
    [SerializeField] Transform GroundCheck;


    [Header("Movement")]
    public bool isSlowly;
    public bool isWalking;
    public bool isRunning;
    [SerializeField] float SlowSpeed;
    [SerializeField] float WalkSpeed;
    [SerializeField] float RunSpeed;
    [SerializeField] float JumpForce;

    float horizontal, vertical;

    [Header("Gravity")]

    Vector3 gravityVector;
    [SerializeField] float gravityAc = -9.81f;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask GroundLayer;

    private void FixedUpdate()
    {
        Movement();
        gravity();
    }
    private void Start()
    {
        print(QualitySettings.GetQualityLevel());
    }
    private void Update()
    {
        jump();
        checkMovement();
    }
    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 move = CharacterBody.right * horizontal + CharacterBody.forward * vertical;
        cc.Move(move * TotalSpeed() * Time.deltaTime);
    }
    void checkMovement()
    {
        if ((horizontal != 0 || vertical != 0) || (horizontal != 0 && vertical != 0))
        {
            if (TotalSpeed() == RunSpeed)
            {
                isRunning = true;
                isWalking = false;
                isSlowly = false;
            }
            else if (TotalSpeed() == WalkSpeed)
            {
                isRunning = false;
                isWalking = true;
                isSlowly = false;
            }
            else if (TotalSpeed() == SlowSpeed)
            {
                isSlowly = true;
                isRunning = false;
                isWalking = false;
            }
        }
        else
        {
            isRunning = false;
            isWalking = false;
        }
    }
    public float TotalSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return RunSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            return SlowSpeed;
        }
        else
        {
            return WalkSpeed; 
        }
    }
    void gravity()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, 0.4f, GroundLayer);

        if (!isGrounded)
        {
            gravityVector.y += gravityAc * Mathf.Pow(Time.deltaTime, 2);
        }
        else if (gravityVector.y < 0f)
        {
            gravityVector.y = -0.1f;
        
        }
       

        cc.Move(gravityVector);
    }
    void jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            gravityVector.y = Mathf.Sqrt(JumpForce * -2f * gravityAc/1000f);
        }
    }
}
