using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBaseClass))]
public class EnemyHealth : MonoBehaviour
{
    public EnemyBaseClass controllerClass;  // The controller class of this enemy object.
    public Material ownMaterial;
    public Material whiteFlash;
    public GameObject deathParticles;
    private bool isDead = false;

    // Enemy took a hit.
    public void TakeDamage(float damage){
        StartCoroutine(WhiteFlash());
        controllerClass.CurrenHealth -= damage;
        controllerClass.HealthBar.UpdateHealthBar(controllerClass.CurrenHealth, controllerClass.MaxHealth);

        // Enemy is dead.
        if(controllerClass.CurrenHealth <= 0 && !isDead){
            EnemyDeath();
            isDead = true;
        }
    }

    // Enemy is dead.
    private void EnemyDeath(){
        controllerClass.enemyState = EnemyBaseClass.EnemyState.Death;

        // Play death effect.
        GameObject newDeathAnimation =  Instantiate(deathParticles, transform.position, deathParticles.transform.rotation);
        newDeathAnimation.GetComponent<DeathAnimation>().Setup(5);

        Destroy(gameObject);
    }

    // Enemy flashes white when hit.
    IEnumerator WhiteFlash(){
        transform.Find("Spider").GetComponent<Renderer>().material = whiteFlash;
        yield return new WaitForSeconds(0.15f);
        transform.Find("Spider").GetComponent<Renderer>().material = ownMaterial;

    }
}
