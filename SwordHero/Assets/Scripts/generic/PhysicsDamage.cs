using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDamage : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float velocityTreshold = 1;
    [SerializeField] private float damageMultiplier = 0.5f;
    [SerializeField] private float blockKnockbackForce = 100;
    [SerializeField] private float enemyKnockbackForce = 25;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player Toched");
            Vector3 direction = this.transform.position - other.transform.position;
            direction.Normalize();
            rb.AddForce(direction * blockKnockbackForce, ForceMode.Impulse);

        }
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<EnemyHealth>())
            {
                if(rb.velocity.magnitude > velocityTreshold)
                other.GetComponent<EnemyHealth>().TakeDamage(rb.velocity.magnitude * damageMultiplier);
                
            }
        }
    }
}
