using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaStar : MonoBehaviour
{
    GameObject monster;
    GameObject monsterTrigger;
    public GameObject arenaManager;
    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.FindGameObjectWithTag("Monster");
        monsterTrigger = GameObject.FindGameObjectWithTag("TriggerReset");
        arenaManager = GameObject.FindGameObjectWithTag("ArenaManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (monsterTrigger.GetComponent<Transform>().position.y + 73 > this.gameObject.GetComponent<Transform>().position.y)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            arenaManager.GetComponent<ArenaStarSpawn>().playerStarCounter++;
            monster.GetComponent<Transform>().position -= new Vector3(0, 140.0f, 0);
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<AudioSource>().Play();

            Destroy(this.gameObject, 1f);
        }
    }
}
