using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDelete : MonoBehaviour
{
    public GameObject hitEffect;
    [SerializeField] private float damagepercentage = 0.5f;
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Enemy")
        {
            if(obj.GetComponent<EnemyHealth>()){
                obj.GetComponent<EnemyHealth>().TakeDamage(1f);
            }
            Instantiate(hitEffect, transform.position, transform.rotation);

            Destroy(this.gameObject);
        }
    }
}
