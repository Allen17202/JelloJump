using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JellyBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxJelly(float jelly)
    {
        slider.maxValue = jelly;
        slider.value = jelly;
    }

    public void SetJellyBar(float jelly)
    {
        slider.value = jelly;
    }
}
