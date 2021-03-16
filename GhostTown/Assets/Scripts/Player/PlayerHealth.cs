using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    private HealthBar healthBar;
    private Animator animator;
    private bool invulnerable;

    void Start()
    {
        // Get player healthbar.
        healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        animator = transform.Find("Archer").GetComponent<Animator>();

        invulnerable = false;
    }

    public void TakeDamage(float damage){
        if(!invulnerable){
            // Player can't take damage for a while after taking damage.
            invulnerable = true;

            // Play flashing effect.
            StartCoroutine(DamagedEffect());

            // Deal damage to player.
            currentHealth -= damage;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);

            // Check if player is dead.
            if(currentHealth <= 0){
                // Player death logic here.
                // animator.Play("Death");
            }
        }
    }

    IEnumerator DamagedEffect(){
        int flashDuration = 5;   // The amount of times renderer should be turned off and on again.
        int flashCount = 0;     //  Counter for flashing.

        // Turn renederer off and on again for flashing effect.
        while (flashCount < flashDuration)
        {
            transform.Find("Archer").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            transform.Find("Archer").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);

            flashCount++;
        }

        // Player can take damage again.
        invulnerable = false;
    }

    private void OnCollisionStay(Collision other) {
        // Player got hit by enemy.
        if(other.gameObject.tag == "Enemy"){
            TakeDamage(2f);
        }
    }

}
