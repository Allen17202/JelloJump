using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JelloCube : MonoBehaviour
{

    GameObject PlayerObject;
    public PlayerController playerController;

    private Animator jelloAnimator;
    private AudioSource jelloAudioSource;
    public AudioClip bounceSFX;

    [Header("JelloSettings")]
    int cubeCounter = 1;
    public int jellyFoodAmtGain = 5;
    public int jellyFoodAmtLoss = 5;
    public KeyCode eatKey = KeyCode.LeftShift;
    bool inCube = false;

    private void Start()
    {
        jelloAnimator = GetComponent<Animator>();
        jelloAudioSource = GetComponent<AudioSource>();
        //todo Refactor : dont set colors via code and use a bool to check if bad jello
        //todo Refactor : consider moving Playerobject FIND ; having 100 cubes find at the start causes an initial dip in performance [Find is costly function]
        //PlayerObject = GameObject.FindGameObjectWithTag("Player");
        //playerController = PlayerObject.GetComponent<PlayerController>();

        //if (GetComponent<Transform>().localScale.x > 10)
        //{
        //    GetComponent<Renderer>().materials[0].color = Color.red;
        //} else
        //{
        //    GetComponent<Renderer>().materials[0].color = Color.green;
        //}
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(eatKey) && !inCube)
        {
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
        else if (!inCube)
        {
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
        //playerSizeCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with: " + other.gameObject.name + " Jiggle Jiggle");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("FLY");
            inCube = true;
            cubeCounter--;
            playerController.EatJelly(20);
            Vector3 playerVelocity = other.gameObject.GetComponent<Rigidbody>().velocity;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Lerp(playerVelocity, new Vector3(playerVelocity.x * .1f, playerVelocity.y * .1f, playerVelocity.z * .1f), 2f);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "JelloGreen")
        {
            //other.gameObject.GetComponent<Transform>().localScale *= 2f;
            //GameObject.FindGameObjectWithTag("CinemachineTopDown").GetComponent<CinemachineCamBoom>().increaseCamRadiusOnGrow();
            Destroy(this.gameObject);
        } else
        {
            //other.gameObject.GetComponent<Transform>().localScale *= .5f;
            GameObject.FindGameObjectWithTag("CinemachineTopDown").GetComponent<CinemachineCamBoom>().decreaseCamRadiusOnShrink();
        }
        //Vector3 cubeScale = this.gameObject.GetComponent<Transform>().localScale;
        //this.gameObject.GetComponent<Transform>().localScale = new Vector3(cubeScale.x * .75f, cubeScale.y * .75f, cubeScale.z * .75f);
        //this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        //inCube = false;

        //if (cubeCounter <= 0)
        //{
        //    Destroy(this.gameObject);
        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            jelloAudioSource.PlayOneShot(bounceSFX);
            collision.gameObject.GetComponentInChildren<Animator>().SetTrigger("Bounce");

            if (gameObject.tag == "JelloGreen" && collision.gameObject.GetComponent<PlayerController>().currentAmtJelly < collision.gameObject.GetComponent<PlayerController>().maxJelly)
            {
                //we eat
                collision.gameObject.GetComponent<PlayerController>().EatJelly(jellyFoodAmtGain);
                jelloAnimator.SetTrigger("Bounce");
                gameObject.GetComponent<BoxCollider>().enabled = false;
            } 
            else if (gameObject.tag == "JelloRed")
            {
                //we lose jellyness
                collision.gameObject.GetComponent<PlayerController>().LoseJelly(jellyFoodAmtLoss);
                jelloAnimator.SetTrigger("Bounce");
            }

            //if (GetComponent<Renderer>().materials[0].color == Color.red && PlayerObject.GetComponent<Transform>().localScale.z > 1)
            //{
            //    collision.gameObject.GetComponent<Transform>().localScale *= .5f;
            //    GameObject.FindGameObjectWithTag("CinemachineTopDown").GetComponent<CinemachineCamBoom>().decreaseCamRadiusOnShrink();
            //}
            //TODO if we want to give more bounce force we have to calculate vector where to bounce cant just go up
            //Vector3 cubeScale = this.gameObject.GetComponent<Transform>().localScale;
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 100.0f * cubeScale.y, 0));
            
        }
    }

    public void playerSizeCheck()
    {
        float currentPlayerZSize = PlayerObject.GetComponent<Transform>().localScale.z;
        
        if (currentPlayerZSize > 10)
        {
            if (GetComponent<Transform>().localScale.z > 10 && GetComponent<Transform>().localScale.z < 20)
            {
                GetComponent<Renderer>().materials[0].color = Color.green;
            } else if (GetComponent<Transform>().localScale.z >= 20 && currentPlayerZSize > 20)
            {
                GetComponent<Renderer>().materials[0].color = Color.green;
            }
        }
    }

}
