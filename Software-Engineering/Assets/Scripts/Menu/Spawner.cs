using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject bunnyPrefab;
    public GameObject foxPrefab;
    public int bunnynum;
    public int foxnum;
    int i = 0;
    int j = 0;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {   bunnynum = SettingsmenuBunny.bunny;
        foxnum = SettingsmenuFox.fox;
        while ( i < bunnynum) {
            Vector3 pos = new Vector3(Random.Range(20, 80), Random.Range(4, 6), Random.Range(20, 80));
            Instantiate(bunnyPrefab, pos, Quaternion.identity);
            i++;

                  }
        while (j < foxnum)
        {
            Vector3 pos = new Vector3(Random.Range(20, 80), Random.Range(4, 6), Random.Range(20, 80));
            Instantiate(foxPrefab, pos, Quaternion.identity);
            j++;

        }
    }
        
}




