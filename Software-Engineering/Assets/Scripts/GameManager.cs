using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Event Methods 
    public delegate void statTracking();
    public static event statTracking onStatTracking;

    //Timer
    public float currentTime = 0f;
    float maxTime = 100f;

    int statsTrackingIntervall = 30;
    int nextStatsTrackingIntervall = 30;



    private void Update()
    {
        if (currentTime < maxTime)
        {
            if (currentTime > nextStatsTrackingIntervall)
            {
                onStatTracking?.Invoke();
            }
            currentTime += 1 * Time.deltaTime;
        }
        else
        {
            //TODO implement end screen
        }
    }
}
