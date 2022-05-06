using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    float maxTime = 100f;


    private void Update()
    {
        if(currentTime < maxTime)
        {
            currentTime += 1 * Time.deltaTime;
        }
        else
        {
            //TODO implement end screen
        }
    }
}
