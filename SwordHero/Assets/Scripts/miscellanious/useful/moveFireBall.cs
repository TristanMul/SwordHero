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
    }

    void FixedUpdate()
    {
        thisRigidbody.AddForce(thisRigidbody.transform.forward * MovementSpeed, ForceMode.Impulse);
        destructionTimer -= Time.deltaTime;
        if(destructionTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
