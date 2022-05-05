using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foxAnimation : MonoBehaviour
{
    private foxMovement movement;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponentInParent<foxMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isMoving", getIsMoving());
    }

    public bool getIsMoving()
    {
        return movement.isWandering || movement.isHunting;
    }
}
