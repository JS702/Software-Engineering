using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuchsMovement : Movement
{
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementSpeed = 10;
        rotationSpeed = 500;
    }

    private void Update()
    {
        if (!isWandering)
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
}