using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : Food
{

    public Animal animal;


    //Event Methods 
    public delegate void gotKilled();
    public static event gotKilled OnGotKilled;

    public int health;
    public int hunger;
    public int thirst;
    public int reproductionDrive;

    public int generation = 0;

    public string gender;

    public bool isAlive = true;
    public bool isEating;
    public bool isHungry;
    public bool isThirsty;
    public bool isDrinking;
    public bool isInWaterArea;
    public bool isUnderwater;
    public bool isHorny;
    public bool isPregnant;
    public bool isLookingForSex;
    public bool isHavingAReallyGoodTime;
    public bool isChild;

    public int speed;

    public Vector3 waterPosition = new Vector3(43.8f, 1.3f, 71.4f);

    //BARS Start:
    public int currentHealth;
    public int currentHunger;
    public int currentThirst;
    public int currentHorny;

    public int hungerLoss;
    public int thirstLoss;
    public int hornyLoss;

    public int hungerGain;
    public int thirstGain;
   

    public Bars healthBar;
    public Bars hungerBar;
    public Bars thirstBar;
    public Bars hornyBar;
    protected bool updatingBars = false;

    public float timePassed = 0f;
    public float eatTimer = 0f;
    public float drinkTimer = 0f;
    public float sexTimer = 0f;
    public float ageTimer = 0f;
    public GameObject babyPrefab; //Prefab vom Hare (manuell über die grafische Oberfläche reinziehen)
    public Animal myFather;
    public ParticleSystem heartParticle;

    public string animalName;
    private string[] namesMale = { "Bob", "Dave", "Ted", "Marvin", "Oscar", "Victor" };
    private string[] namesFemale = { "Alice", "Carol", "Eve", "Mallory", "Peggy", "Trudy" };

    protected void Update()
    {
        if (!updatingBars)
        {
            StartCoroutine(updateBars());
            isHorny = stillHorny();
            isLookingForSex = stillHorny();
        }


    }

    protected void setRandomName()
    {
        if (gender.Equals("male"))
        {
            animalName = namesMale[Random.Range(0, namesMale.Length)];
        }
        else
        {
            animalName = namesFemale[Random.Range(0, namesFemale.Length)];
        }
        this.name = this.tag + "_" + animalName;
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
            die(false);
        }

        changeBar(hungerBar, hungerLoss, ref currentHunger, "minus");
        changeBar(thirstBar, thirstLoss, ref currentThirst, "minus");

        if (!isPregnant && !isChild && currentHorny < reproductionDrive && !isHungry && !isThirsty)
        {
            changeBar(hornyBar, 1, ref currentHorny, "plus");
        }
        yield return new WaitForSeconds(3f);
        updatingBars = false;
    }

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

    private int mutate()
    {
        int mutation = Random.Range(0, 100);
        //Debug.Log("MUTATION" + mutation);
        return mutation > 1 ? 0 : mutation == 1 ? Random.Range(1, 3) : Random.Range(-3, 0);
    }

    private Animal setBabyValues(Animal male, Animal female, GameObject child)
    {
        Animal baby = child.GetComponent<Animal>();
        
        Debug.Log("FAMILIEN PAPA: " + male.animalName);
        Debug.Log("FAMILIEN MAMA: " + female.animalName);
        Debug.Log("FAMILIEN ICH: " + baby.animalName);

        baby.myFather = male;

        baby.GetComponent<Animal>().generation = male.generation >= female.generation ? male.generation + 1 : female.generation + 1;
        baby.GetComponent<Animal>().isPregnant = false;
        baby.GetComponent<Animal>().isChild = true;

        baby.GetComponent<Animal>().health = (male.health + female.health) / 2 + mutate() < 1 ? 1 : (male.health + female.health) / 2 + mutate();
        baby.GetComponent<Animal>().hunger = (male.hunger + female.hunger) / 2 + mutate() < 1 ? 1 : (male.hunger + female.hunger) / 2 + mutate();
        baby.GetComponent<Animal>().thirst = (male.thirst + female.thirst) / 2 + mutate() < 1 ? 1 : (male.thirst + female.thirst) / 2 + mutate();
        baby.GetComponent<Animal>().reproductionDrive = (male.reproductionDrive + female.reproductionDrive) / 2 + mutate() < 1 ? 1 : (male.reproductionDrive + female.reproductionDrive) / 2 + mutate();

        baby.GetComponent<Movement>().normalSpeed = (male.GetComponent<Movement>().normalSpeed + female.GetComponent<Movement>().normalSpeed) / 2 + mutate() < 1 ? 1 : (male.GetComponent<Movement>().normalSpeed + female.GetComponent<Movement>().normalSpeed) / 2 + mutate();
        baby.GetComponent<Movement>().sprintSpeed = (male.GetComponent<Movement>().sprintSpeed + female.GetComponent<Movement>().sprintSpeed) / 2 + mutate() < 1 ? 1 : (male.GetComponent<Movement>().sprintSpeed + female.GetComponent<Movement>().sprintSpeed);

        baby.GetComponent<Animal>().hungerLoss = (male.hungerLoss + female.hungerLoss) / 2 + mutate() < 1 ? 1 : (male.hungerLoss + female.hungerLoss) / 2 + mutate();
        baby.GetComponent<Animal>().thirstLoss = (male.thirstLoss + female.thirstLoss) / 2 + mutate() < 1 ? 1 : (male.thirstLoss + female.thirstLoss) / 2 + mutate();
        baby.GetComponent<Animal>().hornyLoss = (male.hornyLoss + female.hornyLoss) / 2 + mutate() < 1 ? 1 : (male.hornyLoss + female.hornyLoss) / 2 + mutate();

        baby.GetComponent<Animal>().hungerGain = (male.hungerGain + female.hungerGain) / 2 + mutate() < 1 ? 1 : (male.hungerGain + female.hungerGain) / 2 + mutate();
        baby.GetComponent<Animal>().thirstGain = (male.thirstGain + female.thirstGain) / 2 + mutate() < 1 ? 1 : (male.thirstGain + female.thirstGain) / 2 + mutate();
        
        baby.GetComponentInChildren<SphereCollider>().radius = (male.GetComponentInChildren<SphereCollider>().radius + female.GetComponentInChildren<SphereCollider>().radius) / 2 + mutate() < 1 ? 1 : (male.GetComponentInChildren<SphereCollider>().radius + female.GetComponentInChildren<SphereCollider>().radius) / 2 + mutate();
        
        baby.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        return baby;
    }
    
    protected IEnumerator grow(){
        
        yield return new WaitForSeconds(15);
        transform.localScale += new Vector3(0.5f,0.5f,0.5f);
        isChild = false;
    }
    public bool isHavingFun()
    {
        Animal male = this;
        Animal myFemale = GetComponent<HareMovement>().closestSexPartner;

        Debug.Log("SexMale: " + male);
        Debug.Log("SexFemale" + myFemale);

        isHavingAReallyGoodTime = true;

        myFemale.isHavingAReallyGoodTime = true;
        //GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().isHavingAReallyGoodTime = true;

        //GetComponent<HareMovement>().closestSexPartner.GetComponent<HareMovement>().agent.isStopped = true;
        myFemale.GetComponent<Movement>().agent.isStopped = true;


        if (sexTimer > 0.5f)
        {
            haveSex(hornyLoss);
            //GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().haveSex(hornyLoss);
            myFemale.haveSex(hornyLoss);

            sexTimer = 0f;

            hornyBar.setValue(currentHorny);
            //GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().hornyBar.setMaxValue(currentHorny);
            myFemale.hornyBar.setMaxValue(currentHorny);
        }
        
        if (currentHorny <= 0)
        {
            //heartParticel.Stop();
            isHavingAReallyGoodTime = false;
            //GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().isHavingAReallyGoodTime = false;
            myFemale.isHavingAReallyGoodTime = false;


            //GetComponent<HareMovement>().closestSexPartner.GetComponent<HareMovement>().agent.isStopped = false;
            myFemale.GetComponent<Movement>().agent.isStopped = false;


            currentHorny = 0;
            //GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().currentHorny = 0;
            myFemale.currentHorny = 0;

            isHorny = false;
            //GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().isHorny = false;
            myFemale.isHorny = false;
            
            //GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().isPregnant = true;
            myFemale.isPregnant = true;
            
            //GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().pregnancy(this.GetComponent<Hare>(), GetComponent<HareMovement>().closestSexPartner);
            Debug.Log("SexMale: " + male);
            Debug.Log("SexFemale" + myFemale);
            myFemale.pregnancy(male, myFemale);
            //Debug.Log("VERMEHRUNG -> Pregnant" + GetComponent<HareMovement>().closestSexPartner.GetComponent<Hare>().isPregnant);

        }
        return isHavingAReallyGoodTime;
    }

    IEnumerator spawnChild(Animal male, Animal female)
    {
         Vector3 pos = GetComponent<Movement>().transform.position;
        GetComponent<Movement>().agent.isStopped = true;
        //isPregnant = false;
        Debug.Log("VERMEHRUNG: Try to spawn child");
        int childCounter = Random.Range(3, 8); //Random Integer zwischen 1 und 3, der die Anzahl der zu spawnenden Kinder angibt
        yield return new WaitForSeconds(5f);

       
        for (int i = 1; i <= childCounter; i++) //Entsprechende Anzahl von Kindern wird gespawnt
        {
            /**
            hareBaby.GetComponent<Animal>().hunger = 500; //Test, funktioniert so
            hareBaby.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); //Test, funktioniert so
            Instantiate(hareBaby, pos + new Vector3(i, 0, i), Quaternion.identity); //Jedes gespawnte Kind wird um eine Einheit weiter auf der x- und z-Achse verschoben, um Kollisionen zu verhindern
            */
            
            Instantiate(setBabyValues(male, female, babyPrefab), pos + new Vector3(i, 0, i), Quaternion.identity);

            Debug.Log("VERMEHRUNG: child spawned");
        }

        isPregnant = false;
        
        GetComponent<Movement>().agent.isStopped = false;
    }

    public void pregnancy(Animal male, Animal female)
    {
        StartCoroutine(spawnChild(male, female));
    }

    public bool drinkWater()
    {
        isDrinking = true;
        if (drinkTimer > 0.5f)
        {
            drink(thirstGain);
            drinkTimer = 0f;
            thirstBar.setValue(currentThirst);
        }
        if (currentThirst > thirst - 1)
        {
            isThirsty = false;
            isDrinking = false;
        }
        return isDrinking;
    }

    protected void setGenerationZeroValues(){

        GetComponentInChildren<SphereCollider>().radius = Random.Range(5,10);
        health = Random.Range(100, 200);
        hunger = Random.Range(100, 200);
        thirst = Random.Range(100, 200);
        reproductionDrive = Random.Range(11,21);
        
        hungerLoss = Random.Range(2, 6);
        thirstLoss = Random.Range(2, 6);
        hornyLoss = Random.Range(1, 5);

        hungerGain = Random.Range(5, 16);
        thirstGain = Random.Range(5, 16);
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

    public abstract string getAnimalInfo();
    


    public void TestInputs()
    {

        //Debug-Tool, bis der Hase hunger bekommen und fressen kann
        if (Input.GetKeyDown("h"))
        {
            currentHunger = 10;
            //Debug.Log(currentHunger);
        }
        if (Input.GetKeyDown("t"))
        {
            currentHunger = 100;
            //Debug.Log(currentHunger);
        }
        if (Input.GetKeyDown("k"))
        {
            //Denkt daran den Agent zu stoppen wenn ihr die die-Methode aufruft, ich konnte aus Animal nicht darauf zugreifen
            GetComponent<Movement>().agent.isStopped = true;
            die(false);

        }
        if (Input.GetKeyDown("l"))
        {
            currentThirst = 10;
        }

    }

     bool stillHorny(){
        if(isHungry || isThirsty){
            //hornyBar.slider.value = 0;
            currentHorny = 0;
            isHorny = false;
        }
        return isHorny;
    }

}
