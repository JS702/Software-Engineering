using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foxMovement : Movement
{
    float time;
    float jumpHeight;
    bool isGrounded;
    private Fox fox;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementSpeed = 10;
        rotationSpeed = 500;
        jumpHeight = 200;
        fox = GetComponent<Fox>();
    }

    private void Update()
    {
        if (!isWandering)
        {
            StartCoroutine(Wander());
        }
        else
        {

            if (isWalking && isGrounded) {
                if (time > 0.05)
                {
                    rb.MovePosition(transform.position + (transform.forward * movementSpeed * Time.deltaTime) + (transform.up * jumpHeight * Time.deltaTime));
                    time = 0;
                }
                else
                {
                    rb.MovePosition(transform.position + transform.forward * movementSpeed * Time.deltaTime);
                }
            }

            else if (isWalking)
            {
                rb.MovePosition(transform.position + transform.forward * movementSpeed * Time.deltaTime);
            }
            else if (isRotatingLeft)
            {
                rb.MoveRotation(Quaternion.Euler(transform.up * rotationSpeed * Time.deltaTime) * transform.rotation);
            }
            else if (isRotatingRight)
            {
                rb.MoveRotation(Quaternion.Euler(transform.up * -rotationSpeed * Time.deltaTime ) * transform.rotation);
            }
        }

        time = time + Time.deltaTime;
    }

    public Collider col;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prey")
        {
            Debug.Log("penis");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Environment")
        {
            isGrounded = true;
        }
        Debug.Log("isGrounded" + isGrounded);
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.collider.tag == "Environment")
        {
            isGrounded = false;
        }
        Debug.Log("isGrounded" + isGrounded);
    }
}
