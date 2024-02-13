using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaStarSpawn : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject[] levelOneSpawn;
    public GameObject[] levelTwoSpawn;
    public GameObject[] levelThreeSpawn;
    public GameObject[] levelFourSpawn;
    public GameObject[] levelFiveSpawn;
    public GameObject[] levelSixSpawn;
    public GameObject starPrefab;
    public int starCounter = 0;
    public int playerStarCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        levelOneSpawn = GameObject.FindGameObjectsWithTag("ArenaSpawn1");
        levelTwoSpawn = GameObject.FindGameObjectsWithTag("ArenaSpawn2");
        levelThreeSpawn = GameObject.FindGameObjectsWithTag("ArenaSpawn3");
        levelFourSpawn = GameObject.FindGameObjectsWithTag("ArenaSpawn4");
        levelFiveSpawn = GameObject.FindGameObjectsWithTag("ArenaSpawn5");
        levelSixSpawn = GameObject.FindGameObjectsWithTag("ArenaSpawn6");
        winScreen = GameObject.FindGameObjectWithTag("WinMenu");
        winScreen.SetActive(false);
        SpawnNewStar(starCounter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("Star").Length <= 0)
        {
            SpawnNewStar(starCounter);
        }

        
    }

    private void SpawnNewStar(int counter)
    {
        switch(counter)
        {
            case 0:
                Instantiate(starPrefab, levelOneSpawn[Random.Range(0, levelOneSpawn.Length)].transform);
                starCounter++;
                break;
            case 1:
                Instantiate(starPrefab, levelTwoSpawn[Random.Range(0, levelTwoSpawn.Length)].transform);
                starCounter++;
                break;
            case 2:
                Instantiate(starPrefab, levelThreeSpawn[Random.Range(0, levelThreeSpawn.Length)].transform);
                starCounter++;
                break;
            case 3:
                Instantiate(starPrefab, levelFourSpawn[Random.Range(0, levelFourSpawn.Length)].transform);
                starCounter++;
                break;
            case 4:
                Instantiate(starPrefab, levelFiveSpawn[Random.Range(0, levelFiveSpawn.Length)].transform);
                starCounter++;
                break;
            case 5:
                Instantiate(starPrefab, levelSixSpawn[Random.Range(0, levelSixSpawn.Length)].transform);
                starCounter++;
                break;
            case 6:
                EndCondition();
                break;
            default:
                break;
        }
    }

    private void EndCondition()
    {
        if (starCounter >= 6)
        {
            if (playerStarCounter >= 3 && GameObject.FindGameObjectsWithTag("Star").Length <= 0)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                winScreen.SetActive(true);
            }
            else
            {
                GameObject.FindGameObjectWithTag("TriggerReset").GetComponent<TriggerRestart>().LoseScreenPop();
            }
        }
    }
}
