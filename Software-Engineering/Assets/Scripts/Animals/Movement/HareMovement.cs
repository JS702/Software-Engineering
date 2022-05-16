using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class HareMovement : Movement
{
   
   //reference to the hare itself
    public Hare hare;

    //reference to the closest Fox
    public GameObject Fox;

    // a list of foxes around the hare
    public List<GameObject> foxList;
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
        if (hare.isHungry && hare.hasFoundGrass() && !isFleeing)
        {
            agent.SetDestination(hare.moveToNearestGrass());
            Debug.Log("Set destination hare: "+agent.SetDestination(hare.moveToNearestGrass()));
            Debug.Log("remaining Distance hare:"+agent.remainingDistance);
            if (agent.remainingDistance < 0.1)
            {
                hare.eatGrass();
                Debug.Log("Hase frisst. Hunger: " + hare.hunger);
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

    private void OnTriggerEnter(Collider col)
    {
        //if a fox enters the Sight of the hare, the hare add this Fox to his list of Foxes nearby
        if (col.tag == "Fox")
        {
            isWandering = false;
            agent.speed = sprintSpeed;
            Fox = col.gameObject;
            foxList.Add(Fox);
            isFleeing = true;
            hare.isEating = false;

        }

        if (col.tag == "Grass")
        {
            hare.addGrassToList(col);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Fox")
        {
            foxList.Remove(col.gameObject);

            //set isFleeing to false when there is no fox around
            if (foxList.Count == 0)
            {
                agent.speed = normalSpeed;
                isFleeing = false;
            }
        }
    }

    public void getLowestDistance(Vector3 harePosition){
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

        //get the distance to the nearest fox
        try{
            getLowestDistance(harePosition);

            //Look for nearest Fox
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
