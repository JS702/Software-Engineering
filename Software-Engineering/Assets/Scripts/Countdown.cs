using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private float countdowntime=0;
    private bool a = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        countdowntime += 1 * Time.deltaTime;
        

        if((countdowntime > Timeslider.timef) & a)
        {

            a = false;
        }
    }
}
