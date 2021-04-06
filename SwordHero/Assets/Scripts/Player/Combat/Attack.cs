using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    PlayerMovement movement;
    bool playerStatic;
    Animator animator;
    Weapon weapon;
    bool playerHasStarted;
    PlayerParticles particles;

    bool attackIsCharged;
    float chargeTimer;
    [SerializeField] float chargeTime;
    [SerializeField] float waitToChargeTime = .2f;
    float waitToChargeTimer;
    bool isCharging;

    #region events
    public delegate void AttackDelegate();
    public AttackDelegate onAttack;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
        weapon = GetComponentInChildren<Weapon>();
        particles = GetComponentInChildren<PlayerParticles>();
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
                particles.ChargingParticlesActive = false;
                isCharging = false;
            }
            else if (movement != null & movement.isMoving)
            {

                if (playerStatic)
                {
                    StopAttack();
                    playerStatic = false;
                }
                if (waitToChargeTimer > waitToChargeTime)//To run when the player is walking for a certain amount of time
                {
                    if (!isCharging)//To run at the start of charging
                    {
                        particles.ChargingParticlesActive = true;
                        isCharging = true;
                    }
                    chargeTimer += Time.deltaTime / chargeTime;
                    if (!attackIsCharged && chargeTimer > 1f)
                    {
                        attackIsCharged = true;
                        particles.ChargingParticlesActive = false;
                        particles.PlayIsCharged();
                    }
                }
                else { waitToChargeTimer += Time.deltaTime; }

            }
            else waitToChargeTimer = 0f;
        }


    }


    /// <summary>
    /// Activates the sword to do damage and activates the visuals
    /// </summary>
    void StartAttack()
    {
        if (animator != null) { animator.Play("Swordhit"); }
        if (weapon != null) { weapon.StartAttack(); }
        if (attackIsCharged)
        {
            attackIsCharged = false;
            chargeTimer = 0f;
            if (movement != null) { movement.Dash(); }
        }
        onAttack();
    }

    /// <summary>
    /// Deactivates the sword to do damage and deactivates the visuals
    /// </summary>
    public void StopAttack()
    {
        if (animator != null) { animator.SetBool("Attack", false); }
        if (weapon != null) { weapon.StopAttack(); }
    }
}
