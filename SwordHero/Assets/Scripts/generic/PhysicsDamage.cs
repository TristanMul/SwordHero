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
    [SerializeField] private float enemyKnockbackForce = 25;
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
        // make sure only objects with a specific tag collide with this object & checks if the velocity threshold has been reached.
        if(other.tag == "Weapon" || ((other.tag == "Object" || other.tag == "Enemy") && other.GetComponent<Rigidbody>().velocity.magnitude >= velocityTreshold))
        {
            //create a vector between the current object and the object it hits, give it a length of 1.
            Vector3 direction = this.transform.position - other.transform.position;
            direction.Normalize();

            //assign a value to knockbackforce
            knockbackForce = assignKnockbackforce(other.gameObject);
            //if the current object is an enemy, take damage equal to the speed the other object has times a multiplier
            if (this.tag == "Enemy")
                {
                    this.GetComponent<EnemyHealth>().TakeDamage(other.GetComponent<Rigidbody>().velocity.magnitude * damageMultiplier);
                }
              //add the force to the object in the direction of the two objects that collided & equal to the knockback force that has been set
                this.rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
        }

        float assignKnockbackforce(GameObject other)
        {
            
            if (other.tag == "Weapon" && other.gameObject.GetComponent<Weapon>().isAttacking)
            {
                //if the object that hits this object is a weapon and the player is trying to attack, give a constant knockback force
                return playerKnockbackForce;
            }
            else //if(other.tag == "Object" || other.tag == "Enemy")
            {
                //if it is another object or an enemy, give a force equal to the velocity of the other game object times a multiplier
                return other.gameObject.GetComponent<Rigidbody>().velocity.magnitude * forceMultiplier;
            }
        }
        /*++Vector3 direction = this.transform.position - other.transform.position;
        ++direction.Normalize();

        if (other.tag == "Weapon" && other.gameObject.GetComponent<Weapon>().isAttacking)
        {
            ++knockbackForce = playerKnockbackForce;
            if(this.tag == "Enemy")
            {
                --EnemyHit(this.gameObject);
            }
        }

        ??if (rb.velocity.magnitude > velocityTreshold)
        {
            
           if (other.tag == "Enemy")
            {
              
                ++knockbackForce = enemyKnockbackForce;
                ++ other.GetComponent<EnemyHealth>().TakeDamage(rb.velocity.magnitude * damageMultiplier);
                --EnemyHit(other.gameObject);
            }
            else
            {
                
                ++knockbackForce = 0;
            }

            
        }
        ++this.rb.AddForce(direction * knockbackForce, ForceMode.Impulse);*/
    }
  
    public void EnemyHit(GameObject Enemy)
    {
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
