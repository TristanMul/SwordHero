using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    private HealthBar healthBar;
    private Animator animator;

    void Start()
    {
        // Get player healthbar.
        healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        animator = transform.Find("Archer").GetComponent<Animator>();
    }

    public void TakeDamage(float damage){
        // Deal damage to player.
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if(currentHealth <= 0){
            // Player death logic here.
            // animator.Play("Death");
        }
    }

}
