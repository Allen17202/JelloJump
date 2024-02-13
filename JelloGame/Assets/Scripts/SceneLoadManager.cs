using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public GameObject sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        //black screen animation thing at start??
        sceneTransition.SetActive(true);
        StartCoroutine(SceneTransitionDisable());
    }

    //load next level (based on build index) build settings -> set correct order of scenes to work
    public void LoadNextLevel()
    {
        StartCoroutine(changeScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void RestartLevel()
    {
        StartCoroutine(changeScene(SceneManager.GetActiveScene().buildIndex));

    }

    public void LoadLevelbyBuildNum(int num)
    {
        StartCoroutine(changeScene(num));
    }

    IEnumerator SceneTransitionDisable()
    {
        while (!sceneTransition.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            //we wait
            yield return null;
        }
        sceneTransition.transform.SetAsFirstSibling();
    }

    IEnumerator changeScene(int sceneNum)
    {
        sceneTransition.transform.SetAsLastSibling();
        sceneTransition.GetComponent<Animator>().SetTrigger("ChangeScene");
        while (!sceneTransition.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("sceneChange"))
        {
            //we wait
            yield return null;
        }
        SceneManager.LoadScene(sceneNum);
        yield return null;
    }

    IEnumerator changeScene(string sceneName)
    {
        sceneTransition.transform.SetAsLastSibling();
        sceneTransition.GetComponent<Animator>().SetTrigger("ChangeScene");
        while (!sceneTransition.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("sceneChange"))
        {
            //we wait
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        yield return null;
    }

    public void LoadLevel1()
    {
        StartCoroutine(changeScene("JelloJumpPhase1"));
    }

    public void MainMenu()
    {
        StartCoroutine(changeScene("MainMenu"));
    }

    public void LevelSelect()
    {
        StartCoroutine(changeScene("LevelSelect"));
    }

    public void Exit()
    {
        Application.Quit();
    }


}
