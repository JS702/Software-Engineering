using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsmenuFox : MonoBehaviour
{
    public Slider slider;
    public static int fox=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fox = (int)slider.value;
        
    }
}
