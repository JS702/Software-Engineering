using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class HareMovement : Movement
{
   
    public Hare hare;

    public GameObject Fox;

    public List<GameObject> foxList;
    public Vector3 _direction;
    private float lowestDistance = 100;
    private float _distanceToFox;
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
        if (hare.getHunger() < 50 && hare.hasFoundGrass())
        {
            agent.SetDestination(hare.searchAndEatGrass());
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

    private void escape()
    {
        // reset Distance back to 100 to find the new nearest Fox
        lowestDistance = 100; //getLowestDistance();

        Vector3 harePosition = transform.position;
        Vector3 foxPosition = foxList[0].transform.position;

        _distanceToFox = Vector3.Distance(harePosition, foxPosition);
        
        
        if (_distanceToFox < lowestDistance)
        {
            //the nearest fox will be the Fox gameObject which the hare flee from
            Fox = foxList[0];
            lowestDistance = _distanceToFox;
        }

        if (foxList.Count > 1)
        {
            // find the closest Fox
            foreach (GameObject fox in foxList)
            {
                //Vector3 harePosition = transform.position;
                foxPosition = fox.transform.position;
                _distanceToFox = Vector3.Distance(harePosition, foxPosition);

                if (_distanceToFox < lowestDistance)
                {

                    //Der Fox der am dichtesten ist wird zum gameObject Fox vor dem der Hase wegrennt
                    Fox = fox;
                    lowestDistance = _distanceToFox;
                }
            }

            //Look for nearest Fox
            Vector3 dirToFox = transform.position - Fox.transform.position;
            Debug.DrawLine(transform.position, Fox.transform.position, Color.red);

            // Escape direction
            _direction = transform.position + (dirToFox).normalized;
            Debug.DrawLine(transform.position, _direction, Color.blue);

            //Tell Agent where to go  
            agent.SetDestination(_direction);

        }
        else
        { // If there is only one Fox
            Vector3 dirToFox = transform.position - Fox.transform.position;
            Debug.DrawLine(transform.position, Fox.transform.position, Color.red);

            // Escape direction
            _direction = transform.position + (dirToFox).normalized;
            Debug.DrawLine(transform.position, _direction, Color.blue);

            //Tell Agent where to go  
            agent.SetDestination(_direction);

        }
    }
}
