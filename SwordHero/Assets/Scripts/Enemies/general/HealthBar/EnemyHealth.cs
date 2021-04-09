using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyBaseClass))]
public class EnemyHealth : MonoBehaviour
{
    public EnemyBaseClass controllerClass;  // The controller class of this enemy object.
    public Material ownMaterial;
    public Material whiteFlash;
    [SerializeField] private UpdateCoins coinUpdater;
    private FillProgressBar progressBar;
    private bool isDead = false;
    [SerializeField] private int numberOfCoins;
    [SerializeField] private float minimalHealthMult;

    #region Events
    public event Action StartRagdoll;
    public event Action OnEnemyDeath;
    #endregion
    private void Start()
    {
        coinUpdater = gameObject.GetComponent<UpdateCoins>();
        progressBar = GameManager.instance.progressBar;
    }
    // Enemy took a hit.
    public void TakeDamage(float damage){
        StartRagdoll?.Invoke();
        StartCoroutine(WhiteFlash());
        controllerClass.enemyState = EnemyBaseClass.EnemyState.Fall;
        controllerClass.CurrenHealth -= damage;
        controllerClass.HealthBar.UpdateHealthBar(controllerClass.CurrenHealth, controllerClass.MaxHealth);

        // Enemy is dead.
        if(controllerClass.CurrenHealth <= controllerClass.MaxHealth * minimalHealthMult && !isDead){
            EnemyDeath();
            isDead = true;
        }
    }

    // Enemy is dead.
    private void EnemyDeath(){
        OnEnemyDeath?.Invoke();
        //GameManager.instance.TimeSlow(.5f, .5f);

        controllerClass.enemyState = EnemyBaseClass.EnemyState.Death;

        // Play death effect.
        GameManager.instance.AddCoins(5);
        //coinUpdater.UpdateCoinAmount();
        progressBar.Remove(this.gameObject);
        // progressBar.UpdateProgressBar();
        //this.gameObject.SetActive(false);


    }



    // Enemy flashes white when hit.
    IEnumerator WhiteFlash(){
        transform.GetComponentInChildren<Renderer>().material = whiteFlash;
        yield return new WaitForSeconds(0.15f);
        transform.GetComponentInChildren<Renderer>().material = ownMaterial;

    }
}
