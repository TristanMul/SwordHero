using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public bool isAttacking;
    [SerializeField]private MeshCollider myCollider;

    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }


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
        if (!colliders.Contains(other) && other.gameObject.tag == "Enemy") { colliders.Add(other); }//Adds colliders to a list of colliders


        if (IsAttacking)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("attack");
                other.GetComponent<EnemyHealth>().TakeDamage(1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Exit " + other);
            colliders.Remove(other);
        }
    }
    public void StartAttack()
    {
        foreach(Collider col in colliders)
        {
            if (col.gameObject.tag == "Enemy")
            {
                col.GetComponent<EnemyHealth>().TakeDamage(1f);
            }
        }
        IsAttacking = true;
        myCollider.enabled = true;
    }

    public void StopAttack()
    {
        IsAttacking = false;
        myCollider.enabled = false;
    }
}
