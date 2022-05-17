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

    public void kill(GameObject hare){

        hare.GetComponent<Animal>().isAlive = false;
        gameObject.GetComponent<FoxCollider>().preyList.Remove(hare);
        hare.GetComponent<Animal>().die(false);
        //hare.GetComponent<Animal>().die(false);
        //GameObject hare = gameObject.GetComponent<foxMovement>().hare;
        //Vector3 harePosition = gameObject.GetComponent<foxMovement>().hare.transform.position;
        //Vector3 foxPosition = transform.position;
        //float _distanceToHare = Vector3.Distance(foxPosition, harePosition);

        //if(_distanceToHare < 3)
        //{
        //Destroy(hare);
        //}
    }
}


