using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    
    public List<Transform> points;
    public float pointRadius = 1;
    public float speed;
    int current = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        AddToList(this.gameObject.transform.parent.childCount);
        transform.position = points[0].transform.position;
    }

    private void FixedUpdate()
    {
        Move2Point();
    }

    private void Move2Point()
    {
        //transform.position = Vector3.Lerp(transform.position, points[current].transform.position, Time.deltaTime * speed);
        transform.position = Vector3.MoveTowards(transform.position, points[current].transform.position, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, points[current].transform.position) < pointRadius)
        {
            if (current >= points.Count - 1)
            {
                current = 0;
            } 
            else
            {
                current++;
            }
        }
    }

    private void AddToList(int listOfTransforms)
    {
        for (int i = 1; i < listOfTransforms; i++)
        {
            points.Add(this.gameObject.transform.parent.GetChild(i));
        }
    }
}
