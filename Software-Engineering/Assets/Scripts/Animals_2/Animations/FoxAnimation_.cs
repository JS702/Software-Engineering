using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAnimation_ : MonoBehaviour
{
       private FoxMovement_ movement;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<FoxMovement_>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isMoving", getIsMoving());
        animator.SetBool("isAlive", getIsAlive());
        animator.SetBool("isEating", getIsEating());
        //Debug.Log("IsEating: " + getIsEating());
    }

    public bool getIsMoving()
    {
        return (movement.isWandering || movement.isHunting) && movement.fox.isAlive;
    }

    public bool getIsAlive()
    {
        return movement.fox.isAlive;
    }

    public bool getIsEating()
    {
        return movement.fox.isEating;
    }
}