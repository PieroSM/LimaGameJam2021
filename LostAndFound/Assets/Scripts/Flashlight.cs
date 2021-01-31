using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Flashlight : MonoBehaviour
{
    bool isOn = true;
    Light2D light2D;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }
    public void Switch()
    {
        if (isOn)
        {
            light2D.intensity = 0;
            isOn = false;
        }
        else
        {
            light2D.intensity = 1;
            isOn = true;
        }
    }
}
