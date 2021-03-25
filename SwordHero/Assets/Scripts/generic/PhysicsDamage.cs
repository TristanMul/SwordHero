using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDamage : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
