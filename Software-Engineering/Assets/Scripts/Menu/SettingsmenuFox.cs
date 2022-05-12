using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsmenuFox : MonoBehaviour
{
    public Slider slider;
    public static int fox;
    public TextMeshProUGUI foxnumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fox = (int)slider.value;
        foxnumber.text= slider.value.ToString();

    }
}
