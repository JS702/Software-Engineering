using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Food
{

    //Event Methods 
    public delegate void gotKilled();
    public static event gotKilled OnGotKilled;

    public int health=100;
    public int hunger = 100;

    public int thirst = 100;
    public int reproductionDrive = 5;

    public bool isAlive = true;
    public bool isEating;
    
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

    public float timePassed = 0f;
    public float eatTimer = 0f;

    public void changeBar(Bars bar,int damage, ref int currentNumber,string operations){
        
        currentNumber= operations.Equals("plus") ? currentNumber+=damage : currentNumber-=damage;
        bar.setValue(currentNumber);
    }

    public void setBar(ref int currentNumber, int number, Bars bar){
        currentNumber=number;
        bar.setMaxValue(number);
    }

    //BARS End

    public void eat(int food)
    {
        hunger += food;
    } 
    public void drink(int water)
    {
        thirst += water;
    }
    
    public int getHunger()
    {
        return hunger;
    }

    public void getKilled()
    {
        OnGotKilled?.Invoke();
    }

    //Wenn der Methode "true" übergeben wird, so verschwindet die Leiche, nachdem die Sterbeanimation durchgelaufen ist
    //Andernfalls bleibt sie Liegen (z.B. falls sie noch gefressen werden soll)
    public void die(bool instantDespawn)
    {
        if (instantDespawn)
        {
            isAlive = false;
            Destroy(this.gameObject, 5f);
        } else
        {
            isAlive = false;
        }
    }

}
