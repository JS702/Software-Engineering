using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SettingsmenuBunny : MonoBehaviour
{
    public Slider slider;
    public static int bunny;
    public TextMeshProUGUI bunnynumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bunny = (int)slider.value;
        bunnynumber.text = slider.value.ToString();
        
       
        
    }
}
