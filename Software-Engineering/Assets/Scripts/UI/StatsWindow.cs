using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsWindow : MonoBehaviour
{
    [SerializeField] GameObject statsWindow;
    [SerializeField] Text animalName;
    [SerializeField] Bars healthBar;
    [SerializeField] Bars hungerBar;
    [SerializeField] Bars thirstBar;
    [SerializeField] Bars reproductionDriveBar;
    private Animal animal1;

    private void Update()
    {
        if(animal1 != null)
        {
            StartCoroutine(updateStats());
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            statsWindow.SetActive(false);
        }
    }

    public void init(Animal animal)
    {
        statsWindow.SetActive(true);
        animal1 = animal;
        //this.animalName.text = animal.name;
        this.healthBar.setMaxValue(animal1.health);
        this.hungerBar.setMaxValue(animal1.hunger);
        this.thirstBar.setMaxValue(animal1.thirst);
        this.reproductionDriveBar.setMaxValue(animal1.reproductionDrive);
    }

    IEnumerator updateStats()
    {
        this.healthBar.setValue(animal1.currentHealth);
        this.hungerBar.setValue(animal1.currentHunger);
        this.thirstBar.setValue(animal1.currentThirst);
        this.reproductionDriveBar.setValue(animal1.currentHorny);

        yield return new WaitForSeconds(3f);
    }


}
