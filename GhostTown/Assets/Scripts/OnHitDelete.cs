using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDelete : MonoBehaviour
{
    private void OnTriggerEnter(Collider obj)
    {

        Debug.Log("Destroy");
        if (obj.tag == "Enemy")
        {
            if(obj.GetComponent<EnemyHealth>()){
                obj.GetComponent<EnemyHealth>().TakeDamage(1f);
            }
            Destroy(this.gameObject);
        }
    }
}
