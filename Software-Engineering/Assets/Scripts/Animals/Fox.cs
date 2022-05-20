using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fox : Animal
{

    private Movement movement;
    public int killedHares = 0;
    public int killRange = 4;
    void Start()
    {
        movement = gameObject.GetComponent<Movement>();
        setBar(ref currentHealth, health, healthBar);
        setBar(ref currentHunger, hunger, hungerBar);
        setBar(ref currentThirst, thirst, thirstBar);

        hornyBar.slider.maxValue = 5;
        hornyBar.slider.value = 0;
        currentHorny = 0;
    }

    public void kill(GameObject hare)
    {
        
        //if(!hare.GetComponent<Movement>().agent.isStopped){
        hare.GetComponent<HareMovement>().agent.isStopped = true;
        // }
        hare.GetComponent<Hare>().isAlive = false;
        hare.GetComponent<Hare>().die(false);

       


    }
    public IEnumerator fillStomach(GameObject hare)
    {
        
        while (currentHunger < 100)
        {
            GetComponent<foxMovement>().isHunting = false;
            if (hare != null)
            {
                if (hare.GetComponent<Hare>().nutritionalValue == 1)
                {
                    break;
                }

            }

            currentHunger += 10;

            yield return new WaitForSeconds(1.0f);
        }
        isEating = false;
        isHungry = false;
        movement.agent.isStopped = false;
        gameObject.GetComponent<FoxCollider>().preyList.Remove(hare);


    }

    public void eatHare(GameObject hare)
    {
        isEating = true;
        movement.agent.SetDestination(hare.transform.position);
        movement.agent.isStopped = true;
        StartCoroutine(fillStomach(hare));
       
    }
    void Update()
    {
        base.Update();

        drinkTimer += Time.deltaTime;
        if (currentThirst < 50)
        {
            isThirsty = true;
        }
        if (currentHunger < 50)
        {
            isHungry = true;
        }
    }
}


