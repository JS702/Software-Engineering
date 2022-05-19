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


    private void Start()
    {

        
        rb = GetComponent<Rigidbody>();
        fox = GetComponent<Fox>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (fox.isHungry && !fox.isEating)
        {
            if(GetComponent<FoxCollider>().preyList.Count > 0){
                isWandering = false;
                
                hunt();
            }
        }
        else if (!isWandering && !isHunting && !fox.isEating)
        {
            StartCoroutine(setWanderDestination());
        }

        if (Input.GetKeyDown("k"))
        {
            fox.die(true);
            //Denkt daran den Agent zu stoppen wenn ihr die die-Methode aufruft, ich konnte aus Animal nicht darauf zugreifen
            agent.isStopped = true;
        }
        if (fox.isThirsty && fox.hasFoundWaterSource() && !isHunting)
        {
            agent.SetDestination(fox.moveToNearestWaterSource());
            if (agent.remainingDistance < 0.1)
            {
                fox.drinkWater();
            }
        }

    }


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

    public Vector3 runToHare(Vector3 foxPosition){
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
            setLowestDistanceHare(foxPosition);

            // If the Hunted Animal is allready dead -> stop hunting and remove it from preyList
            if (hare.GetComponent<Animal>().isAlive == false)
            {
                gameObject.GetComponent<FoxCollider>().preyList.Remove(hare);
                isHunting = false;
            }  

             // is there anything to hunt?
            if (gameObject.GetComponent<FoxCollider>().preyList.Count == 0)
            {
                isHunting = false;
            }
            nearestHarePosition = hare.transform.position;
            _distanceToPrey = Vector3.Distance(foxPosition, nearestHarePosition);
            //Debug.DrawLine(foxPosition, nearestHarePosition, Color.black);
            if (_distanceToPrey < fox.killRange)
            {
                isHunting = false;
                fox.kill(hare);
        // TODO hier laufe zum hasen einfuegen
                fox.eatHare(hare);
                
            }
            //Tell Agent where to go
            agent.SetDestination(runToHare(foxPosition));  
        }
        catch (MissingReferenceException e)
        {
            //Debug.LogException(e,this);
        }
        finally
        {
            //check if there are any missing GameObjects in preyList and remove them
            gameObject.GetComponent<FoxCollider>().checkPreyList();
        }

    }
}


