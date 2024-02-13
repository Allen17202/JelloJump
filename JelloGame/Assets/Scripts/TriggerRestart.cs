using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerRestart : MonoBehaviour
{
    private Scene scene;
    private GameObject screen;
    public Vector3 gravityAdj;
    public GameObject playerCharacter;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Physics.gravity = gravityAdj;
        screen = GameObject.FindGameObjectWithTag("LoseMenu");
        screen.SetActive(false);
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (playerCharacter.gameObject.GetComponent<PlayerController>().currentAmtJelly <= 0)
        {
            LoseScreenPop();
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LoseScreenPop();
        }
    }

    public void LoseScreenPop()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        screen.SetActive(true);
        //Time.timeScale = 0f;
    }
}
