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
    Renderer[] renderers;

    void Start()
    {
        // Get player healthbar.
        healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        animator = GetComponentInChildren<Animator>();

        renderers = animator.GetComponentsInChildren<Renderer>();



        invulnerable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("IsDead", true);
        }
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
            if (currentHealth <= 0)
            {
                // Player death logic here.
                GetComponent<PlayerMovement>().enabled = false;
                
                Debug.Log(animator);
                animator.SetBool("IsDead", true);
            }

        }
    }

    IEnumerator DamagedEffect(){
        int flashDuration = 5;   // The amount of times renderer should be turned off and on again.
        int flashCount = 0;     //  Counter for flashing.

        // Turn renederer off and on again for flashing effect.
        while (flashCount < flashDuration)
        {
            foreach(Renderer r in renderers)
            {
                r.enabled = false;
            }
            yield return new WaitForSeconds(0.1f);
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);

            flashCount++;
        }


        // Player can take damage again.
        invulnerable = false;
    }

    private void OnCollisionEnter(Collision other) {
        // Player got hit by enemy.
        if(other.gameObject.tag == "Enemy"){
            TakeDamage(2f);
        }
    }

    private void OnCollisionStay(Collision other) {
        // Player got hit by enemy.
        if(other.gameObject.tag == "Enemy"){
            TakeDamage(2f);
        }
    }

}
