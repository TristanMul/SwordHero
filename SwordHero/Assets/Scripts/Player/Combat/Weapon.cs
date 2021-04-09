using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public bool isAttacking;
    [SerializeField]private MeshCollider myCollider;


    bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        StopAttack();
        myCollider = GetComponent<MeshCollider>();
        myCollider.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {


        if (IsAttacking)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<EnemyHealth>().TakeDamage(1f);
            }
        }
    }


    public void StartAttack()
    {
        IsAttacking = true;
        myCollider.enabled = true;
    }

    public void StopAttack()
    {
        IsAttacking = false;
        myCollider.enabled = false;
    }
}
