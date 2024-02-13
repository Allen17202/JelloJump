using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRotation : MonoBehaviour
{

    public float rotationDir = 0.0f;
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        transform.Rotate(0, rotationDir, 0);
    }
}
