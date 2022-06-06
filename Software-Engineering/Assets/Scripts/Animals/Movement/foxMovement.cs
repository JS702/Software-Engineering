using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class foxMovement : Movement
{
    public Fox fox;

    //public NavMeshAgent _agent;
    // Hunting Variables

    //the Hare that the fox is Hunting
    public GameObject hare;
    private Vector3 nearestHarePosition;

    private Vector3 _huntDirection;

    private float _distanceToPrey;
    //Vector3 foxPosition = transform.position;
    public bool isHunting = false;
    public bool isUnderwater;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fox = GetComponent<Fox>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isUnderwater)
        {
            StartCoroutine(getOutOfWater());
        }
        //HUNTING
        if (fox.isHungry && !fox.isEating && !isUnderwater)
        {
            if (GetComponent<FoxCollider>().preyList.Count > 0)
            {
                isWandering = false;
                hunt();
            }
        }
        //DRINKING
        if (fox.isThirsty && !isHunting && !fox.isEating)
        {
            agent.SetDestination(fox.waterPosition);
            if (fox.isInWaterArea)
            {
                agent.isStopped = fox.drinkWater();
            }
        }
        //HORNY
        if (fox.isHorny && !fox.isHungry && !fox.isThirsty && !fox.isUnderwater)
        {
            reproduce();
        }

        if (!isWandering && !isHunting && !fox.isEating && !isUnderwater && !fox.isDrinking)
        {
            StartCoroutine(setWanderDestination());
        }

    }


    /*
        public void setLowestDistanceHare(Vector3 foxPosition)
        {
            List<GameObject> preyList = gameObject.GetComponent<FoxCollider>().preyList;
            float lowestDistance = 100;
            foreach (GameObject hare in preyList)
            {
                Vector3 harePosition = hare.transform.position;
                _distanceToPrey = Vector3.Distance(foxPosition, harePosition);

                if (_distanceToPrey < lowestDistance)
                {
                    //Der Fox der am dichtesten ist wird zum gameObject Fox vor dem der Hase wegrennt
                    this.hare = hare;
                    lowestDistance = _distanceToPrey;
                }
            }

        }
        */

    public Vector3 runToHare(Vector3 foxPosition, Animal hare)
    {
        //Look for nearest Fox
        Vector3 dirToHare = foxPosition - hare.transform.position;
        // Escape direction
        _huntDirection = foxPosition - (dirToHare).normalized;
        return _huntDirection;
    }

    private void hunt()
    {

        isHunting = true;
        agent.speed = sprintSpeed;

        try
        {
            Vector3 foxPosition = transform.position;

            //get the distance to the nearest fox
            //setLowestDistanceHare(foxPosition);

            Animal closestHare = GetComponent<AnimalCollider>().lowestDistanceAnimal(fox, GetComponent<FoxCollider>().preyList);

            // If the Hunted Animal is allready dead -> stop hunting and remove it from preyList
            if (closestHare.isAlive == false)
            {
                GetComponent<FoxCollider>().preyList.Remove(closestHare);
                isHunting = false;
            }

            // is there anything to hunt?
            if (GetComponent<FoxCollider>().preyList.Count == 0)
            {
                isHunting = false;
            }

            nearestHarePosition = closestHare.transform.position;
            _distanceToPrey = Vector3.Distance(foxPosition, nearestHarePosition);
            //Debug.DrawLine(foxPosition, nearestHarePosition, Color.black);
            if (_distanceToPrey < fox.killRange)
            {
                isHunting = false;

                if (closestHare.isAlive)
                {
                    fox.kill(closestHare.gameObject);
                    //StartCoroutine(runToDeadHare());
                    // TODO hier laufe zum hasen einfuegen
                    fox.eatHare(closestHare.gameObject);
                }


            }
            //Tell Agent where to go
            agent.SetDestination(runToHare(foxPosition, closestHare));
        }
        catch (MissingReferenceException)
        {
            //Debug.LogException(e,this);
        }
        finally
        {
            //check if there are any missing GameObjects in preyList and remove them
            gameObject.GetComponent<FoxCollider>().checkPreyList();
        }

    }

    IEnumerator getOutOfWater()
    {
        if (fox.transform.position.z > 71)
        {
            if (fox.transform.position.x > 43)
            {
                //Fuchs ist im rechten oberen Viertel
                agent.SetDestination(new Vector3(100f, 0f, 100f));
            }
            else
            {
                //Fuchs ist im linken oberen Viertel
                agent.SetDestination(new Vector3(0f, 0f, 100f));
            }
        }
        else
        {
            if (fox.transform.position.x > 43)
            {
                //Fuchs ist im rechten unteren Viertel
                agent.SetDestination(new Vector3(100f, 0f, 0f));
            }
            else
            {
                //Fuchs ist im linken unteren Viertel
                agent.SetDestination(new Vector3(0f, 0f, 0f));
            }
        }

        yield return new WaitForSeconds(2.0f);
        isUnderwater = false;
    }
}


