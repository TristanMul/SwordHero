using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    TrailRenderer trail;
     bool isAttacking;
    bool IsAttacking
    {
        get { return isAttacking; }
        set
        {
            isAttacking = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (IsAttacking)
        {
            if (other.gameObject.tag == "Enemy") {
                other.GetComponent<EnemyHealth>().TakeDamage(1f);
            }
        }
    }

    public void StartAttack()
    {
        IsAttacking = true;
        trail.enabled = true;
    }

    public void StopAttack()
    {
        IsAttacking = true;
        trail.enabled = false;
    }
}
