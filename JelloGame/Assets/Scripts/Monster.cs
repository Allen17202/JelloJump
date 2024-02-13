using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    GameObject monster;
    GameObject player;
    Scene currentScene;

    string sceneName;
    public float ascensionRate;
    float monsterUp;

    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.FindGameObjectWithTag("Monster");
        player = GameObject.FindGameObjectWithTag("Player");
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        //Debug.Log("sceneName: " + sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Debug.Log(player.transform.position);
        if (sceneName == "JelloJumpBoss")
        {
            monsterUp = monster.transform.position.y + ascensionRate;
            monster.transform.position = new Vector3(player.transform.position.x, monsterUp, player.transform.position.z);
        }
        else
        {
            monster.transform.position = new Vector3(player.transform.position.x, monster.transform.position.y, player.transform.position.z);
        }
    }
}
