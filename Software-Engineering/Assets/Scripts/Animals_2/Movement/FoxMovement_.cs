using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxMovement_ : Movement_
{
    public Fox_ fox;
    public GameObject foxGameObject;
    private AnimalCollider_ animalCollider;
    public GameObject closestHare;
    private Vector3 closestHarePosition;
    private float _distanceToPrey;
    public bool isHunting = false;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fox = GetComponent<Fox_>();
        foxGameObject = this.gameObject;
        agent = GetComponent<NavMeshAgent>();
        animalCollider = GetComponent<AnimalCollider_>();
    }

    private void Update()
    {
        //HUNGRY
        if (fox.isHungry && !fox.isEating && !fox.isUnderwater)
        {
            if (GetComponent<FoxCollider_>().preyList.Count > 0)
            {
                hunt();
            }
        }

        //THIRSTY
        if (fox.isThirsty && !isHunting)
        {
            agent.SetDestination(fox.waterPosition);
            if (fox.isInWaterArea)
            {
                agent.isStopped = fox.drinkWater();
            }
        }

        //HORNY


        //DROWNING 
        if (fox.isUnderwater)
        {
            StartCoroutine(GetComponent<Animal_>().getOutOfWater());
        }

        // WANDER
        if (!isWandering && !isHunting && !fox.isEating && !fox.isUnderwater)
        {
            StartCoroutine(setWanderDestination());
        }

        KeyInputs();



    }

    private void KeyInputs()
    {
        if (Input.GetKeyDown("k"))
        {
            fox.die(true);
            //Denkt daran den Agent zu stoppen wenn ihr die die-Methode aufruft, ich konnte aus Animal nicht darauf zugreifen
            agent.isStopped = true;
        }

        if (Input.GetKeyDown("o"))
        {
            GetComponent<Fox_>().currentHunger -= 50;
        }
    }

    private void hunt()
    {
        isHunting = true;
        agent.speed = sprintSpeed;
        try
        {
            Vector3 foxPosition = transform.position;
            closestHare = animalCollider.lowestDistanceAnimal(foxGameObject, GetComponent<FoxCollider_>().preyList);

            // If the Hunted Animal is allready dead -> stop hunting and remove it from preyList
            if (closestHare.GetComponent<Animal_>().isAlive == false)
            {
                GetComponent<FoxCollider_>().preyList.Remove(closestHare);
                isHunting = false;
            }

            // is there anything to hunt?
            if (gameObject.GetComponent<FoxCollider_>().preyList.Count == 0)
            {
                isHunting = false;
            }
            closestHarePosition = closestHare.transform.position;
            _distanceToPrey = Vector3.Distance(foxPosition, closestHarePosition);
            //Debug.DrawLine(foxPosition, nearestHarePosition, Color.black);
            if (_distanceToPrey < fox.killRange)
            {
                isHunting = false;

                if (closestHare.GetComponent<Hare_>().isAlive)
                {
                    fox.kill(closestHare);
                    //StartCoroutine(runToDeadHare());
                    // TODO hier laufe zum hasen einfuegen
                    fox.eatHare(closestHare);
                }
            }
            //Tell Agent where to go
            agent.SetDestination(closestHarePosition);
        }
        catch (MissingReferenceException)
        {

        }
        finally
        {
            //check if there are any missing GameObjects in preyList and remove them
            GetComponent<AnimalCollider_>().removeMissingObjectsFromAnimalList(GetComponent<FoxCollider_>().preyList);
        }
    }
}
