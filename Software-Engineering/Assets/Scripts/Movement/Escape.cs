using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Escape : MonoBehaviour
{

    public NavMeshAgent _agent;
    public GameObject Fox;
    public bool isFleeing = false;
    private Vector3 _direction;

     
     public void start() {
        _agent = GetComponent<NavMeshAgent>();
     }
     
     
     private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Fox"){

            // Den Fuchs in einen Array speichern
            Fox = col.gameObject;
            isFleeing = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {

         if(collider.gameObject.tag == "Fox"){
            Fox = collider.gameObject;

            //isFleeing erst auf false setzen wenn das array mit fuechsen leer ist
            isFleeing = false;
        }
    }
     public void Update() {

        if(isFleeing){
            escape(); 
        } 
     }

    
    // wenn in einer Liste Gespeichert dann muss vor dem Fuchs geflohen werden der am dichtesten ist
    private void escape()
    {
            // Distance to the fox
            Vector3 dirToFox = transform.position - Fox.transform.position;
            Debug.DrawLine(transform.position, Fox.transform.position, Color.red );
            
            // Escape direction
            _direction = transform.position + (dirToFox).normalized;
            Debug.DrawLine(transform.position,  _direction, Color.blue );

            //Tell Agent where to go  
            _agent.SetDestination(_direction);
    }
   
}
