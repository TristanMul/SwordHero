using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public bool testing;
    bool chargingParcticlesActive;//To activate the charging particles set ParticlesRunning to true, mind the capitol letter
    bool whileChargedActive;
    public ParticleSystem chargingParticles;
    public ParticleSystem isChargedParticles;
    public ParticleSystem whileChargedParticles;

    public bool ChargingParticlesActive//set to activate/deactivate particles
    {
        get { return chargingParcticlesActive; }
        set
        {
            if (value)
            {
                chargingParticles.Play();
            }
            else
            {
                chargingParticles.Stop();
            }
            chargingParcticlesActive = value;
        }
    }

    public bool WhileChargedActive
    {
        get { return whileChargedActive; }
        set
        {
            if (value)
            {
                whileChargedParticles.Play();
            }
            else
            {
                whileChargedParticles.Stop();
            }
            whileChargedActive = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (testing)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ChargingParticlesActive = !chargingParcticlesActive;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                PlayIsCharged();
            }
        }
    }

    public void PlayIsCharged()
    {
        isChargedParticles.Play();
    }

}
