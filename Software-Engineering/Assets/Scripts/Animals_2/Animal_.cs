using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal_ : Food
{
    public Animal_ animal;

    //Event Methods 
    public delegate void gotKilled();
    public static event gotKilled OnGotKilled;

    public int health = 100;
    public int hunger = 100;
    public int thirst = 100;
    public int reproductionDrive = 5;

    public string gender;

    public bool isAlive = true;
    public bool isEating;
    public bool isHungry;
    public bool isThirsty;
    public bool isDrinking;
    public bool isInWaterArea;
    public bool isHorny;
    public bool isPregnant;
    public bool isLookingForSex;
    public bool isHavingAReallyGoodTime;
    public bool isUnderwater;
    public int speed;

    public Vector3 waterPosition = new Vector3(43.8f, 1.3f, 71.4f);

    //BARS Start:
    public int currentHealth;
    public int currentHunger;
    public int currentThirst;
    public int currentHorny;

    public Bars healthBar;
    public Bars hungerBar;
    public Bars thirstBar;
    public Bars hornyBar;
    protected bool updatingBars = false;

    public float timePassed = 0f;
    public float eatTimer = 0f;
    public float drinkTimer = 0f;
    public float sexTimer = 0f;
    public float pregnancyTimer = 0f;

    public GameObject baby;



    protected void Update()
    {
        if (isPregnant)
        {
            pregnancyTimer += Time.deltaTime;
            if (pregnancyTimer > 5)
            {
                Debug.Log("NEW LIFE");
                pregnancyTimer = 0;
                isPregnant = false;
            }
        }
        if (!updatingBars)
        {
            StartCoroutine(updateBars());
        }
    }

    public void changeBar(Bars bar, int damage, ref int currentNumber, string operations)
    {

        currentNumber = operations.Equals("plus") ? currentNumber += damage : currentNumber -= damage;
        bar.setValue(currentNumber);
        if (currentNumber < 0)
        {
            currentNumber = 0;
        }
    }

    public void setBar(ref int currentNumber, int number, Bars bar)
    {
        currentNumber = number;
        bar.setMaxValue(number);
    }

    protected IEnumerator updateBars()
    {
        updatingBars = true;
        if (thirstBar.slider.value == 0)
        {
            changeBar(healthBar, 10, ref currentHealth, "minus");
        }
        if (hungerBar.slider.value == 0)
        {
            changeBar(healthBar, 10, ref currentHealth, "minus");
        }
        if (healthBar.slider.value == 0)
        {
            GetComponent<Movement>().agent.isStopped = true;
            die(false);
        }

        changeBar(hungerBar, 5, ref currentHunger, "minus");
        changeBar(thirstBar, 5, ref currentThirst, "minus");

        if (!isPregnant && currentHorny < reproductionDrive)
        {
            changeBar(hornyBar, 1, ref currentHorny, "plus");
        }


        yield return new WaitForSeconds(3f);
        updatingBars = false;
    }

    //BARS End

    public void eat(int food)
    {
        currentHunger += food;
    }
    public void drink(int water)
    {
        currentThirst += water;
    }

    public void haveSex(int endurance)
    {
        currentHorny -= endurance;
    }

    public int getHunger()
    {
        return hunger;
    }

    GameObject produceNewLife(GameObject male, GameObject female)
    {

        return baby;

    }

    public bool isHavingFun(GameObject male, GameObject female)
    {

        //male.isHavingAReallyGoodTime = true;
        isHavingAReallyGoodTime = true;
        GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().isHavingAReallyGoodTime = true;

        GetComponent<HareMovement>().closestSexPartner.GetComponent<HareMovement>().agent.isStopped = true;
        //GetComponent<Movement>().agent.isStopped = true;


        if (sexTimer > 0.5f)
        {
            haveSex(2);
            gameObject.GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().haveSex(2);
            sexTimer = 0f;

            hornyBar.setValue(currentHorny);
            gameObject.GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().hornyBar.setMaxValue(currentHorny);
        }
        if (currentHorny <= 0)
        {
            currentHorny = 0;
            GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().currentHorny = 0;

            isHavingAReallyGoodTime = false;
            isHorny = false;
            //GetComponent<Movement>().agent.isStopped = false;

            GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().isHorny = false;
            GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().isHavingAReallyGoodTime = false;

            GetComponent<HareMovement>().closestSexPartner.GetComponent<HareMovement>().agent.isStopped = false;
            GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().isPregnant = true;
        }
        return isHavingAReallyGoodTime;
    }

    public bool drinkWater()
    {
        isDrinking = true;
        if (drinkTimer > 0.5f)
        {
            drink(20);
            drinkTimer = 0f;
            thirstBar.setValue(currentThirst);
        }
        if (currentThirst > 99)
        {
            isThirsty = false;
            isDrinking = false;
        }
        return isDrinking;
    }

    public void getKilled()
    {
        OnGotKilled?.Invoke();
    }

    //Wenn der Methode "true" �bergeben wird, so verschwindet die Leiche, nachdem die Sterbeanimation durchgelaufen ist
    //Andernfalls bleibt sie Liegen (z.B. falls sie noch gefressen werden soll) und verliert pro Sekunde einen N�hrwertpunkt
    public void die(bool instantDespawn)
    {
        GetComponent<Movement>().agent.isStopped = true;

        if (instantDespawn)
        {
            Destroy(this.gameObject, 5f);
        }
        else
        {
            StartCoroutine(decreaseNutritionalValue());
        }
        isAlive = false;
    }

    public IEnumerator getOutOfWater()
    {
        if (transform.position.z > 71)
        {
            if (transform.position.x > 43)
            {
                //Tier ist im rechten oberen Viertel
                GetComponent<Movement_>().agent.SetDestination(new Vector3(100f, 0f, 100f));
            }
            else
            {
                //Tier ist im linken oberen Viertel
                GetComponent<Movement_>().agent.SetDestination(new Vector3(0f, 0f, 100f));
            }
        }
        else
        {
            if (transform.position.x > 43)
            {
                //Tier ist im rechten unteren Viertel
                GetComponent<Movement_>().agent.SetDestination(new Vector3(100f, 0f, 0f));
            }
            else
            {
                //Tier ist im linken unteren Viertel
                GetComponent<Movement_>().agent.SetDestination(new Vector3(0f, 0f, 0f));
            }
        }

        yield return new WaitForSeconds(2.0f);
        isUnderwater = false;
    }



}
