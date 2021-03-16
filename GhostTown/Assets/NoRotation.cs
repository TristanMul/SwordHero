using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotation : MonoBehaviour
{
    Quaternion initRotate;
    // Start is called before the first frame update
    void Start()
    {
        initRotate = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = initRotate;   
    }
}
