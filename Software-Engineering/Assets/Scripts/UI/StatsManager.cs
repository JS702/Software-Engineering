using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] List<UILineRenderer> uiLineRenderer;
    GameManager gameManager;

   
    
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        initStats();
    }
    //Subscribe to Event Methods
    private void OnEnable()
    {
        //Animal.OnGotKilled += addHaresKilled; -- Jons alter Code
        GameManager.onStatTracking += getAnimalsAlive;
        GameManager.onStatTracking += getHaresAlive;
        GameManager.onStatTracking += getFoxesAlive;
        GameManager.onStatTracking += getAverageFoxSpeed;
        GameManager.onStatTracking += getAverageHareSpeed;
        GameManager.onStatTracking += getAverageFoxSight;
        GameManager.onStatTracking += getAverageHareSight;
        GameManager.onStatTracking += getFoxGeneration;
        GameManager.onStatTracking += getHareGeneration;
        GameManager.onStatTracking += getFoxMalesAlive;
        GameManager.onStatTracking += getHareMalesAlive;
        GameManager.onStatTracking += getFoxFemalesAlive;
        GameManager.onStatTracking += getHareFemalesAlive;
        GameManager.onStatTracking += getFoxesStarved;
        GameManager.onStatTracking += getHaresStarved;
        GameManager.onStatTracking += getHaresKilled;
    }

    private void OnDisable()
    {
        //Animal.OnGotKilled -= addHaresKilled; -- Jons alter Code
        GameManager.onStatTracking -= getAnimalsAlive;
        GameManager.onStatTracking -= getHaresAlive;
        GameManager.onStatTracking -= getFoxesAlive;
        GameManager.onStatTracking -= getAverageFoxSpeed;
        GameManager.onStatTracking -= getAverageHareSpeed;
        GameManager.onStatTracking -= getAverageFoxSight;
        GameManager.onStatTracking -= getAverageHareSight;
        GameManager.onStatTracking -= getFoxGeneration;
        GameManager.onStatTracking -= getHareGeneration;
        GameManager.onStatTracking -= getFoxMalesAlive;
        GameManager.onStatTracking -= getHareMalesAlive;
        GameManager.onStatTracking -= getFoxFemalesAlive;
        GameManager.onStatTracking -= getHareFemalesAlive;
        GameManager.onStatTracking -= getFoxesStarved;
        GameManager.onStatTracking -= getHaresStarved;
        GameManager.onStatTracking -= getHaresKilled;
    }

    private void getAverageFoxSpeed() 
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.averageFoxSpeed, line);
    }

    private void getAverageHareSpeed()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.averageHareSpeed, line);
    }

    private void getAverageFoxSight()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.averageFoxSight, line);
    }

    private void getAverageHareSight()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.averageHareSight, line);
    }

    private void getFoxGeneration()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.foxGeneration, line);
    }

    private void getHareGeneration()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.hareGeneration, line);
    }

    private void getFoxMalesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.foxMalesAlive, line);
    }

    private void getHareMalesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.hareMalesAlive, line);
    }

    private void getFoxFemalesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.foxFemalesAlive, line);
    }

    private void getHareFemalesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.hareFemalesAlive, line);
    }

    private void getFoxesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("foxesAlive"));
        drawPoint(GameManager.foxesAlive, line);
    }

    private void getHaresAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("haresAlive"));
        drawPoint(GameManager.haresAlive, line);
    }

    private void getFoxesStarved()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.foxesStarved, line);
    }

    private void getHaresStarved()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals(""));
        drawPoint(GameManager.haresStarved, line);
    }

    private void getHaresKilled()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("haresKilled"));
        drawPoint(GameManager.haresKilled, line);
    }

    private void getAnimalsAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("animalsAlive"));
        drawPoint(GameManager.animalsAlive, line);
    }

    //Jons alter Code, für den ich einheitliche neue Methoden geschrieben haben ~Vinzent
    /**
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
        //Debug.Log(count);
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
    */
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

