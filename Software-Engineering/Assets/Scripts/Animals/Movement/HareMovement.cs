using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class HareMovement : Movement
{
    Bunny bunny;
    public Hare hare;

    public GameObject Fox;
    public GameObject Grass;

    public List<GameObject> foxList;
    public List<Vector3> grassPositionList;
    public Vector3 _direction;
    private float lowestDistance = 100;
    private float _distanceToFox;
    public bool isFleeing = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bunny = GetComponent<Bunny>();
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
        if (hare.hunger < 50 && grassPositionList.Count > 0)
        {
            Debug.Log("Dichtestes Gras wird gesucht...");
            //Default: Dichtestes Gras ist das, das er zuerst entdeckt hat
            float distanceToNearestGrass = Vector3.Distance(grassPositionList[0], transform.position);
            Vector3 nearestGrassPosition = grassPositionList[0];
            foreach (Vector3 grassPosition in grassPositionList)
            {
                float distanceToGrass = Mathf.Abs(Vector3.Distance(grassPosition, transform.position));
                //Dichtestes Gras finden
                if (distanceToGrass < distanceToNearestGrass)
                {
                    distanceToNearestGrass = distanceToGrass;
                    nearestGrassPosition = grassPosition;
                }
            }
            //Zum dichtesten bekannten Gras laufen
            Debug.Log("Zum Gras bewegen");
            agent.SetDestination(nearestGrassPosition);
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
            Debug.Log("Grass in sight");
            Grass = col.gameObject;
            Vector3 grassPosition = Grass.transform.position;
            if (!grassPositionList.Contains(grassPosition))
            {
                grassPositionList.Add(grassPosition);
                Debug.Log("Grass added to list" + grassPosition.ToString());
            } else
            {
                Debug.Log("Grass already in list");
            }
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
        agent.speed = 20; // wert für jeden hasen einzeln setzte -> fleeSpeed
        // reset Distance back to 100 to find the new nearest Fox
        lowestDistance = 100;

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
