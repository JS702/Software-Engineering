using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fox : Animal
{
    
    
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
        if(hungerBar.slider.value==0){
          changeBar(healthBar,10,ref currentHealth,"minus");
        }
        if(thirstBar.slider.value==0){
          changeBar(healthBar,10,ref currentHealth,"minus");
        }
        changeBar(hungerBar,10, ref currentHunger,"minus");
        changeBar(thirstBar,15, ref currentThirst,"minus");
        changeBar(hornyBar,1,ref currentHorny,"plus");
        Debug.Log("2 sec passed ");
    
        timePassed=0f;  
    } 
    }
}


