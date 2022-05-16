using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Event Methods 
    public delegate void statTracking();
    public static event statTracking onStatTracking;

    public List<GameObject> hareAlives;
    public List<GameObject> foxAlives;

    //Timer
    public float currentTime = 0f;
    float maxTime = 100f;

    int statsTrackingIntervall = 10;
    int nextStatsTrackingIntervall = 10;
    public int currentStatsTrackingIntervall = 1;
    private void Start()
    {
        hareAlives.Add(new GameObject());
        hareAlives.Add(new GameObject());
    }



    private void Update()
    {
        if (currentTime < maxTime)
        {
            if (currentTime > nextStatsTrackingIntervall)
            {
                onStatTracking?.Invoke();
                nextStatsTrackingIntervall += statsTrackingIntervall;
                currentStatsTrackingIntervall++;
            }
            currentTime += 1 * Time.deltaTime;
        }
        else
        {
            //TODO implement end screen
        }
    }
}
