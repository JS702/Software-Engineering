using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;

public class HareMovement : Movement
{
   
   //reference to the hare itself
    public Hare hare;

    //reference to the closest Fox
    public GameObject closestFox;
    //
    public GameObject closestSexPartner;

    //public List<GameObject> foxList
    public bool danger = false;
    public bool isFleeing = false;
    public bool isUnderwater;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        hare = GetComponent<Hare>();
    }

    private void Update()
    {
        if(GetComponent<hareCollider>().foxList.Count > 0){
            danger = true;
        }
        //Debug.Log("isFleeing:" + isFleeing);
        if(danger)
        {
            escape();
        }
        else if (!isWandering && !isFleeing && !isUnderwater && !GetComponent<Hare>().isHorny)
        {
            StartCoroutine(setWanderDestination());
        }
        
        //Abfrage, ob der Hase hungrig ist und Gras kennt
        if (hare.isHungry && hare.hasFoundGrass() && !isFleeing && !hare.isDrinking && !isUnderwater && !GetComponent<Hare>().isHorny)
        {
            agent.SetDestination(hare.moveToNearestGrass());
            if (/**agent.remainingDistance < 0.5*/hare.isInGrassArea)
            {
                //agent.isStopped = true;
                agent.isStopped = hare.eatGrass();
            }
        }

        //Stefans Code
        /**
        if (hare.isThirsty && hare.hasFoundWaterSource() && !isFleeing &&!hare.isEating)
        {
            agent.SetDestination(hare.moveToNearestWaterSource());
            if (agent.remainingDistance < 0.1)
            {
                hare.drinkWater();
            }
        }
        */

        //Dieser Code > Stefans Code
        if (hare.isThirsty && !isFleeing && !hare.isEating && !isUnderwater && !hare.isHorny)
        {
            agent.SetDestination(hare.waterPosition);
            if (hare.isInWaterArea)
            {
                agent.isStopped = hare.drinkWater();
            }
        }

        if(hare.isHorny && !isFleeing && !hare.isEating && !hare.isThirsty){
            reproduce();
        }

        if(isUnderwater)
        {
            //Debug.Log("hare is underwater");
            //Vector3 direction = agent.destination;
            //agent.SetDestination(-direction);
            //Debug.Log("hare is underwater" + direction + " " + -direction);
            //agent.isStopped = true;
            StartCoroutine(getOutOfWater());
        }

        IEnumerator getOutOfWater()
        {
            //Vector3 direction = agent.destination;
            //agent.SetDestination(-direction*5);
            if(hare.transform.position.z > 71)
            {
                if (hare.transform.position.x > 43)
                {
                    //Hase ist im rechten oberen Viertel
                    agent.SetDestination(new Vector3(100f, 0f, 100f));
                }
                else
                {
                    //Hase ist im linken oberen Viertel
                    agent.SetDestination(new Vector3(0f, 0f, 100f));
                }
            }
            else
            {
                if (hare.transform.position.x > 43)
                {
                    //Hase ist im rechten unteren Viertel
                    agent.SetDestination(new Vector3(100f, 0f, 0f));
                }
                else
                {
                    //Hase ist im linken unteren Viertel
                    agent.SetDestination(new Vector3(0f, 0f, 0f));
                }
            }
            
            yield return new WaitForSeconds(2.0f);
            isUnderwater = false;
        }

        //Debug-Tool, bis der Hase hunger bekommen und fressen kann
        if (Input.GetKeyDown("h"))
        {
            hare.currentHunger = 10;
            Debug.Log(hare.currentHunger);
        }
        if (Input.GetKeyDown("j"))
        {
            hare.currentHunger = 100;
            Debug.Log(hare.currentHunger);
        }
        if (Input.GetKeyDown("k"))
        {
            //Denkt daran den Agent zu stoppen wenn ihr die die-Methode aufruft, ich konnte aus Animal nicht darauf zugreifen
            agent.isStopped = true;
            hare.die(false);
            
        }
        if (Input.GetKeyDown("l"))
        {
            hare.currentThirst = 10;
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
                    closestFox = fox;
                    lowestDistance = _distanceToFox;
                }
            }
    }

     public void setLowestDistanceSexPartner(Vector3 harePosition){
        
        List<GameObject> potentialSexPartnerList = GetComponent<hareCollider>().potentialSexPartnerList;
        float _distanceToSexPartner;
        float lowestDistance = 100;
         foreach (GameObject hare in potentialSexPartnerList)
            {
                Vector3 sexPartnerPosition = hare.transform.position;
                _distanceToSexPartner = Vector3.Distance(harePosition, sexPartnerPosition);

                if (_distanceToSexPartner < lowestDistance)
                {
                    //Der Fox der am dichtesten ist wird zum gameObject Fox vor dem der Hase wegrennt
                    closestSexPartner = hare;
                    lowestDistance = _distanceToSexPartner;
                }
            }
    }

    private void escape(){
        isFleeing = true;
        agent.speed = sprintSpeed;
        //the direction in wich the hare is fleeing if a Fox is around
        Vector3 _fleeDirection;
        Vector3 harePosition = transform.position;

        //set the lowest distance Fox in range as the Fox too flee from
        try{
            setLowestDistanceFox(harePosition);

            //in whoch direction is the nearest Fox?
            Vector3 dirToFox = harePosition - closestFox.transform.position;
            //Debug.DrawLine(harePosition, Fox.transform.position, Color.red);

            // Escape direction
            _fleeDirection = harePosition + (dirToFox).normalized;
            //Debug.DrawLine(harePosition, _fleeDirection, Color.blue);

            //Tell Agent where to go  
            agent.SetDestination(_fleeDirection);

        }catch(MissingReferenceException){

        }
    }

    private void reproduce(){
        //meine position
        Vector3 harePosition = transform.position;
        //ist auf partnersuche
        //wantSomeLove = true;

        //welches ist der naechste hase
        setLowestDistanceSexPartner(harePosition);

        if(closestSexPartner != null){
            //wenn in reichweite des potentiellen partners dann paaren
            if(Vector3.Distance(harePosition, closestSexPartner.transform.position) < 2){
                
                hare.isHavingFun();
                Debug.Log(" IM HAVING A REALLY GOOD TIME");
            }else{
                 //Zum naechstbesten hasen laufen
                 // abfragen ob geschlecht bzw alter stimmt
                agent.SetDestination(closestSexPartner.transform.position);

            }
  
        }
    
    }
    
}
