using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected int movementSpeed; // für jedes Tier anders
    protected int rotationSpeed; // für jedes Tier anders

    protected bool isWandering = false;
    protected bool isRotatingLeft = false;
    protected bool isRotatingRight = false;
    protected bool isWalking = false;

    protected Rigidbody rb;

    protected IEnumerator Wander()
    {
        float rotationTime = Random.Range(0.5f, 1.5f);
        int rotateWait = Random.Range(1, 2);
        int rotateDirection = Random.Range(1, 3);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 7);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);

        isWalking = true;

        yield return new WaitForSeconds(walkTime);

        isWalking = false;

        yield return new WaitForSeconds(rotateWait);

        if(rotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }
        else if(rotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }

        isWandering = false;
    }



}
