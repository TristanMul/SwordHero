using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles: MonoBehaviour
{
    public bool testing;
    bool particlesRunning;//To activate the charging particles set ParticlesRunning to true, mind the capitol letter
    public ParticleSystem chargingParticles;
    public ParticleSystem isChargedParticles;

    public bool ParticlesRunning//set to activate/deactivate particles
    {
        get { return particlesRunning; }
        set
        {
            if (value) {
                chargingParticles.Play();
            }
            else {
                chargingParticles.Stop();
            }
            particlesRunning = value;
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
                ParticlesRunning = !particlesRunning;
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
