using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_ : MonoBehaviour
{
    Hare_ hare;
    Fox_ fox;
    Movement_ movement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prey")
        {
            hare = other.GetComponent<Hare_>();
            hare.isInWaterArea = true;
        }

        if (other.tag == "Fox")
        {
            fox = other.GetComponent<Fox_>();
            fox.isInWaterArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Prey")
        {
            hare = other.GetComponent<Hare_>();
            hare.isInWaterArea = false;
        }

        if (other.tag == "Fox")
        {
            fox = other.GetComponent<Fox_>();
            fox.isInWaterArea = false;
        }
    }

    /**
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        movement = collision.gameObject.GetComponent<Movement>();
        Vector3 destination = movement.agent.destination;
        movement.agent.SetDestination(-destination);
    }
    */
}
