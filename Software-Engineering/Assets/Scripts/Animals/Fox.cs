using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: später wechseln zu Animal erben!!!!
public class Fox : Animal
{
    
    public int currentHunger;
    public HungerBar hungerBar;

    float timePassed = 0f;


    void TakeDamage(int damage){
        currentHunger-=damage;

        hungerBar.setHunger(currentHunger);
    }
    
    void Start()
    {
        currentHunger=hunger;
        hungerBar.setMaxHunger(hunger);

    }

    void Update(){
    timePassed += Time.deltaTime;
    if(timePassed > 2f)
    {
         TakeDamage(10);
         timePassed=0f;
    } 
    }
    
    
}


