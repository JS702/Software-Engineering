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

    public Hare thisHare;

    private Vector3 _huntDirection;
  
    private float _distanceToPrey;
    //Vector3 foxPosition = transform.position;
    public bool isHunting = false;


    private void Start()
    {
        sprintSpeed = 20;
        rb = GetComponent<Rigidbody>();
        fox = GetComponent<Fox>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isHunting){
            //isWandering = false;
            hunt();
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
                    thisHare = hare.GetComponent<Hare>();
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

           
            //Look for nearest Fox
            Vector3 dirToHare = foxPosition - hare.transform.position;
            Debug.DrawLine(foxPosition, hare.transform.position, Color.red);

            // Escape direction
            _huntDirection = foxPosition - (dirToHare).normalized;
            Debug.DrawLine(foxPosition, _huntDirection, Color.blue);

            //Tell Agent where to go  
            agent.SetDestination(_huntDirection);

            //Debug.Log(_distanceToPrey);
            if(_distanceToPrey < 3){
                
                isHunting = false;
                fox.kill(hare);
                
                fox.eatHare(thisHare);
                //kill(hare);
            }
        }catch(MissingReferenceException e)
        {
            //Debug.LogException(e,this);
        }finally{
            gameObject.GetComponent<FoxCollider>().checkPreyList();
        }
    }
}

