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
    bool playerHasStarted;

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
        if (!playerHasStarted && movement.isMoving)
        {
            playerHasStarted = true;
        }

        if (playerHasStarted)
        {
            //starts the attack
            if (movement != null && !movement.isMoving && !playerStatic)
            {
                StartAttack();
                playerStatic = true;
            }
            else if (movement != null & movement.isMoving && playerStatic)
            {
                StopAttack();
                playerStatic = false;
            }
        }
    }

    void StartAttack()
    {
        if (animator != null) { animator.SetBool("Attack", true); }
        if (weapon != null) { weapon.StartAttack(); }
        if (movement != null) { movement.Dash(); }
    }
    void StopAttack()
    {
        if (animator != null) { animator.SetBool("Attack", false); }
        if (weapon != null) { weapon.StopAttack(); }
    }
}
