using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier : Nahrung
{
    public int durst = 100;
    public int hunger = 100;
    public int fortpflanzungsTrieb = 0;
    public int geschwindigkeit;
    public int leben;


    public void fressen(int nahrung)
    {
        hunger += nahrung;
    } 
    public void trinken(int wasser)
    {
        durst += wasser;
    } 
    
}
