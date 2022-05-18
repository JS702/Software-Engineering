using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class HareMovement : Movement
{
   
   //reference to the hare itself
    public Hare hare;

    //reference to the closest Fox
    public GameObject Fox;

    //public List<GameObject> foxList;
    public bool isFleeing = false;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        hare = GetComponent<Hare>();
    }

    private void Update()
    {
        //Debug.Log("isFleeing:" + isFleeing);
        if(isFleeing)
        {
            escape();
        }
        else if (!isWandering && !isFleeing)
        {
            StartCoroutine(setWanderDestination());
        }
        
        //Abfrage, ob der Hase hungrig ist und Gras kennt
        if (hare.isHungry && hare.hasFoundGrass() && !isFleeing && !hare.isDrinking)
        {
            agent.SetDestination(hare.moveToNearestGrass());
            if (agent.remainingDistance < 0.1)
            {
                hare.eatGrass();
            }
        }
        if (hare.isThirsty && hare.hasFoundWaterSource() && !isFleeing &&!hare.isEating)
        {
            agent.SetDestination(hare.moveToNearestWaterSource());
            if (agent.remainingDistance < 0.1)
            {
                hare.drinkWater();
            }
        }
        
        
        //Debug-Tool, bis der Hase hunger bekommen und fressen kann
        if (Input.GetKeyDown("h"))
        {
            hare.hunger = 10;
            Debug.Log(hare.hunger);
        }
        if (Input.GetKeyDown("j"))
        {
            hare.hunger = 100;
            Debug.Log(hare.hunger);
        }
        if (Input.GetKeyDown("k"))
        {
            hare.die(false);
            //Denkt daran den Agent zu stoppen wenn ihr die die-Methode aufruft, ich konnte aus Animal nicht darauf zugreifen
            agent.isStopped = true;
        }
    }

    
    public void setLowestDistanceFox(Vector3 harePosition){
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
                    Fox = fox;
                    lowestDistance = _distanceToFox;
                }
            }
    }

    private void escape(){
        //the direction in wich the hare is fleeing if a Fox is around
        Vector3 _fleeDirection;
        Vector3 harePosition = transform.position;

        //set the lowest distance Fox in range as the Fox too flee from
        try{
            setLowestDistanceFox(harePosition);

            //in whoch direction is the nearest Fox?
            Vector3 dirToFox = harePosition - Fox.transform.position;
            Debug.DrawLine(harePosition, Fox.transform.position, Color.red);

            // Escape direction
            _fleeDirection = harePosition + (dirToFox).normalized;
            Debug.DrawLine(harePosition, _fleeDirection, Color.blue);

            //Tell Agent where to go  
            agent.SetDestination(_fleeDirection);

        }catch(MissingReferenceException){

        }
    }
    
}
