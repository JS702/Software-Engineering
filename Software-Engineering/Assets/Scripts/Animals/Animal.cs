using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Food
{
    public int thirst = 100;
    public int hunger = 100;
    public int reproductionDrive = 0;
    public int speed;
    public int health;


    public void eat(int food)
    {
        hunger += food;
    } 
    public void drink(int water)
    {
        thirst += water;
    } 
    
}
