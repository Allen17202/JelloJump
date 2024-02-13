using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour
{
    public bool switchFlipped;
    // Start is called before the first frame update
    private void Start()
    {
        switchFlipped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switchFlipped = true;
        }
    }
}
