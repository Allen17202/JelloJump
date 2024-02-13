using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float boostSpeed = .5f;


    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    bool readyToJump = true;

    public float airMultiplier;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode diveKey = KeyCode.Mouse0;
    public KeyCode boostKey = KeyCode.Mouse1;
    public KeyCode jumpKey = KeyCode.Space;

    public float maxFuel = 4f;
    public float curFuel;
    public GameObject jetpackFX;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public float diveForce;
    private bool diving = false; //can only be reset after hitting a block or doing an action
    public float diveCooldown;
    bool readyToDive = true;

    public Transform orientation;
    public GameObject playerObj;

    float horizontalInput;
    float verticalInput;

    [Header("UI Bar")]
    public float maxJelly = 100;
    public float currentAmtJelly = 5;
    public GameObject jellyBar;

    Vector3 moveDirection;
    Rigidbody rb;
    Animator animator;
    public GameObject audioSource;
    private GameObject animeLinesFX;
    private GameObject boostGreen;
    private GameObject boostGrey;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
        animeLinesFX = GameObject.Find("Anime Lines FX");

        jellyBar.GetComponent<JellyBar>().SetMaxJelly(maxJelly);
        jellyBar.GetComponent<JellyBar>().SetJellyBar(currentAmtJelly);
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            boostGreen = GameObject.FindGameObjectWithTag("BoostGreen");
            boostGrey = GameObject.FindGameObjectWithTag("BoostGrey");
            boostGrey.SetActive(false);
        }
        curFuel = maxFuel;
        readyToJump = true;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // ground check
//        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
//
  //      MyInput();
    //    SpeedControl();

        // handle drag
      //  if (grounded)
   //         rb.drag = groundDrag;
     //   else
       //     rb.drag = 0;

    }

    public void EatJelly(int lunch)
    {
        currentAmtJelly += lunch;
        jellyBar.GetComponent<JellyBar>().SetJellyBar(currentAmtJelly);
    }

    public void LoseJelly(int lunch)
    {
        currentAmtJelly -= lunch;
        jellyBar.GetComponent<JellyBar>().SetJellyBar(currentAmtJelly);
    }

    private void FixedUpdate()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        // Native to FU
        MovePlayer();

        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            CheckBoost();
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animator.SetBool("InAir", !grounded);

        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            // Dive
            if (Input.GetKey(diveKey) && readyToDive && !grounded)
            {
                Dive();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            //Boost /jetpack mechanic
            if (Input.GetKey(boostKey) && curFuel > 0 && !grounded) //todo aquire fuel from items
            {
                Boost();
            }
            else
            {
                jetpackFX.SetActive(false);
                audioSource.gameObject.SetActive(false);
            }
        }
        else
        {
            jetpackFX.SetActive(false);
            audioSource.gameObject.SetActive(false);
        }

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 35f * airMultiplier, ForceMode.Force);
    }

    private void RotatePlayer()
    {
        // Matching character y rotation with camera
        Vector3 playerRotation = this.gameObject.transform.eulerAngles;

        playerRotation.y = Camera.main.transform.eulerAngles.y;
        //playerRotation.x = Camera.main.transform.eulerAngles.x;
        this.gameObject.transform.eulerAngles = playerRotation;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Boost()
    {
        //Play some particle FX and SFX
        if(currentAmtJelly > 1) //cant use up all your life force
        {
            jetpackFX.SetActive(true);
            audioSource.gameObject.SetActive(true);
            currentAmtJelly -= Time.deltaTime;
            rb.AddForce(rb.transform.up * boostSpeed, ForceMode.Impulse);
            jellyBar.GetComponent<JellyBar>().SetJellyBar(currentAmtJelly);
        }
    }

    private void CheckBoost()
    {
        if (currentAmtJelly > 1)
        {
            boostGreen.SetActive(true);
            boostGrey.SetActive(false);
        }
        else
        {
            boostGreen.SetActive(false);
            boostGrey.SetActive(true);
            jetpackFX.SetActive(false);
            audioSource.gameObject.SetActive(false);
        }
    }

    private void Dive()
    {
        if (diving == false && readyToDive)
        {
            Debug.Log("divedive");
            rb.AddForce(-transform.up * diveForce, ForceMode.Impulse);
            // diving GFX/SFX
            playerObj.transform.localScale *= -1;
            diving = true;
            readyToDive = false;
            animeLinesFX.SetActive(true);

        }
    }

    private void ResetDive()
    {
        readyToDive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //reset dive after bounce
        if (diving == true)
        {
            playerObj.transform.localScale *= -1;
        }
        diving = false;
        animeLinesFX.SetActive(false);
        Invoke(nameof(ResetDive), diveCooldown);

    }

}