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
        if (other.name == "testBlock")
        {
            Debug.Log(other.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        }
        if(other.tag == "Weapon")
        {
            ApplyKnockBack(playerKnockbackForce, other.gameObject);
        }
        // make sure only objects with a specific tag collide with this object & checks if the velocity threshold has been reached.
        if (other.GetComponent<PhysicsDamage>() && other.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= velocityTreshold)
        {
            if (this.tag == "Enemy")
            {
                GetComponent<RagdollManager>().ReceiveDirection(other.gameObject);
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
        Vector3 direction = GetDirection(other);
        direction.y = 0;
        direction.Normalize();
     
        //add the force to the object in the direction of the two objects that collided & equal to the knockback force that has been set
        this.rb.AddForce(direction * force, ForceMode.Impulse);
    }
    Vector3 GetDirection(GameObject other)
    {
        if(this.tag == "Object" && other.tag == "Weapon")
        {
            return other.transform.parent.forward;
        }
            return this.transform.position - other.transform.position;
    }
     /*IEnumerator RotateToHit(GameObject other)
    {
        Vector3 direction = other.transform.position - this.transform.position;
        direction.Normalize();
    }*/
}
