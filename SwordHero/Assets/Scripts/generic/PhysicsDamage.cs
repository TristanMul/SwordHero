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
    [SerializeField] private float getUpTime = 0.5f;
    private float knockbackForce;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Weapon" && other.gameObject.GetComponent<Weapon>().isAttacking)
        {
            knockbackForce = playerKnockbackForce;
        }
        Vector3 direction = this.transform.position - other.transform.position;
        direction.Normalize();
        if (rb.velocity.magnitude > velocityTreshold)
        {
            
           if (other.tag == "Enemy")
            {
              
                knockbackForce = enemyKnockbackForce;
                other.GetComponent<EnemyHealth>().TakeDamage(rb.velocity.magnitude * damageMultiplier);
                other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                controllerClass.enemyState = EnemyBaseClass.EnemyState.Fall;
                //StartCoroutine(getBackUp(getUpTime));
            }
            else
            {
                
                knockbackForce = 0;
            }

            
        }
        rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
    }
    IEnumerator getBackUp(float time)
    {
        Debug.Log("Started coroutine");
        yield return new WaitForSeconds(time);
        this.GetComponent<NavMeshAgent>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Rigidbody>().useGravity = false;
        controllerClass.enemyState = EnemyBaseClass.EnemyState.Move;
    }
}
