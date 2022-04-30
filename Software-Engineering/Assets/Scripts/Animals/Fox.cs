using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: später wechseln zu Animal erben!!!!
public class Fox : MonoBehaviour
{
    public int hunger=100;
    public int currentHunger;
    public HungerBar hungerBar;
    void Start()
    {
        currentHunger=hunger;
        //hungerBar.setMaxHunger(hunger);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(20);       
        }
    }
    void TakeDamage(int damage){
        currentHunger-=damage;

        hungerBar.setHunger(currentHunger);
    }
}


