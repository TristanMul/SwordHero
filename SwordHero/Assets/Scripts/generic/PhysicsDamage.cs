using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDamage : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float velocityTreshold = 0;
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
        Debug.Log("Has triggered");
        if (rb.velocity.magnitude > velocityTreshold)
        {
            Debug.Log("threshold passed");
            Vector3 direction = this.transform.position - other.transform.position;
            direction.Normalize();
            if(other.tag == "Player")
            {
                Debug.Log("Player detected");
                knockbackForce = playerKnockbackForce;
            }
            else if(other.tag == "Enemy")
            {
                Debug.Log("Enemy detected");
                knockbackForce = enemyKnockbackForce;
                other.GetComponent<EnemyHealth>().TakeDamage(rb.velocity.magnitude * damageMultiplier);
            }
            else
            {
                Debug.Log("no player or enemy detected");
                knockbackForce = 0;
            }
            rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
            Debug.Log("Force added");
        }
    }
}
