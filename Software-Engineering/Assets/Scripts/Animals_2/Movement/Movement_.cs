using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement_ : MonoBehaviour
{
       public float range = 10.0f;

    public bool isWandering = false;
    protected bool isRotatingLeft = false;
    protected bool isRotatingRight = false;
    protected bool isWalking = false;

    public int normalSpeed = 2;
    public int sprintSpeed = 8;

    public NavMeshAgent agent;
    protected Rigidbody rb;


    public IEnumerator setWanderDestination()
    {
        isWandering = true;
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //Debug.Log(hit.position);
            agent.destination = hit.position;
            yield return new WaitForSeconds(3f);
        }
        isWandering = false;
    }

}
