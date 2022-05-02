using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class foxMovement : Movement
{
    private Fox fox;
    

    // Hunting Variables
    public NavMeshAgent _agent;
    public GameObject Hare;
    public List<GameObject> preyList;
    
    public Vector3 _huntDirection;
    private float lowestDistance = 100;
    private float _distanceToPrey;
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
    }

    private void OnTriggerExit(Collider col)
    {
        
        if(col.gameObject.tag == "Prey")
        {
            preyList.Remove(col.gameObject);

        }


    }

    public bool getIsWalking()
    {
        return isWalking;
    }

    public void hunt()
    {
        // is there anything to hunt?
        if(preyList.Count == 0)
        {
            isHunting=false;
        }


        // reset Distance back to 100 to find the new nearest Fox
        lowestDistance = 100;

        Vector3 foxPosition = transform.position;
        Vector3 preyPosition = preyList[0].transform.position;

        if(preyList.Count > 1)
        {

            // find the closest Fox
            foreach(GameObject prey in preyList)
            {
                
                preyPosition = prey.transform.position;
                _distanceToPrey = Vector3.Distance(foxPosition, preyPosition);

                if(_distanceToPrey < lowestDistance){

                    //Der Fox der am dichtesten ist wird zum gameObject Fox vor dem der Hase wegrennt
                    Hare = prey;
                    lowestDistance = _distanceToPrey;
                }
            }

            
           
            
            //Look for nearest Fox
            Vector3 dirToPrey = transform.position - Hare.transform.position;
            Debug.DrawLine(transform.position, Hare.transform.position, Color.red );
            
            // Escape direction
            _huntDirection = transform.position - (dirToPrey).normalized;
            Debug.DrawLine(transform.position,  _huntDirection, Color.blue );

            //Tell Agent where to go  
            _agent.SetDestination(_huntDirection);
            
        }
        else
        { // If there is only one Fox

            _distanceToPrey = Vector3.Distance(foxPosition, preyPosition);
            if(_distanceToPrey < lowestDistance)
            {
                //the nearest fox will be the Fox gameObject which the hare flee from
                Hare = preyList[0];
                lowestDistance = _distanceToPrey;
            }

            Vector3 dirToFox = transform.position - Hare.transform.position;
            Debug.DrawLine(transform.position, Hare.transform.position, Color.red );
            
            // Escape direction
            _huntDirection = transform.position - (dirToFox).normalized;
            Debug.DrawLine(transform.position,  _huntDirection, Color.blue );

            //Tell Agent where to go  
            _agent.SetDestination(_huntDirection);

        }
    }


}

