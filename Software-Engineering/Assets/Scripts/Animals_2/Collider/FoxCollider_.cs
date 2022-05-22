using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxCollider_ : AnimalCollider_
{
    FoxMovement_ foxMovement;
    public List<GameObject> preyList;
    //public Collider col;


    void Start()
    {
        foxMovement = GetComponent<FoxMovement_>();

    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Prey")
        { 
            foxMovement.closestHare = col.gameObject;
            preyList.Add(foxMovement.closestHare);
        }
    }

    private void OnTriggerExit(Collider col)
    {  
        if(col.gameObject.tag == "Prey")
        {
            preyList.Remove(col.gameObject);
        }
    }
}
