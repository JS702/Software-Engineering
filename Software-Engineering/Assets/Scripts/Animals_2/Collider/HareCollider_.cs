using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HareCollider_ : AnimalCollider_
{
    HareMovement_ hareMovement;
    public List<GameObject> foxList;

    void Start()
    {
        hareMovement = GetComponent<HareMovement_>();

    }
    private void OnTriggerEnter(Collider col)
    {
        //if a fox enters the Sight of the hare, the hare add this Fox to his list of Foxes nearby
        if (col.tag == "Fox")
        {
            GetComponent<Hare_>().inDanger = true;
            hareMovement.closestFox = col.gameObject;
            foxList.Add(hareMovement.closestFox);
        }
        if (col.tag == "Prey" && col.gameObject.GetComponent<Animal_>().gender != gameObject.GetComponent<Animal_>().gender && col.GetComponent<Animal_>().isAlive && !col.GetComponent<Animal_>().isPregnant)
        {
           potentialSexPartnerList.Add(col.gameObject);
        }

        if (col.tag == "Grass")
        {
            hareMovement.hare.addGrassToList(col);
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
                GetComponent<Movement_>().agent.speed = GetComponent<Movement_>().normalSpeed;
                hareMovement.hare.inDanger = false;
                hareMovement.hare.isFleeing = false;
            }
        }
        if (col.tag == "Prey")
        {
           potentialSexPartnerList.Remove(col.gameObject);
        }

    }
}
