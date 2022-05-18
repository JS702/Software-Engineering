using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class foxMovement : Movement
{
    public Fox fox;
    

    
    public NavMeshAgent _agent;
    // Hunting Variables

    //the Hare that the fox is Hunting
    public GameObject hare;

    //list of prey nearby the Fox
    //public List<GameObject> preyList;
    
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
            if (agent.remainingDistance < 0.1)
            {
                fox.drinkWater();
            }
        }
    }

/*
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
    }

    private void OnTriggerExit(Collider col)
    {  
        if(col.gameObject.tag == "Prey")
        {
            preyList.Remove(col.gameObject);
        }
    }
    */

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


    private void hunt()
    {
        

    try
        {
        if(hare.GetComponent<Animal>().isAlive == false){
            gameObject.GetComponent<FoxCollider>().preyList.Remove(hare);
            isHunting = false;
        }
         // is there anything to hunt?
        if(gameObject.GetComponent<FoxCollider>().preyList.Count == 0)
        {
            isHunting=false;
        }
        
        //the direction in wich the hare is fleeing if a Fox is around
        Vector3 _huntDirection;
        Vector3 foxPosition = transform.position;

        //get the distance to the nearest fox
       
            setLowestDistanceHare(foxPosition);

            if(_distanceToPrey < 4){
                
                gameObject.GetComponent<Fox>().kill(hare);
                isHunting = false;
                //kill(hare);
            }
            //Look for nearest Fox
            Vector3 dirToHare = foxPosition - hare.transform.position;
            Debug.DrawLine(foxPosition, hare.transform.position, Color.red);

            // Escape direction
            _huntDirection = foxPosition - (dirToHare).normalized;
            Debug.DrawLine(foxPosition, _huntDirection, Color.blue);

            //Tell Agent where to go  
            agent.SetDestination(_huntDirection);
        }catch(MissingReferenceException e)
        {
            //Debug.LogException(e,this);
        }finally{
            gameObject.GetComponent<FoxCollider>().checkPreyList();
        }
    }
}

