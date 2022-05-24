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
    public Hare closestSexPartner;

    //public List<GameObject> foxList
    public bool inDanger = false;
    public bool isFleeing = false;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        hare = GetComponent<Hare>();
    }

    private void Update()
    {

        if(hare.isAlive){
            if (inDanger)
        {
            escape();
        }
        else
        {     
            //HUNGRY
            if (hare.isHungry && hare.hasFoundGrass() && !isFleeing && !hare.isDrinking && !hare.isUnderwater && !hare.isThirsty)
            {
                agent.SetDestination(hare.moveToNearestGrass());
                if (hare.isInGrassArea)
                {
                    agent.isStopped = hare.eatGrass();
                }
            }
            
            //THIRSTY
            if (hare.isThirsty && !isFleeing && !hare.isEating && !hare.isUnderwater)
            {
                agent.SetDestination(hare.waterPosition);
                if (hare.isInWaterArea)
                {
                    agent.isStopped = hare.drinkWater();
                }
            }

            //DROWNING
            if (hare.isUnderwater)
            {
                //Debug.Log("hare is underwater");
                //Vector3 direction = agent.destination;
                //agent.SetDestination(-direction);
                //Debug.Log("hare is underwater" + direction + " " + -direction);
                //agent.isStopped = true;
                StartCoroutine(getOutOfWater());
            }
            
            //HORNY
            if (hare.isHorny && !isFleeing && !hare.isHungry && !hare.isThirsty && !hare.isUnderwater)
            {
                reproduce();
            }
         
            //JUST WANDER
            if (!isWandering && !isFleeing && !hare.isUnderwater && !hare.isHungry && !hare.isThirsty)
            {
                StartCoroutine(setWanderDestination());
            }

            GetComponent<Animal>().TestInputs();
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


        }
        
    }


    public void setLowestDistanceFox(Vector3 harePosition)
    {
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

    public void setLowestDistanceSexPartner(Vector3 harePosition)
    {

        List<Hare> potentialSexPartnerList = GetComponent<hareCollider>().potentialSexPartnerList;
        float _distanceToSexPartner;
        float lowestDistance = 100;
        foreach (Hare sexPartner in potentialSexPartnerList)
        {
            Vector3 sexPartnerPosition = hare.transform.position;
            _distanceToSexPartner = Vector3.Distance(harePosition, sexPartnerPosition);

            if (_distanceToSexPartner < lowestDistance && !sexPartner.GetComponent<Hare>().isPregnant)
            {
                //Der Fox der am dichtesten ist wird zum gameObject Fox vor dem der Hase wegrennt
                closestSexPartner = sexPartner;
                lowestDistance = _distanceToSexPartner;
            }
        }
    }

    private void escape()
    {
        isFleeing = true;

        agent.isStopped = false;
        hare.isHavingAReallyGoodTime = false;
        hare.isHorny = false;
        hare.isLookingForSex = false;
        hare.isDrinking = false;
        hare.isEating = false;

        agent.speed = sprintSpeed;
        //the direction in wich the hare is fleeing if a Fox is around
        Vector3 _fleeDirection;
        Vector3 harePosition = transform.position;

        //set the lowest distance Fox in range as the Fox too flee from
        try
        {
            setLowestDistanceFox(harePosition);

            //in whoch direction is the nearest Fox?
            Vector3 dirToFox = harePosition - closestFox.transform.position;
            //Debug.DrawLine(harePosition, Fox.transform.position, Color.red);

            // Escape direction
            _fleeDirection = harePosition + (dirToFox).normalized;
            //Debug.DrawLine(harePosition, _fleeDirection, Color.blue);

            //Tell Agent where to go  
            agent.SetDestination(_fleeDirection);
        }
        catch (MissingReferenceException)
        {
        }
    }


    private void reproduce()
    {
        hare.isLookingForSex = true;
        GetComponent<hareCollider>().checkPotentialSexPartnerList();
        //meine position
        Vector3 harePosition = transform.position;

        //welches ist der naechste hase
        setLowestDistanceSexPartner(harePosition);
        

        if (closestSexPartner != null)
        {   
            if (Vector3.Distance(harePosition, closestSexPartner.transform.position) < 2                        // in range of a potential sex Partner
                                && hare.gender.Equals("male")                                                   // the active hare is male
                                && closestSexPartner.GetComponent<Hare>().isHorny                               // the target hare isHorny
                                && !closestSexPartner.GetComponent<Hare>().isPregnant                           // the target hare is NOT pregnant
                                //&& closestSexPartner.isAlive                                                    // the hare is Alive
                                && closestSexPartner.GetComponent<HareMovement>().closestSexPartner == hare     //the target of my closest sex partner is me
                                )
            {
                hare.isLookingForSex = false;
                agent.isStopped = hare.isHavingFun();                                       // start to have sex
                //Debug.Log("IM HAVING A REALLY GOOD TIME");
            }

            if (closestSexPartner.GetComponent<Hare>().isPregnant)
            {                              // if the target is allready pregnant:
                GetComponent<hareCollider>().potentialSexPartnerList.Remove(closestSexPartner); // remove it from potentialSexPartnerList
            }
            else
            {
                agent.SetDestination(closestSexPartner.transform.position);                     // else: run to the closestSexPartner
            }

        }
    }
    IEnumerator getOutOfWater()
    {
        //Vector3 direction = agent.destination;
        //agent.SetDestination(-direction*5);
        if (transform.position.z > 71)
        {
            if (transform.position.x > 43)
            {
                //Hase ist im rechten oberen Viertel
                GetComponent<Movement>().agent.SetDestination(new Vector3(100f, 0f, 100f));
            }
            else
            {
                //Hase ist im linken oberen Viertel
                GetComponent<Movement>().agent.SetDestination(new Vector3(0f, 0f, 100f));
            }
        }
        else
        {
            if (transform.position.x > 43)
            {
                //Hase ist im rechten unteren Viertel
                GetComponent<Movement>().agent.SetDestination(new Vector3(100f, 0f, 0f));
            }
            else
            {
                //Hase ist im linken unteren Viertel
                GetComponent<Movement>().agent.SetDestination(new Vector3(0f, 0f, 0f));
            }
        }

        yield return new WaitForSeconds(2.0f);
        hare.isUnderwater = false;
    }


}
