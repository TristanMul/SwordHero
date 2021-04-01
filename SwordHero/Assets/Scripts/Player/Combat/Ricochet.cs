using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    //private Transform projectilePos;
    //private Rigidbody projectileRigid;
    private bool canRicochet;
    Collider obj;
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Swordhit") && canRicochet && obj != null)
        {
            Richocet(obj.transform);
            canRicochet = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        obj = other;
        if (other.CompareTag("Enemy"))
        {
            canRicochet = true;
        }
    }

    void Richocet(Transform _object)
    {
        _object.GetComponent<Rigidbody>().velocity = -_object.GetComponent<Rigidbody>().velocity * 1.5f;
        _object.gameObject.layer = 14;
        _object.GetComponent<Collider>().isTrigger = true;
    }
}
