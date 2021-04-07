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

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if (other.CompareTag("EnemyProjectile"))
            {
                Richocet(other.transform);
            }
        }
    }

    void Richocet(Transform _object)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Swordhit") &&  _object != null)
        {
            GameManager.instance.TimeSlow(.5f, .5f);
            _object.GetComponent<Rigidbody>().velocity = -_object.GetComponent<Rigidbody>().velocity * 1.5f;
            _object.gameObject.layer = 14;
            _object.GetComponent<Collider>().isTrigger = true;
            canRicochet = false;
        }
    }
}

