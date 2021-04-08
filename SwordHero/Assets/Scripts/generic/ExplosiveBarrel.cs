using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private float power;
    [SerializeField] private float radius;
    [SerializeField] private float upForce;
    [SerializeField] private float explosionDamage;
    [SerializeField] private float hardFallVelocity;
    [SerializeField] private GameObject explosionVisual;
    
    private bool isExplodable;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isExplodable = false;
    }
    
    /// <summary>
    /// Makes a list of objects within radius and adds force to objects with PhysicsDamageComponent.
    /// </summary>
    public void Detonate(){
        GameManager.instance.TimeSlow(1f, .5f);

        Collider[] potentialTargets = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider target in potentialTargets)
        {
            if(target.GetComponent<Rigidbody>() && target.GetComponent<PhysicsDamage>()){
                if(target.CompareTag("Enemy")){
                    target.GetComponent<EnemyHealth>().TakeDamage(explosionDamage);
                }
                
                target.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Impulse);
            }
        }

        if(explosionVisual != null)
        {
            Instantiate(explosionVisual, transform.position, explosionVisual.transform.rotation);
        }

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) {
        bool onHitGround = other.gameObject.tag == "Walkable";
        bool onHardFall = other.relativeVelocity.magnitude >= hardFallVelocity;

        if(onHitGround && onHardFall){
            Detonate();
        }

        if(isExplodable) Detonate();
    }
    
    private void OnTriggerEnter(Collider other) {
        bool onPlayerAttack = other.gameObject.tag == "Weapon";

        if(onPlayerAttack){
            isExplodable = true;
        }
    }
}
