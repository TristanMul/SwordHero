using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private float power;
    [SerializeField] private float radius;
    [SerializeField] private float upForce;
    [SerializeField] private float explosionDamage;
    [SerializeField] private float explodeThreshhold;
    private float oldVelocity;

    private void Start()
    {
        oldVelocity = GetComponent<Rigidbody>().velocity.sqrMagnitude;
        //explodeThreshhold *= explodeThreshhold;
    }

    private void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity.sqrMagnitude - oldVelocity > explodeThreshhold)
        {
            Detonate();
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// Makes a list of objects within radius and adds force to objects with PhysicsDamageComponent.
    /// </summary>
    public void Detonate(){
        GameManager.instance.TimeSlow(.5f, .5f);
        Collider[] potentialTargets = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider target in potentialTargets)
        {
            if(target.GetComponent<Rigidbody>() && target.GetComponent<PhysicsDamage>()){
                // Have enemies act like a ragdoll.
                if(target.CompareTag("Enemy")){
                    target.GetComponent<EnemyHealth>().TakeDamage(explosionDamage);
                }
                
                target.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Impulse);
            }
        }
    }
}
