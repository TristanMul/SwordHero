using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFireBall : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float destructionTimer;

    void FixedUpdate()
    {
        transform.position += Vector3.up * MovementSpeed;
        destructionTimer -= Time.deltaTime;
        if(destructionTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
