using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource previousAudio;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = clip;
        previousAudio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
