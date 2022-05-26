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
        initStats();
    }
    //Subscribe to Event Methods
    private void OnEnable()
    {
        Animal.OnGotKilled += addHaresKilled;
        GameManager.onStatTracking += getAnimalsAlive;
        GameManager.onStatTracking += getHaresAlive;
        GameManager.onStatTracking += getFoxesAlive;
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

    private void getHaresAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("haresAlive"));
        drawPoint(gameManager.hareAlives.Count, line);
    }

    private void getFoxesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("foxesAlive"));
        drawPoint(gameManager.foxAlives.Count, line);
    }

    private int getAnimalsStarved()
    {
        return foxesStarved + haresStarved;
    }


    private void drawPoint(float value, UILineRenderer lineRenderer)
    {
        lineRenderer.points.Add(new Vector2(gameManager.currentStatsTrackingIntervall, value));
        lineRenderer.updateScale();
    }

    private void initStats()
    {
        uiLineRenderer.Find(renderer => renderer.diagramName.Equals("foxesAlive")).points.Add(new Vector2(0, SettingsmenuFox.fox));
        uiLineRenderer.Find(renderer => renderer.diagramName.Equals("foxesAlive")).updateScale();

        uiLineRenderer.Find(renderer => renderer.diagramName.Equals("haresAlive")).points.Add(new Vector2(0, SettingsmenuBunny.bunny));
        uiLineRenderer.Find(renderer => renderer.diagramName.Equals("haresAlive")).updateScale();

        uiLineRenderer.Find(renderer => renderer.diagramName.Equals("animalsAlive")).points.Add(new Vector2(0, SettingsmenuBunny.bunny + SettingsmenuFox.fox));
        uiLineRenderer.Find(renderer => renderer.diagramName.Equals("animalsAlive")).updateScale();

    }
}

