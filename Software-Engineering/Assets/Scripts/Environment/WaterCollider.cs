using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        /**
        if (other.tag == "Fox" || other.tag == "Prey")
        {
            Debug.Log("Water Collider triggered");
            Movement movement = other.GetComponent<Movement>();
            Vector3 destination = movement.agent.destination;
            movement.agent.SetDestination(destination*-1);
        }
        */

        if (other.tag == "Prey")
        {
            other.GetComponent<HareMovement>().hare.isUnderwater = true;
        }

        if (other.tag == "Fox")
        {
            other.GetComponent<FoxMovement>().isUnderwater = true;
        }
    }
    /**
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Prey")
        {
            other.GetComponent<HareMovement>().isUnderwater = false;
        }

        if (other.tag == "Fox")
        {
            other.GetComponent<foxMovement>().isUnderwater = true;
        }
    }
    */
}
