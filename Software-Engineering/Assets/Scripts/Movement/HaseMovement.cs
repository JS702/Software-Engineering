using UnityEngine;

public class HaseMovement : Movement
{
    Hase hase;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementSpeed = 10;
        rotationSpeed = 500;
    }

    private void Update()
    {
        if(!isWandering)
        {
            StartCoroutine(Wander());
        }
        else
        {
            if (isWalking)
            {
                rb.MovePosition(transform.position + transform.forward * movementSpeed * Time.deltaTime);
            }
            else if (isRotatingLeft)
            {
                rb.MoveRotation(Quaternion.Euler(transform.up * rotationSpeed * Time.deltaTime) * transform.rotation);
            }
            else if (isRotatingRight)
            {
                rb.MoveRotation(Quaternion.Euler(-transform.up * rotationSpeed * Time.deltaTime) * transform.rotation);
            }
        }
    }

    private void flüchten()
    {
        if (hase.isFleeing)
        {

        }
    }
}
