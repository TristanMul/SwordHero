﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDelete : MonoBehaviour
{
    public GameObject deathParticles;
    private void OnTriggerEnter(Collider obj)
    {

        Debug.Log("Destroy");
        if (obj.tag == "Enemy")
        {
            GameObject newDeathAnimation =  Instantiate(deathParticles, obj.transform.position, deathParticles.transform.rotation);
            newDeathAnimation.GetComponent<DeathAnimation>().Setup(5);
            Destroy(this.gameObject);
        }
    }
}
