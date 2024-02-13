using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArenaText : MonoBehaviour
{
    public GameObject text;
    public int playerScore;
    public GameObject arenaManager;
    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject;
        arenaManager = GameObject.FindGameObjectWithTag("ArenaManager");
    }

    // Update is called once per frame
    void Update()
    {
        playerScore = arenaManager.GetComponent<ArenaStarSpawn>().playerStarCounter;

        text.GetComponent<TextMeshProUGUI>().text = $"{playerScore}/6";

        if (playerScore >= 3)
        {
            text.GetComponent<TextMeshProUGUI>().color = Color.green;
        }
    }
}
