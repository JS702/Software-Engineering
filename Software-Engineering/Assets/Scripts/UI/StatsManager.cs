using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] List<UILineRenderer> uiLineRenderer;
    GameManager gameManager;

    private int foxesAlive;
    private int haresAlive;

    private int haresKilled;
    private int haresStarved;
    private int foxesStarved;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    //Subscribe to Event Methods
    private void OnEnable()
    {
        Animal.OnGotKilled += addHaresKilled;
        GameManager.onStatTracking += getAnimalsAlive;
    }

    private void OnDisable()
    {
        Animal.OnGotKilled -= addHaresKilled;
        GameManager.onStatTracking -= getAnimalsAlive;
    }

    private void addHaresKilled()
    {
        //haresKilled++;
        //UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("haresKilled"));
        //drawPoint(haresKilled, line);
    }

    private void getAnimalsAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("animalsAlive"));
        int count = gameManager.hareAlives.Count + gameManager.foxAlives.Count;
        Debug.Log(count);
        drawPoint(count, line);
    }

    private int getAnimalsStarved()
    {
        return foxesStarved + haresStarved;
    }

    private int getTime()
    {
        return Mathf.FloorToInt(gameManager.currentTime);
    }

    private void drawPoint(float value, UILineRenderer lineRenderer)
    {
        lineRenderer.points.Add(new Vector2(gameManager.currentStatsTrackingIntervall, value));
        lineRenderer.updateScale();
    }

}

