using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PhysicsDamage : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;
    EnemyBaseClass controllerClass;
    [SerializeField] private float velocityTreshold = 0;
    [SerializeField] private float damageMultiplier = 0.5f;
    [SerializeField] private float playerKnockbackForce = 100;
    [SerializeField] private float enemyKnockbackForce = 25;
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

        if (other.tag == "Player")
        {
            knockbackForce = playerKnockbackForce;
        }
        Vector3 direction = this.transform.position - other.transform.position;
        direction.Normalize();
        if (rb.velocity.magnitude > velocityTreshold)
        {
            Debug.Log("Past treshold");
           if (other.tag == "Enemy")
            {
              
                knockbackForce = enemyKnockbackForce;
                other.GetComponent<EnemyHealth>().TakeDamage(rb.velocity.magnitude * damageMultiplier);
            }
            else
            {
                
                knockbackForce = 0;
            }

            
        }
        rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
    }
}
