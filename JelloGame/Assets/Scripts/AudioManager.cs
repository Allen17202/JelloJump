using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] randomAudioSources;
    public AudioClip[] directedAudioSources;
    public AudioSource MonsterVO;

    bool audioSwitch01;
    bool audioSwitch02;
    bool audioSwitch03;
    // Start is called before the first frame update
    void Start()
    {
        
        audioSwitch01 = true;
        audioSwitch02 = true;
        audioSwitch03 = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }


    private void StartAudio(int index)
    {
        MonsterVO.clip = directedAudioSources[index];
        MonsterVO.Play();
        audioSwitch01 = false;
        AudioDelay();
    }

    IEnumerator AudioDelay()
    {
        yield return new WaitForSeconds(MonsterVO.clip.length);
    }
}
