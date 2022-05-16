using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hare : Animal
{
    public bool isHungry;
    public bool isFleeing = false;
    public List<Vector3> grassPositionList;

    public void addGrassToList(Collider col)
    {
        Debug.Log("Grass in sight");
        GameObject Grass = col.gameObject;
        Vector3 grassPosition = Grass.transform.position;
        if (!grassPositionList.Contains(grassPosition))
        {
            grassPositionList.Add(grassPosition);
        }

    }

    public Vector3 moveToNearestGrass()
    {
        Debug.Log("Dichtestes Gras wird gesucht...");
        //Default: Dichtestes Gras ist das, das er zuerst entdeckt hat
        float distanceToNearestGrass = Vector3.Distance(grassPositionList[0], transform.position);
        Vector3 nearestGrassPosition = grassPositionList[0];
        foreach (Vector3 grassPosition in grassPositionList)
        {
            float distanceToGrass = Mathf.Abs(Vector3.Distance(grassPosition, transform.position));
            //Dichtestes Gras finden
            if (distanceToGrass < distanceToNearestGrass)
            {
                distanceToNearestGrass = distanceToGrass;
                nearestGrassPosition = grassPosition;
            }
        }
        //Zum dichtesten bekannten Gras laufen
        Debug.Log("Zum Gras bewegen");
        return nearestGrassPosition;
    }

    public bool hasFoundGrass()
    {
        return grassPositionList.Count > 0;
    }

    //W�hrend der Hase hungrig ist (Hunger < 50), frisst er jede halbe Sekunde einen Nahrungspunkt
    public void eatGrass()
    {
        isEating = true;
        if (eatTimer > 0.5f)
        {
            eat(10);
            eatTimer = 0f;
            hungerBar.setValue(currentHunger);
        }
        if (currentHunger > 99)
        {
            isHungry = false;
            isEating = false;
        }
        Debug.Log("isEating: " + isEating);
    }

    void Start()
    {
        setBar(ref currentHealth, health, healthBar);
        setBar(ref currentHunger, hunger, hungerBar);
        setBar(ref currentThirst, thirst, thirstBar);
        
        hornyBar.slider.maxValue=5;
        hornyBar.slider.value=0;
        currentHorny=0;

    }

    void Update(){
    timePassed += Time.deltaTime;
        if(timePassed > 2f)
        {
            if(isEating==false){
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
    void Update()
    {
        base.Update();// updates the bars
        eatTimer += Time.deltaTime;

        if (currentHunger < 50)
        {
            isHungry = true;
        }
    }
}
}
