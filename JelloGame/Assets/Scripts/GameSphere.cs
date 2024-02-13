using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSphere : MonoBehaviour
{
    GameObject sphere;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        sphere = GameObject.FindGameObjectWithTag("Sphere");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        sphere.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
