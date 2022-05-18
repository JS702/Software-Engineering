using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Food
{
    
    public Animal animal;
    //Event Methods 
    public delegate void gotKilled();
    public static event gotKilled OnGotKilled;

    public int health=100;
    public int hunger = 100;
    
    public int thirst = 100;
    public int reproductionDrive = 5;

    public bool isAlive = true;
    public bool isEating;
    public bool isHungry;
    public bool isDrinking;
    
    public int speed;
    
    //BARS Start:
    public int currentHealth;
    public int currentHunger;
    public int currentThirst;
    public int currentHorny;

    public Bars healthBar;
    public Bars hungerBar;
    public Bars thirstBar;
    public Bars hornyBar;
    protected bool updatingBars = false;

    public float timePassed = 0f;
    public float eatTimer = 0f;
    public float drinkTimer = 0f;


    protected void Update()
    {
        
        if (!updatingBars)
        {
            StartCoroutine(updateBars());
        }
    }

    public void changeBar(Bars bar,int damage, ref int currentNumber,string operations){
        
        currentNumber= operations.Equals("plus") ? currentNumber+=damage : currentNumber-=damage;
        bar.setValue(currentNumber);
        if (currentNumber < 0) {
                  currentNumber = 0;
                }
    }

    public void setBar(ref int currentNumber, int number, Bars bar){
        currentNumber=number;
        bar.setMaxValue(number);
    }

    protected IEnumerator updateBars()
    {
        updatingBars = true;
        if (thirstBar.slider.value == 0)
        {
            changeBar(healthBar, 10, ref currentHealth, "minus");
        }
        changeBar(hungerBar, 10, ref currentHunger, "minus");
        changeBar(thirstBar, 15, ref currentThirst, "minus");
        changeBar(hornyBar, 1, ref currentHorny, "plus");

        yield return new WaitForSeconds(3f);
        updatingBars = false;
    }

  

    //BARS End

    public void eat(int food)
    {
        currentHunger+=food;
    } 
    public void drink(int water)
    {
        currentThirst += water;
    }
    
    public int getHunger()
    {
        return hunger;
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
        Debug.Log("Closest watersource...");

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
        Debug.Log("move to water");
        return waterSourcePositionList[0];
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
            drink(20);
            drinkTimer = 0f;
            thirstBar.setValue(currentThirst);
        }
        if (currentThirst > 99)
        {
            isThirsty = false;
            isDrinking = false;
        }
    }

    public void getKilled()
    {
        OnGotKilled?.Invoke();
    }

    //Wenn der Methode "true" �bergeben wird, so verschwindet die Leiche, nachdem die Sterbeanimation durchgelaufen ist
    //Andernfalls bleibt sie Liegen (z.B. falls sie noch gefressen werden soll) und verliert pro Sekunde einen N�hrwertpunkt
    public void die(bool instantDespawn)
    {
        if (instantDespawn)
        {
            Destroy(this.gameObject, 5f);
        }
        else
        {
            StartCoroutine(decreaseNutritionalValue());
        }
        isAlive = false;
    }

}
