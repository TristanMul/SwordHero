using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    private Transform projectilePos;
    private Rigidbody projectileRigid;
    private bool canRicochet;
    Collider obj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canRicochet)
        {
            Richocet(obj.transform);
            Debug.Log("Hit");
            canRicochet = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        obj = other;
        if (other.CompareTag("Projectile"))
        {
            canRicochet = true;
        }
    }

    void Richocet(Transform _object)
    {
        _object.GetComponent<Rigidbody>().velocity = -_object.GetComponent<Rigidbody>().velocity;
        Debug.Log("Changed units");
    }
}
