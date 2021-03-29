using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDamage : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float velocityTreshold = 1;
    [SerializeField] private float damageMultiplier = 0.5f;
    [SerializeField] private float playerKnockbackForce = 100;
    [SerializeField] private float enemyKnockbackForce = 25;
    private float knockbackForce;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (rb.velocity.magnitude > velocityTreshold)
        {
            Vector3 direction = this.transform.position - other.transform.position;
            direction.Normalize();
            if(other.tag == "Player")
            {
                knockbackForce = playerKnockbackForce;
            }
            else if(other.tag == "Enemy")
            {
                knockbackForce = enemyKnockbackForce;
                other.GetComponent<EnemyHealth>().TakeDamage(rb.velocity.magnitude * damageMultiplier);
            }
            else
            {
                knockbackForce = 0;
            }
            rb.AddForce(direction * knockbackForce, ForceMode.Impulse);

        }
    }
}
