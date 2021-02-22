using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Standards
{
    public MobileKeyboard target;
    public Quaternion standardRotation;
    void Start()
    {
        standardRotation = transform.rotation;
    }
    void Update()
    {
        if (target.lockedEnemy != null)
        {
            transform.LookAt(new Vector3(target.lockedEnemy.transform.position.x , 1.58f, target.lockedEnemy.transform.position.z), transform.up);
        }
        else
        { transform.rotation = standardRotation; } 
    }
}