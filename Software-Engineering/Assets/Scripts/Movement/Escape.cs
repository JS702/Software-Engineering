using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Escape : MonoBehaviour
{

    Hare hare;
    public NavMeshAgent _agent;
    public GameObject Fox;
    public List<GameObject> foxList;
   
    public Vector3 _direction;
    private float lowestDistance = 100;
    private float _distanceToFox;
    public bool isFleeing = false;
    
    
     
     public void start() 
     {
        hare = GetComponent<Hare>();
        _agent = GetComponent<NavMeshAgent>();
     }
     
     
     private void OnTriggerEnter(Collider col)
    {

        //if a fox enters the Sight of the hare, the hare add this Fox to his list of Foxes nearby
        if(col.tag == "Fox")
        {
            Fox = col.gameObject;
            foxList.Add(Fox);
            isFleeing = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        
         if(col.gameObject.tag == "Fox")
         {
            foxList.Remove(col.gameObject);

            //set isFleeing to false when there is no fox around
            if(foxList.Count == 0){
                isFleeing = false;
            }
            
        }
    }
     public void Update() 
     {

        if(isFleeing){
            escape(); 
        } 
     }

    private void escape()
    {
        // reset Distance back to 100 to find the new nearest Fox
        lowestDistance = 100;

        Vector3 harePosition = transform.position;
        Vector3 foxPosition = foxList[0].transform.position;

        _distanceToFox = Vector3.Distance(harePosition, foxPosition);

        if(_distanceToFox < lowestDistance)
        {
                //the nearest fox will be the Fox gameObject which the hare flee from
                Fox = foxList[0];
                lowestDistance = _distanceToFox;
        }

        if(foxList.Count > 1)
        {
            // find the closest Fox
            foreach(GameObject fox in foxList)
            {
                //Vector3 harePosition = transform.position;
                foxPosition = fox.transform.position;
                _distanceToFox = Vector3.Distance(harePosition, foxPosition);

                if(_distanceToFox < lowestDistance){

                    //Der Fox der am dichtesten ist wird zum gameObject Fox vor dem der Hase wegrennt
                    Fox = fox;
                    lowestDistance = _distanceToFox;
                }
            }

     
            
            //Look for nearest Fox
            Vector3 dirToFox = transform.position - Fox.transform.position;
            Debug.DrawLine(transform.position, Fox.transform.position, Color.red );
            
            // Escape direction
            _direction = transform.position + (dirToFox).normalized;
            Debug.DrawLine(transform.position,  _direction, Color.blue );

            //Tell Agent where to go  
            _agent.SetDestination(_direction);
            
        }else
        { // If there is only one Fox
            Vector3 dirToFox = transform.position - Fox.transform.position;
            Debug.DrawLine(transform.position, Fox.transform.position, Color.red );
            
            // Escape direction
            _direction = transform.position + (dirToFox).normalized;
            Debug.DrawLine(transform.position,  _direction, Color.blue );

            //Tell Agent where to go  
            _agent.SetDestination(_direction);

        }
    }
}
