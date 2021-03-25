using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }


    private void OnTriggerEnter(Collider other)
    {

    }

    public void StartAttack()
    {
        trail.enabled = true;
    }

    public void StopAttack()
    {
        trail.enabled = false;
    }
}
