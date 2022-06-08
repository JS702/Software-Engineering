using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     //Animals
    public static int animalsAlive;

    // Foxes
    public static float averageFoxSpeed;
    public static float averageFoxSight;
    public static int foxGeneration;
    public static int foxMalesAlive;
    public static int foxFemalesAlive;
    public static int foxesAlive;
    public static int foxesStarved;


    // Hares
    public static float averageHareSpeed;
    public static float averageHareSight;
    public static int hareGeneration;
    public static int hareFemalesAlive;
    public static int hareMalesAlive;
    public static int haresAlive;
    public static int haresStarved;
    public static int haresKilled;

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
    public int currentStatsTrackingIntervall = 0;
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

                    float tempAverageFoxSpeed = 0;
                    float tempAverageFoxSight = 0;
                    foreach(GameObject fox in foxAlives){
                        tempAverageFoxSpeed += fox.GetComponent<Movement>().normalSpeed;
                        tempAverageFoxSight += fox.GetComponentInChildren<SphereCollider>().radius;
                    }
                    averageFoxSpeed = tempAverageFoxSpeed / foxAlives.Count;
                    averageFoxSight = tempAverageFoxSight / foxAlives.Count;

                    float tempAverageHareSpeed = 0;
                    float tempAverageHareSight = 0;
                    foreach(GameObject hare in hareAlives){
                        tempAverageHareSpeed += hare.GetComponent<Movement>().normalSpeed;
                        tempAverageHareSight += hare.GetComponentInChildren<SphereCollider>().radius;
                    }
                    averageHareSpeed = tempAverageHareSpeed / hareAlives.Count;
                    averageHareSight = tempAverageHareSight / hareAlives.Count;

                    currentStatsTrackingIntervall++;

                    onStatTracking?.Invoke();
                    nextStatsTrackingIntervall += statsTrackingIntervall;
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
