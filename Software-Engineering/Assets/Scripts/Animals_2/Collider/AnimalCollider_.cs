using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalCollider_ : MonoBehaviour
{
    public void removeMissingObjectsFromAnimalList(List<GameObject> animalList)
    {
        for (var i = animalList.Count - 1; i > -1; i--)
        {
            if (animalList[i] == null)
                animalList.RemoveAt(i);
        }
    }

    public GameObject lowestDistanceAnimal(GameObject self, List<GameObject> animalList)
    {
        Vector3 selfPosition = self.transform.position;
        float distanceToOther;
        float lowestDistance = 100;
        GameObject closestAnimal = null;

        if (animalList != null)
        {
            foreach (GameObject animal in animalList)
            {
                Vector3 otherAnimalPosition = animal.transform.position;
                distanceToOther = Vector3.Distance(selfPosition, otherAnimalPosition);
                if (distanceToOther < lowestDistance)
                {
                    closestAnimal = animal;
                    lowestDistance = distanceToOther;
                }
            }
            return closestAnimal;
        }
        return null;
    }
    public List<GameObject> potentialSexPartnerList;

}
