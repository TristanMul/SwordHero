using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObjects : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy") 
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(Mathf.Infinity);
        }
        else { Destroy(other.gameObject); }
    }
}
