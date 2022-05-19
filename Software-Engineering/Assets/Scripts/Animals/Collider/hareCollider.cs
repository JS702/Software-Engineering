using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hareCollider : HareMovement
{
    //private Hare hare;
    public List<GameObject> foxList;
      private void OnTriggerEnter(Collider col)
    {
        //if a fox enters the Sight of the hare, the hare add this Fox to his list of Foxes nearby
        if (col.tag == "Fox")
        {
            
            //isWandering = false;
            //agent.speed = sprintSpeed;
            Fox = col.gameObject;
            foxList.Add(Fox);
            //danger = true;
            //hare.isEating = false;
            //hare.isDrinking = false;

        }

        if (col.tag == "Grass")
        {
            hare.addGrassToList(col);
        }
        if (col.tag == "WaterSource")
        {
            hare.addWaterSourceToList(col);
            
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
                danger = false;
            }
        }
        
    }
  
}
