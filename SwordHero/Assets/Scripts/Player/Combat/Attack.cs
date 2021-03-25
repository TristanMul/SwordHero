using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    PlayerMovement movement;
    bool playerStatic;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movement != null && !movement.isMoving && !playerStatic) 
        {
            Strike();
        }
        playerStatic = true;

    }

    void Strike() {
        if(animator != null)
        {
            animator.SetBool("Attack", true);
            //animator.SetBool("Attack", false);

        }
    }
}
