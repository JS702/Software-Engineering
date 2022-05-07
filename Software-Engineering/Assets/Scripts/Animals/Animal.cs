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
    
    public int speed;
    


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

}
