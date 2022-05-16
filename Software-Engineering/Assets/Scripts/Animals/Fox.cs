using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fox : Animal
{

    public int killedHares = 0;
    void Start()
    {
        setBar(ref currentHealth, health, healthBar);
        setBar(ref currentHunger, hunger, hungerBar);
        setBar(ref currentThirst, thirst, thirstBar);
        
        hornyBar.slider.maxValue=5;
        hornyBar.slider.value=0;
        currentHorny=0;

    }

    public bool isThirsty;
    public List<Vector3> waterSourcePositionList;
    public void addWaterSourceToList(Collider col)
    {
        Debug.Log("Water in sight");
        GameObject water = col.gameObject;
        Vector3 waterSourcePostion = water.transform.position;
        if (!waterSourcePositionList.Contains(waterSourcePostion))
        {
            waterSourcePositionList.Add(waterSourcePostion);
            Debug.Log("Watersource added" + waterSourcePostion.ToString());
        }
        else
        {
            Debug.Log("Water already in list");
        }
    }

    public Vector3 moveToNearestWaterSource()
    {

        
        float distanceToNearestWaterSource = Vector3.Distance(waterSourcePositionList[0], transform.position);
        Vector3 nearestWaterSourcePosition = waterSourcePositionList[0];
        foreach (Vector3 waterSourceposition in waterSourcePositionList)
        {
            float distanceToWaterSource = Mathf.Abs(Vector3.Distance(waterSourceposition, transform.position));
            //find closest watersource
            if (distanceToWaterSource < distanceToNearestWaterSource)
            {
                distanceToNearestWaterSource = distanceToWaterSource; 
                nearestWaterSourcePosition = waterSourceposition;
            }
        }
        //move to watersource

        //return nearestWaterSourcePosition;
        Debug.Log("Watersource position: " + waterSourcePositionList[0]);
        return nearestWaterSourcePosition;
    }

    public bool hasFoundWaterSource()
    {
        return waterSourcePositionList.Count > 0;
    }

    
    public void drinkWater()
    {
        isDrinking = true;
        if (drinkTimer > 0.5f)
        {
            drink(5);
            drinkTimer = 0f;
            thirstBar.setValue(currentThirst);
        }
        if (currentThirst > 99)
        {
            isThirsty = false;
            isDrinking = false;
        }

    }
    void Update(){
    timePassed += Time.deltaTime;
    if(timePassed > 2f)
    {
      if(isDrinking==false){
        if(hungerBar.slider.value==0){
          changeBar(healthBar,10,ref currentHealth,"minus");
        }
        if(thirstBar.slider.value==0){
          changeBar(healthBar,10,ref currentHealth,"minus");
        }
        changeBar(hungerBar,10, ref currentHunger,"minus");
        changeBar(thirstBar,15, ref currentThirst,"minus");
        changeBar(hornyBar,1,ref currentHorny,"plus");
      }
        timePassed=0f;  
      
    } 

    drinkTimer += Time.deltaTime;
    if (currentThirst < 50)
        {
           Debug.Log("fox is thirsty");
            isThirsty = true;
        }
    }
}


