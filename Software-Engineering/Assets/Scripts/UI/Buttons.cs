using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void quitApplication()
    {
        Application.Quit();
    }
    
    public void resume(GameObject pausePanel)
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FreeFlyCamera>().enabled = true;
        pausePanel.SetActive(false);
    }
}
