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
    public bool gameOver = false;


    //UI
    [SerializeField] GameObject panel;
    [SerializeField] GameObject endScreen;


    private void Start()
    {
        maxTime = Timeslider.timef;
    }
    private void Update()
    {
        if (!gameOver)
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
            else
            {
                Debug.Log("Ende");
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FreeFlyCamera>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endScreen.SetActive(true);
                gameOver = true;
                Time.timeScale = 0;
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
        }
    }
}
