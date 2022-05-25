using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hare_ : Animal_
{   
    public bool inDanger = false;
    public bool isFleeing = false;
    public bool isInGrassArea;
    public List<Vector3> grassPositionList;

    public void addGrassToList(Collider col)
    {
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

    public bool eatGrass()
    {
        isEating = true;
        if (eatTimer > 0.5f)
        {
            eat(20);
            eatTimer = 0f;
            hungerBar.setValue(currentHunger);
        }
        if (currentHunger > 99)
        {
            isHungry = false;
            isEating = false;
        }
        return isEating;
    }

    void Start()
    {
        gender = Random.Range(0,2) == 1 ? "male" : "female";

        setBar(ref currentHealth, health, healthBar);
        setBar(ref currentHunger, hunger, hungerBar);
        setBar(ref currentThirst, thirst, thirstBar);

        hornyBar.slider.maxValue = 5;
        hornyBar.slider.value = 0;
        currentHorny = 0;

    }

    bool stillHorny(){
        if(isHungry || isThirsty){
            //hornyBar.slider.value = 0;
            currentHorny = 0;
            isHorny = false;
        }else{
            isHorny = true;
        } 
        return isHorny;
    }
    
    void Update()
    {
        base.Update();// updates the bars
        eatTimer += Time.deltaTime;
        drinkTimer += Time.deltaTime;
        sexTimer += Time.deltaTime;
        if (currentHorny == 5)
        {
            isHorny = true;
        }
        if (currentHunger < 50)
        {
            isHungry = true;
        }
        if (currentThirst < 50)
        {
            isThirsty = true;
        }
        stillHorny();
    }

}