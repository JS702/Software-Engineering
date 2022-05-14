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
            Debug.Log("Grass added to list" + grassPosition.ToString());
        }
        else
        {
            Debug.Log("Grass already in list");
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

    //Während der Hase hungrig ist (Hunger < 50), frisst er jede halbe Sekunde einen Nahrungspunkt
    public void eatGrass()
    {
        isEating = true;
        if (eatTimer > 0.5f)
        {
            eat(1);
            eatTimer = 0f;
        }
        if (hunger > 99)
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

    void Update()
    {
        base.Update();// updates the bars
        eatTimer += Time.deltaTime;

        if (hunger < 50)
        {
            isHungry = true;
        }
    }
}
