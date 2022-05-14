using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fox : Animal
{

    public int killedHares = 0;
    void Start()
    {
        setBar(ref currentHealth, health, healthBar);
        setBar(ref currentHunger, hunger, hungerBar);
        setBar(ref currentThirst, thirst, thirstBar);
        
        hornyBar.slider.maxValue=5;
        hornyBar.slider.value=0;
        currentHorny=0;

    }
}


