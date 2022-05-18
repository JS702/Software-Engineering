using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fox : Animal
{
    private Movement movement;
    public int killedHares = 0;
    void Start()
    {
        movement = gameObject.GetComponent<Movement>();
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
        hare.GetComponent<Movement>().agent.isStopped = true;

    }
    public IEnumerator fillStomach(Hare hare)
    {
        
        while (hunger < 100)
        {
            if(hare.nutritionalValue == 1){
                break;
            }
            hunger++;

            yield return new WaitForSeconds(1.0f);
        }
        isEating = false;
        isHungry = false;
        movement.agent.isStopped = false;
       
        
    }

    public void eatHare(Hare hare){

        isEating = true;
        movement.agent.SetDestination(hare.transform.position);
        new WaitForSeconds(2f);

        if(Vector3.Distance(transform.position,hare.transform.position) < 3){
            movement.agent.isStopped = true;
            StartCoroutine(fillStomach(hare));
        }
    }
    void Update()
    {
        base.Update();
        
        drinkTimer += Time.deltaTime;
        if (currentThirst < 50)
        {
            isThirsty = true;
        }
        if (hunger < 50)
        {
            isHungry = true;
        }
    }
}


