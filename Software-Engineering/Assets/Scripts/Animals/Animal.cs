using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Food
{
    public int health=100;
    public int hunger = 100;

    public int thirst = 100;
    public int reproductionDrive = 5;

    
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
    
}
