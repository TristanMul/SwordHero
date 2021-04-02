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
    [SerializeField] private UpdateCoins coinUpdater;
    private FillProgressBar progressBar;
    private bool isDead = false;
    [SerializeField] private int numberOfCoins;

    private void Start()
    {
        coinUpdater = gameObject.GetComponent<UpdateCoins>();
        progressBar = GameManager.instance.progressBar;
    }
    // Enemy took a hit.
    public void TakeDamage(float damage){
        StartCoroutine(WhiteFlash());
        controllerClass.enemyState = EnemyBaseClass.EnemyState.Fall;
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
        newDeathAnimation.GetComponent<DeathAnimation>().Setup(numberOfCoins);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + numberOfCoins);
        //coinUpdater.UpdateCoinAmount();
        progressBar.Remove(this.gameObject);
        progressBar.UpdateProgressBar();
        this.gameObject.SetActive(false);
    }

    // Enemy flashes white when hit.
    IEnumerator WhiteFlash(){
        transform.GetComponentInChildren<Renderer>().material = whiteFlash;
        yield return new WaitForSeconds(0.15f);
        transform.GetComponentInChildren<Renderer>().material = ownMaterial;

    }
}
