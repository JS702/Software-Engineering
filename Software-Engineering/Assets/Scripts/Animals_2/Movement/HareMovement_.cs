using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HareMovement_ : Movement_
{
    //reference to the hare itself
    public Hare_ hare;
    public GameObject hareGameObject;
    public AnimalCollider_ animalCollider;

    public GameObject closestFox;
    public GameObject closestSexPartner;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        hare = GetComponent<Hare_>();
        hareGameObject = this.gameObject;
        animalCollider = GetComponent<AnimalCollider_>();
    }

    private void Update()
    {

        if (hare.inDanger)
        {
            escape();

        }
        else
        {
            // EATING
            if (hare.isHungry && hare.hasFoundGrass() && !hare.isFleeing && !hare.isDrinking && !hare.isUnderwater && !hare.isHavingAReallyGoodTime)
            {
                agent.SetDestination(hare.moveToNearestGrass());
                if (hare.isInGrassArea)
                {
                    agent.isStopped = hare.eatGrass();
                }
            }
            // DRINKING
            if (hare.isThirsty && !hare.isFleeing && !hare.isEating && !hare.isUnderwater)
            {
                agent.SetDestination(hare.waterPosition);
                if (hare.isInWaterArea)
                {
                    agent.isStopped = hare.drinkWater();
                }
            }
            // HAVE SOME FUN
            if (hare.isHorny && !hare.isFleeing && !hare.isHungry && !hare.isThirsty && !hare.isUnderwater)
            {
                reproduce();
            }
            //SAVE FROM DROWNING
            if (hare.isUnderwater)
            {
                StartCoroutine(GetComponent<Animal_>().getOutOfWater());
            }
            if (!isWandering && !hare.isFleeing && !hare.isUnderwater && !hare.isHungry && !hare.isThirsty)
            {
                StartCoroutine(setWanderDestination());
            }
        }
        KeyInputs();
    }

    void KeyInputs()
    {
        //Debug-Tool, bis der Hase hunger bekommen und fressen kann
        if (Input.GetKeyDown("h"))
        {
            hare.currentHunger = 10;
            Debug.Log(hare.currentHunger);
        }
        if (Input.GetKeyDown("j"))
        {
            hare.currentHunger = 100;
            Debug.Log(hare.currentHunger);
        }
        if (Input.GetKeyDown("k"))
        {
            //Denkt daran den Agent zu stoppen wenn ihr die die-Methode aufruft, ich konnte aus Animal nicht darauf zugreifen
            agent.isStopped = true;
            hare.die(false);

        }
        if (Input.GetKeyDown("l"))
        {
            hare.currentThirst = 10;
        }
    }
   

    /*
    public void setLowestDistanceFox(Vector3 harePosition)
    {
        List<GameObject> foxList = GetComponent<hareCollider>().foxList;
        float _distanceToFox;
        float lowestDistance = 100;
        foreach (GameObject fox in foxList)
        {
            Vector3 foxPosition = fox.transform.position;
            _distanceToFox = Vector3.Distance(harePosition, foxPosition);

            if (_distanceToFox < lowestDistance)
            {
                //Der Fox der am dichtesten ist wird zum gameObject Fox vor dem der Hase wegrennt
                closestFox = fox;
                lowestDistance = _distanceToFox;
            }
        }
    }
    */

    /*
        public void setLowestDistanceSexPartner(Vector3 harePosition)
        {

            List<GameObject> potentialSexPartnerList = GetComponent<hareCollider>().potentialSexPartnerList;
            float _distanceToSexPartner;
            float lowestDistance = 100;
            foreach (GameObject sexPartner in potentialSexPartnerList)
            {
                Vector3 sexPartnerPosition = hare.transform.position;
                _distanceToSexPartner = Vector3.Distance(harePosition, sexPartnerPosition);

                if (_distanceToSexPartner < lowestDistance && !sexPartner.GetComponent<Hare>().isPregnant)
                {
                    //Der Fox der am dichtesten ist wird zum gameObject Fox vor dem der Hase wegrennt
                    closestSexPartner = sexPartner;
                    lowestDistance = _distanceToSexPartner;
                }
            }
        }
        */

    private void escape()
    {
        hare.isFleeing = true;
        agent.speed = sprintSpeed;

        try
        {
            closestFox = animalCollider.lowestDistanceAnimal(hareGameObject, GetComponent<HareCollider_>().foxList);

            if(closestFox != null){
                 agent.SetDestination(closestFox.transform.position);
            }
           
        }
        catch (MissingReferenceException)
        {
        }
        finally
        {
            
          // animalCollider.removeMissingObjectsFromAnimalList(GetComponent<HareCollider_>().foxList);
        }
    }


    private void reproduce()
    {

        //meine position
        hare.isLookingForSex = true;
        Vector3 harePosition = transform.position;

        //welches ist der naechste hase
        closestSexPartner = animalCollider.lowestDistanceAnimal(hareGameObject, animalCollider.potentialSexPartnerList);


        if (closestSexPartner != null)
        {                                                      // wheen there is a hare of another gender around
            if (Vector3.Distance(harePosition, closestSexPartner.transform.position) < 2 &&  // in range of a potential sex Partner
                                hare.gender.Equals("male") &&   
                                closestSexPartner.GetComponent<Animal_>().gender.Equals("female") &&                              // the active hare is male
                                closestSexPartner.GetComponent<Hare_>().isHorny &&           // the target hare isHorny
                                !closestSexPartner.GetComponent<Hare_>().isPregnant          // the target hare is NOT pregnant
                                )
            {
                hare.isLookingForSex = false;
                agent.isStopped = hare.isHavingFun(hareGameObject, closestSexPartner);                                       // start to have sex
                Debug.Log(" IM HAVING A REALLY GOOD TIME");
            }


            if (closestSexPartner.GetComponent<Hare_>().isPregnant)
            {                              // if the target is allready pregnant:
                GetComponent<HareCollider_>().potentialSexPartnerList.Remove(closestSexPartner); // remove it from potentialSexPartnerList
            }
            else
            {
                agent.SetDestination(closestSexPartner.transform.position);                     // else: run to the closestSexPartner
            }

        }


    }
}
