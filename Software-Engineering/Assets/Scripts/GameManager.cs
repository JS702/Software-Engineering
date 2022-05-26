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
    float maxTime;

    private int statsTrackingIntervall = 10;
    private int nextStatsTrackingIntervall = 10;
    public int currentStatsTrackingIntervall = 1;

    //UI
    [SerializeField] GameObject panel;

    private void Start()
    {
        maxTime = Timeslider.timef;
    }
    private void Update()
    {
        if (currentTime < maxTime)
        {
            if (currentTime > nextStatsTrackingIntervall)
            {
                hareAlives = new List<GameObject>(GameObject.FindGameObjectsWithTag("Prey"));
                foxAlives = new List<GameObject>(GameObject.FindGameObjectsWithTag("Fox"));
                onStatTracking?.Invoke();
                nextStatsTrackingIntervall += statsTrackingIntervall;
                currentStatsTrackingIntervall++;
            }
            currentTime += 1 * Time.deltaTime;
        }
        if (!panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                float timeScale = Time.timeScale;
                if (timeScale >= 16)
                {
                    Time.timeScale = 0.5f;
                }
                else
                {
                    Time.timeScale *= 2;
                }

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                float timeScale = Time.timeScale;

                if (timeScale <= 0.5)
                {
                    Time.timeScale = 16f;
                }
                else
                {
                    Time.timeScale /= 2;
                }
            }
        }
        else
        {
            //TODO implement end screen
        }  
    }
}
