using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fox : Animal
{
    
    public int currentHealth;
    public int currentHunger;
    public int currentThirst;
    public int currentHorny;

    public Bars healthBar;
    public Bars hungerBar;
    public Bars thirstBar;
    public Bars hornyBar;

    float timePassed = 0f;


    public void TakeDamage(Bars bar,int damage){
        currentHealth-=damage;
        bar.setValue(currentHealth);
    }
    public void TakeHunger(Bars bar,int damage){
        currentHunger-=damage;
        bar.setValue(currentHunger);
    }
    public void TakeThirst(Bars bar,int damage){
        currentThirst-=damage;
        bar.setValue(currentThirst);
    }
    public void TakeHorny(Bars bar,int damage){
        currentHorny+=damage;
        bar.setValue(currentHorny);
    }


    
    void Start()
    {
        currentHealth=health;
        healthBar.setMaxValue(health);
        currentHunger=hunger;
        hungerBar.setMaxValue(hunger);
        currentThirst=thirst;
        thirstBar.setMaxValue(thirst);
        
        hornyBar.slider.maxValue=5;
        hornyBar.slider.value=0;
        currentHorny=0;

    }

    void Update(){
    timePassed += Time.deltaTime;
    if(timePassed > 2f)
    {
        if(hungerBar.slider.value==0){
          TakeDamage(healthBar,10);
        }
        if(thirstBar.slider.value==0){
          TakeDamage(healthBar,10);
        }
        TakeHunger(hungerBar,10);
        TakeThirst(thirstBar,15);
       
        Debug.Log("2 sec passed ");
        timePassed=0f;
    } 
    }
    
    
}


