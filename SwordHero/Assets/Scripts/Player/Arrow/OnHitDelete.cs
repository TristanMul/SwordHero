using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDelete : MonoBehaviour
{
    public GameObject hitEffect;
    private PlayerHealth player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

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
        else if(obj.tag == "Player")
        {
            player.TakeDamage(1f);
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
