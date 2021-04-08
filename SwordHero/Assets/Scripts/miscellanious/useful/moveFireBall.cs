using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFireBall : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float destructionTimer;
    Rigidbody thisRigidbody;

    private void Awake()
    {
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
        thisRigidbody.velocity = transform.forward * MovementSpeed;
    }

    void FixedUpdate()
    {
        
        destructionTimer -= Time.deltaTime;
        if(destructionTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
