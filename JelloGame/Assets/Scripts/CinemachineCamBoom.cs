using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCamBoom : MonoBehaviour
{
    public CinemachineFreeLook cinemachineTopDown;
    public float camIncrease = 1.75f;
    public float camDecrease = .571f;

    public float camSpeed = .5f;

    public void increaseCamRadiusOnGrow()
    {
        /*
        for (int i = 0; i < cinemachineTopDown.m_Orbits.Length; i++)
        {
            cinemachineTopDown.m_Orbits[i].m_Radius *= 1.75f;
            if (i == 0)
            {
                cinemachineTopDown.m_Orbits[i].m_Height *= 1.75f;
            }
        }
        */
        StartCoroutine("increaseCam");
    }

    IEnumerator increaseCam()
    {
        float targetCam = cinemachineTopDown.m_Orbits[0].m_Radius * camIncrease;
        while(cinemachineTopDown.m_Orbits[0].m_Radius < targetCam)
        {
            cinemachineTopDown.m_Orbits[0].m_Radius += (.1f * camSpeed);
            cinemachineTopDown.m_Orbits[1].m_Radius += (.1f * camSpeed);
            cinemachineTopDown.m_Orbits[2].m_Radius += (.1f * camSpeed);

            cinemachineTopDown.m_Orbits[0].m_Height += (.1f * camSpeed);
            yield return null;
        }
    }

    public void decreaseCamRadiusOnShrink()
    {
        /*
        for (int i = 0; i < cinemachineTopDown.m_Orbits.Length; i++)
        {
            cinemachineTopDown.m_Orbits[i].m_Radius *= .571f;
            if (i == 0)
            {
                cinemachineTopDown.m_Orbits[i].m_Height *= .571f;
            }
        }
        */
        StartCoroutine("decreaseCam");
    }

    IEnumerator decreaseCam()
    {
        float targetCam = cinemachineTopDown.m_Orbits[0].m_Radius * camDecrease;
        while (cinemachineTopDown.m_Orbits[0].m_Radius < targetCam)
        {
            cinemachineTopDown.m_Orbits[0].m_Radius -= (.1f * camSpeed);
            cinemachineTopDown.m_Orbits[1].m_Radius -= (.1f * camSpeed);
            cinemachineTopDown.m_Orbits[2].m_Radius -= (.1f * camSpeed);

            cinemachineTopDown.m_Orbits[0].m_Height -= (.1f * camSpeed);
            yield return null;
        }
    }
}
