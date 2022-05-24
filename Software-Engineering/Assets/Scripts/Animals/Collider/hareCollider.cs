using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hareCollider : MonoBehaviour
{
    //private Hare hare;
    public List<GameObject> foxList;
    public List<Hare> potentialSexPartnerList;
      private void OnTriggerEnter(Collider col)
    {
        //if a fox enters the Sight of the hare, the hare add this Fox to his list of Foxes nearby
        if (col.tag == "Fox")
        {
            
            //isWandering = false;
            //agent.speed = sprintSpeed;
            GetComponent<HareMovement>().inDanger = true;
            GetComponent<HareMovement>().closestFox = col.gameObject;
            foxList.Add( GetComponent<HareMovement>().closestFox);
            //danger = true;
            //hare.isEating = false;
            //hare.isDrinking = false;

        }
        if(col.tag == "Prey" && col.GetComponent<Hare>().gender != gameObject.GetComponent<Hare>().gender && col.GetComponent<Animal>().isAlive){
            potentialSexPartnerList.Add(col.GetComponent<Hare>());
        }

        if (col.tag == "Grass")
        {
            GetComponent<HareMovement>().hare.addGrassToList(col);
        }
        /**
        if (col.tag == "WaterSource")
        {
            hare.addWaterSourceToList(col);
            
        }
        */
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Fox")
        {
            foxList.Remove(col.gameObject);

            //set isFleeing to false when there is no fox around
            if (foxList.Count == 0)
            {
                //Debug.Log(" FLUCHT BEEENDEN");
                GetComponent<HareMovement>().agent.speed =  GetComponent<Movement>().normalSpeed;
                GetComponent<HareMovement>().inDanger = false;
                GetComponent<HareMovement>().isFleeing = false;
            }
        }
        if(col.tag == "Prey"){
            if(potentialSexPartnerList.Count == 0){
                GetComponent<HareMovement>().closestSexPartner = null;
            }
            potentialSexPartnerList.Remove(col.GetComponent<Hare>());
        }
        
    }
  
}
