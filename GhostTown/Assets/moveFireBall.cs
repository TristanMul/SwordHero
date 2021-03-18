using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFireBall : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;

    void FixedUpdate()
    {
        transform.position += Vector3.up * MovementSpeed;
    }
}
