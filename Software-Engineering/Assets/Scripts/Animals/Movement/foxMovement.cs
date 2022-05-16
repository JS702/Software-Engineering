using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class foxMovement : Movement
{
    public Fox fox;
    

    // Hunting Variables
    public NavMeshAgent _agent;
    public GameObject Hare;
    public List<GameObject> preyList;
    
    public Vector3 _huntDirection;
    private float lowestDistance = 100;
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
        if (isHunting){
            hunt();
        }
        else if (!isWandering && !isHunting)
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
            Debug.Log("Set destination: "+agent.SetDestination(fox.moveToNearestWaterSource()));
            Debug.Log("remaining Distance:"+agent.remainingDistance);
            if (agent.remainingDistance < 0.1)
            {

                fox.drinkWater();     
 ;         
            }
        }
    }

    public Collider col;

    private void OnTriggerEnter(Collider col)
    {
         //if a fox enters the Sight of the hare, the hare add this Fox to his list of Foxes nearby
        if(col.tag == "Prey")
        {
            
            isWandering = false;
            agent.speed = sprintSpeed;
            Hare = col.gameObject;
            preyList.Add(Hare);
            isHunting = true;
        }

        if (col.tag == "WaterSource")
        {
            fox.addWaterSourceToList(col);
        }

    }

    private void OnTriggerExit(Collider col)
    {  
        if(col.gameObject.tag == "Prey")
        {
            preyList.Remove(col.gameObject);
        }
    }

     public void getLowestDistance(Vector3 foxPosition)
     {
        float _distanceToHare;
        float lowestDistance = 100;
         foreach (GameObject hare in preyList)
            {
                Vector3 harePosition = hare.transform.position;
                _distanceToHare = Vector3.Distance(foxPosition, harePosition);

                if (_distanceToHare < lowestDistance)
                {
                    //Der Fox der am dichtesten ist wird zum gameObject Fox vor dem der Hase wegrennt
                    Hare = hare;
                    lowestDistance = _distanceToHare;
                }
            }
    }


    private void hunt()
    {
         // is there anything to hunt?
        if(preyList.Count == 0)
        {
            isHunting=false;
        }
        
        //the direction in wich the hare is fleeing if a Fox is around
        Vector3 _huntDirection;
        Vector3 foxPosition = transform.position;

        //get the distance to the nearest fox
        try
        {
            getLowestDistance(foxPosition);
            //Look for nearest Fox
            Vector3 dirToHare = foxPosition - Hare.transform.position;
            Debug.DrawLine(foxPosition, Hare.transform.position, Color.red);

            // Escape direction
            _huntDirection = foxPosition - (dirToHare).normalized;
            Debug.DrawLine(foxPosition, _huntDirection, Color.blue);

            //Tell Agent where to go  
            agent.SetDestination(_huntDirection);
        }catch(MissingReferenceException e)
        {
            //Debug.LogException(e,this);
        }
    }
}

