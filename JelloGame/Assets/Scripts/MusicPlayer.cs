using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioF;
    public AudioClip music;
    public AudioClip arenaMusicClip;
    public bool sceneSwitch;
    public bool sceneNotFour;

    private void Awake()
    {
        int numMusicPlayer = FindObjectsOfType<MusicPlayer>().Length;

        if (numMusicPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

        }
    }

    private void Start()
    {
        sceneNotFour = true;
        sceneSwitch = true;
        audioF = GetComponent<AudioSource>();
        audioF.clip = music;
        audioF.volume = 0.1f;
        audioF.Play();
    }

    private void FixedUpdate()
    {
        arenaMusic();
    }



    private void arenaMusic()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4 && sceneSwitch)
        {
            audioF.Stop();

            audioF.clip = arenaMusicClip;

            audioF.volume = 0.5f;

            audioF.Play();
            sceneSwitch = false;
            sceneNotFour = false;
        }
        else if (sceneNotFour == false && SceneManager.GetActiveScene().buildIndex != 4)
        {
            audioF.clip = music;
            audioF.volume = 0.1f;
            audioF.Play();

            sceneNotFour = true;
        }
    }

}