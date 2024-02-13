using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveOnTouch : MonoBehaviour
{
    public Vector3 endPoint;
    public GameObject triggerPlate;
    public float speed;
    public float pointRadius = 1;
    // Start is called before the first frame update
    void Start()
    {
        endPoint = transform.parent.GetChild(2).transform.position;
    }

    private void FixedUpdate()
    {
        if (triggerPlate.GetComponent<TriggerSwitch>().switchFlipped == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, endPoint) < pointRadius)
            {
                // Destroy(GameObject.FindGameObjectWithTag("Phase1"));
            }
        }
    }
}
