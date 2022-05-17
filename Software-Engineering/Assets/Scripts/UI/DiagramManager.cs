using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagramManager : MonoBehaviour
{
    [SerializeField] List<GameObject> diagrams;
    private int activeDiagramIndex = 0; // muss default mäßig dass diagram sein, was als erstes an ist

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
        
}
