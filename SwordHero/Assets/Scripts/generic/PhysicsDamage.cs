using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class PhysicsDamage : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;
    EnemyBaseClass controllerClass;
    [SerializeField] private float velocityTreshold = 0;
    [SerializeField] private float damageMultiplier = 0.5f;
    [SerializeField] private float playerKnockbackForce = 100;
    [SerializeField] private float enemyKnockbackForce = 25;
    [SerializeField] private float getUpTime = 0.5f;
    private float knockbackForce;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(this.tag == "Enemy")
        {
            agent = GetComponent<NavMeshAgent>();
            controllerClass = GetComponent<EnemyBaseClass>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Weapon" && other.gameObject.GetComponent<Weapon>().isAttacking)
        {
            knockbackForce = playerKnockbackForce;
            if(this.tag == "Enemy")
            {
                EnemyHit(this.gameObject);
            }
        }
        Vector3 direction = this.transform.position - other.transform.position;
        direction.Normalize();
        if (rb.velocity.magnitude > velocityTreshold)
        {
            
           if (other.tag == "Enemy")
            {
              
                knockbackForce = enemyKnockbackForce;
                other.GetComponent<EnemyHealth>().TakeDamage(rb.velocity.magnitude * damageMultiplier);
                EnemyHit(other.gameObject);
            }
            else
            {
                
                knockbackForce = 0;
            }

            
        }
        this.rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
    }
    void EnemyHit(GameObject Enemy)
    {
        Debug.Log("AmHit");
        Enemy.GetComponent<NavMeshAgent>().enabled = false;
        Enemy.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Enemy.gameObject.GetComponent<Rigidbody>().useGravity = true;
        controllerClass.enemyState = EnemyBaseClass.EnemyState.Fall;
        StartCoroutine(getBackUp(getUpTime, Enemy));
    }
    IEnumerator getBackUp(float time, GameObject Enemy)
    {
        Debug.Log("Started coroutine");
        yield return new WaitForSeconds(time);
        Enemy.GetComponent<NavMeshAgent>().enabled = true;
        Enemy.GetComponent<Rigidbody>().isKinematic = true;
        Enemy.GetComponent<Rigidbody>().useGravity = false;
        controllerClass.enemyState = EnemyBaseClass.EnemyState.Move;
    }
}
