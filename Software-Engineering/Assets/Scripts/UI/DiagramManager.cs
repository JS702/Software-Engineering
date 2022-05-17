using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiagramManager : MonoBehaviour
{
    [SerializeField] List<GameObject> diagrams;
    [SerializeField] GameObject y_Axis;
    [SerializeField] GameObject x_Axis;
    private int activeDiagramIndex = 0; // muss default m��ig dass diagram sein, was als erstes an ist

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            activeDiagramIndex++;
            setDiagram(true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            activeDiagramIndex--;
            setDiagram(false);
        }
    }

    private void setDiagram(bool direction)
    {
        int maxIndex = diagrams.Count - 1;
        if(activeDiagramIndex > maxIndex)
        {
            activeDiagramIndex = 0;
            diagrams[maxIndex].SetActive(false);
            diagrams[activeDiagramIndex].SetActive(true);
        }
        else if(activeDiagramIndex < 0)
        {
            activeDiagramIndex = maxIndex;

            diagrams[0].SetActive(false);
            diagrams[maxIndex].SetActive(true);
        }
        else
        {
            if (direction)
            {
                diagrams[activeDiagramIndex - 1].SetActive(false);
                diagrams[activeDiagramIndex].SetActive(true);
            }
            else
            {
                diagrams[activeDiagramIndex + 1].SetActive(false);
                diagrams[activeDiagramIndex].SetActive(true);
            }
           
        }
    }

    public void scaleAxis()
    {
        string number = y_Axis.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        int.TryParse(number, out int scale);
        scale *= 2;
        y_Axis.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = scale.ToString();

    }

        
}
