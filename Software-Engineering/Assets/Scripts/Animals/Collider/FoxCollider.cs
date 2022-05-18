using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxCollider : foxMovement
{
    // a list of foxes around the hare
         
    //private Fox fox;
    public List<GameObject> preyList;
    //public Collider col;

    
    public void checkPreyList()
    {
        for(var i = preyList.Count - 1; i > -1; i--)
        {   
        if (preyList[i] == null)
        preyList.RemoveAt(i);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
         //if a fox enters the Sight of the hare, the hare add this Fox to his list of Foxes nearby
        if(col.tag == "Prey" && fox.isHungry)
        {
            isWandering = false;
            agent.speed = sprintSpeed;
            hare = col.gameObject;
            preyList.Add(hare);
            isHunting = true;
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
