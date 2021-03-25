using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    PlayerMovement movement;
    bool playerStatic;
    Animator animator;
    TrailRenderer trail;
    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
        trail = GetComponentInChildren<TrailRenderer>();
        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movement != null && !movement.isMoving && !playerStatic)
        {
            Strike();
            playerStatic = true;
        }
        else if (movement != null & movement.isMoving && playerStatic)
        {
            StopAttack();
            playerStatic = false;
        }
    }

    void Strike()
    {
        if (animator != null)
        {
            animator.SetBool("Attack", true);
        }
        if (weapon != null)
        {
            weapon.StartAttack();
        }
    }
    void StopAttack()
    {
        if (animator != null)
        {
            animator.SetBool("Attack", false);
        }
        if (weapon != null) {
            weapon.StopAttack();
        }
    }
}
