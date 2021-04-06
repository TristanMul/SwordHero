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
    [SerializeField] private float forceMultiplier = 0.1f;
    [SerializeField] private float playerKnockbackForce = 100;
    [SerializeField] private float getUpTime = 0.5f;
    private float knockbackForce;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (this.tag == "Enemy")
        {
            agent = GetComponent<NavMeshAgent>();
            controllerClass = GetComponent<EnemyBaseClass>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapon")
        {
            ApplyKnockBack(playerKnockbackForce, other.gameObject);
        }
        // make sure only objects with a specific tag collide with this object & checks if the velocity threshold has been reached.
        if (other.GetComponent<PhysicsDamage>() && other.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= velocityTreshold)
        {
        Debug.Log(rb.velocity.magnitude);
            if (this.tag == "Enemy")
            {

                //if the current object is an enemy, take damage equal to the speed the other object has times a multiplier
                float amountOfDamage = other.GetComponent<Rigidbody>().velocity.magnitude * damageMultiplier;
                this.GetComponent<EnemyHealth>().TakeDamage(amountOfDamage);
            }
            ApplyKnockBack(other.gameObject.GetComponent<Rigidbody>().velocity.magnitude * forceMultiplier, other.gameObject);
        }
    }

    void ApplyKnockBack(float force, GameObject other)
    {
        //create a vector between the current object and the object it hits, give it a length of 1.
        Vector3 direction = this.transform.position - other.transform.position;
        direction.Normalize();
     
        //add the force to the object in the direction of the two objects that collided & equal to the knockback force that has been set
        this.rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
