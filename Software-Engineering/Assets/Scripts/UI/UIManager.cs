using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject diagramPanel;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isActive = diagramPanel.activeInHierarchy;
            diagramPanel.SetActive(!isActive);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            float timeScale = Time.timeScale;
            if (timeScale >= 16 )
            {
                Time.timeScale = 0.5f;
            }
            else
            {
                Time.timeScale *= 2;
            }

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            float timeScale = Time.timeScale;

            if (timeScale <= 0.5)
            {
                Time.timeScale = 16f;
            }
            else
            {
                Time.timeScale /= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.timeScale > 0)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
